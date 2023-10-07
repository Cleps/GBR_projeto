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
    private LayerMask groundLayer;
    [SerializeField]
    private Transform wallCheckRight;
    private float groundedTimer;
    private bool isGrounded;
    [SerializeField]
    private Transform groundCheck;
    private bool isWallF;
    [SerializeField]
    new Vector2 areaBoxGround;
    [SerializeField]
    new Vector2 areaBoxWall;
    [SerializeField]
    private float dashForce;



    private Animator anim;
    private SpriteRenderer spriteRenderer; // Referência ao SpriteRenderer do personagem

    private void Awake()
    {
        Application.targetFrameRate = 60; // fps
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        Time.timeScale = 1f; // velocidade

        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0){
            anim.SetBool("idle", false);
        }else{
            anim.SetBool("idle", true);
        }
        if (horizontal > 0)
        {
            //spriteRenderer.flipX = false; // Sem inversão horizontal
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z); // inverter o proprio objeto
        }
        else if (horizontal < 0)
        {
            //spriteRenderer.flipX = true; // Inversão horizontal para trás
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }

        isGrounded = Physics2D.OverlapBox(groundCheck.position, areaBoxGround, 0, groundLayer);
        if (isGrounded){
            anim.SetBool("nochao", true);
            groundedTimer += Time.deltaTime;
        }else{
            anim.SetBool("nochao", false);
            groundedTimer = 0f;
        }

        if (groundedTimer >= 0.05f){
            // ai sim pode trocar a animação
            anim.SetBool("mudarpraidle", true);
        }else{
            anim.SetBool("mudarpraidle", false);
        }
         // parte do pulo
        Jump();

        Dash();
        
        // if (isWallL && horizontal < 0){
        //     //print("colidiu na esquerda");
        // }else if (isWallR && horizontal > 0){
        //     //print("colidiu na direita");
        // }
        // else{
            
        // }
        
        // pro player para de correr na parede
        isWallF = Physics2D.OverlapBox(wallCheckRight.position, areaBoxWall, 0, groundLayer); 
        if (!(horizontal < 0) && !isWallF){ // obrigatorio ter 2 ifs
            Move();
            }
        else if (!(horizontal > 0) && !isWallF){
            Move();
        }

    }

    private void Jump(){

        isGrounded = Physics2D.OverlapBox(groundCheck.position, areaBoxGround, 0, groundLayer);
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

    private void Dash(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        //if (Input.GetButtonDown("left shift")){
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded){
            rb.AddForce(new Vector3(dashForce * horizontal, 0f, 0f), ForceMode2D.Impulse);
        }
        
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, areaBoxGround);
        Gizmos.DrawWireCube(wallCheckRight.position, areaBoxWall);
    }

}