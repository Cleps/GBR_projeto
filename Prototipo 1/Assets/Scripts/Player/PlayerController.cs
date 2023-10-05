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

        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        if (isGrounded){
            anim.SetBool("nochao", true);
        }else{
            anim.SetBool("nochao", false);
        }
         // parte do pulo
        Jump();

        Move();
    }

    private void Jump(){
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer); // para checar se o player ta no chão

        if (isGrounded && Input.GetButtonDown("Jump")){
            //rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            rb.velocity = Vector2.up * jumpforce;//(rb.velocity.x, jumpforce);
            //rb.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
            //rb.AddForce(new Vector3(0f, jumpforce, 0f), ForceMode2D.Impulse);

        }
    }

    private void Move(){
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
    }
}
