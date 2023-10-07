using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash")]
    public bool canDash = true;
    private bool isDashing;
    public float speedDash;
    public float timeDash;
    private float currentTimeDash;
    private float originalGravity;
    private PlayerController playerScript;
    private Rigidbody2D rbPlayer;
    private Animator anim;
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
        if (Input.GetMouseButton(1) && canDash) //&& playerScript.isGrounded)
        {
            Dash();
        }



        else if(Input.GetMouseButtonUp(1))
        {
            canDash = true;
            isDashing = false;
            anim.SetBool("dash", false);
            rbPlayer.gravityScale = originalGravity;
            timeDash = currentTimeDash;
        }
        
    }

    private void Dash()
    {
        isDashing = true;
        rbPlayer.gravityScale = 0f;
        timeDash -= Time.deltaTime;
        anim.SetBool("dash", true);

        rbPlayer.velocity = new Vector2(playerScript.horizontal * speedDash, 0f);// rbPlayer.velocity.y);

        if (timeDash <= 0)
        {
            rbPlayer.gravityScale = originalGravity;
            isDashing = false;
            canDash = false;
            anim.SetBool("dash", false);
        }
    }


}
