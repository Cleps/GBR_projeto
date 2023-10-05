using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float jumpforce = 5f;
    [SerializeField]
    private float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;



    private Animator anim;
    private SpriteRenderer spriteRenderer; // Referência ao SpriteRenderer do personagem

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector2(horizontal, 0);

        if (horizontal != 0){
            anim.SetBool("idle", false);
        }else{
            anim.SetBool("idle", true);
        }


        if (horizontal > 0)
        {
            spriteRenderer.flipX = false; // Sem inversão horizontal
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true; // Inversão horizontal para trás
        }

         // parte do pulo
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer); // para checar se o player ta no chão

        if (isGrounded && Input.GetButtonDown("Jump")){
            print(jumpforce);
            //rb.velocity = Vector2.up * jumpforce;//(rb.velocity.x, jumpforce);
            rb.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movePosition = (speed * Time.fixedDeltaTime * moveDirection.normalized) + rb.position;
        rb.MovePosition(movePosition);
    }
}
