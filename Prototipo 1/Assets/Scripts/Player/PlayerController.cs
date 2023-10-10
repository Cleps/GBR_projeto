using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private Animator anim;
    private SpriteRenderer spriteRenderer; // Referência ao SpriteRenderer do personagem

    private bool run = false;
    private bool isGrounded;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Vector2 areaBoxGround;

    [SerializeField]
    private LayerMask groundLayer;

    private bool pulo;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, areaBoxGround);

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if( horizontal != 0){
            run = true;
        }
        else{
            run = false;
        }
        anim.SetBool("Run", run);

        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z); // Sem inversão horizontal
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z); // Inversão horizontal para trás
        }

        Jump();

        isGrounded = Physics2D.OverlapBox(groundCheck.position, areaBoxGround, 0, groundLayer);
         if(isGrounded){
            pulo = false;
        }
        else{
            pulo = true;
        }
        anim.SetBool("Pulo", pulo);
    }

    private void Jump(){
        isGrounded = Physics2D.OverlapBox(groundCheck.position, areaBoxGround, 0, groundLayer);
        if(isGrounded && Input.GetButtonDown("Jump")){
            // rb.velocity = Vector2.up * jumpForce;
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode2D.Impulse);
        }

       
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 movePosition = new Vector3(horizontal, 0f, 0f);
        transform.position += movePosition* Time.deltaTime * speed;
    }
}
