using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash")]
    public bool canDash = true;
    public bool isDashing;
    public float speedDash;
    public float timeDash;
    private float currentTimeDash;
    private float originalGravity;
    private PlayerController playerScript;
    private Rigidbody2D rbPlayer;
    private Animator anim;
    private bool airDash;
    private bool dash;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer =GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerScript = FindObjectOfType<PlayerController>();

        originalGravity = rbPlayer.gravityScale;
        currentTimeDash = timeDash;
        canDash = true;
        isDashing = false;
    }

    void Update()
    {//Input.GetKeyDown(KeyCode.LeftShift)

        if (timeDash <= 0)
        {
            rbPlayer.gravityScale = originalGravity;
            isDashing = false;
            canDash = false;
            anim.SetBool("dash", false);
        }

        if (Input.GetMouseButton(1)) //&& playerScript.isGrounded)
        {
            timeDash -= Time.deltaTime;
            if(canDash && !dash){
                Dash();
            }

        }

        else if(Input.GetMouseButtonUp(1))
        {
            canDash = true;
            isDashing = false;
            anim.SetBool("dash", false);
            rbPlayer.gravityScale = originalGravity;
            timeDash = currentTimeDash;
        }

        if(playerScript.isGrounded && dash){
            dash = false;
        }
    
        
    }

    private void Dash()
    {
        
        isDashing = true;
        rbPlayer.gravityScale = 0f;
        anim.SetBool("dash", true);

        rbPlayer.velocity = new Vector2(playerScript.transform.localScale.x * speedDash, 0f);// rbPlayer.velocity.y);

        if (!playerScript.isGrounded && isDashing){
            dash = true;
        }else{
            dash = false;
        }

    }


}
