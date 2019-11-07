using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public float comboTimer;
    public int clickCounter;
    private float horizontalMove;
    private float verticalMove;
    public float moveSpeed;
    public float jumpSpeed;
    private Rigidbody2D rigidbody;
    public BoxCollider2D slashAttackCollider;
    public BoxCollider2D chainAttackCollider;
    public bool isJumping;
    private bool phoenix;


    void Start()
    {        
        animator = GetComponent<Animator>();
        slashAttackCollider.enabled = false;
        chainAttackCollider.enabled = false;
        clickCounter = 0;
        comboTimer = -1f;
        isJumping = false;
        rigidbody = GetComponent<Rigidbody2D>();     
        
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("run", Mathf.Abs(horizontalMove));     
        comboTimer -= Time.deltaTime;
        if(comboTimer <= 0f)
        {
            clickCounter = 0;
        }
        
    }

    private void FixedUpdate()
    {
        Run();
        Attack();
        Jump();
    }

    void ResetAnimationParameters()
    {
        animator.SetBool("jump", false);       
        animator.SetBool("combo", false);
        animator.SetBool("slash", false);
        animator.SetBool("chain", false);
        animator.SetBool("phoenix", false);
        slashAttackCollider.enabled = false;
        transform.rotation = Quaternion.identity;
        rigidbody.isKinematic = false;

    }

    void Attack()
    {
        //Slash Attack
        if (Input.GetMouseButtonDown(0) && !isJumping && clickCounter == 0)
        {
            animator.SetBool("slash", true);
            clickCounter++;
            comboTimer = .90f;
        }
        //Combo Chain Attack
        else if (Input.GetMouseButtonDown(0) && !isJumping && clickCounter == 1 && comboTimer >= 0f && animator.GetFloat("run") < 0.01)
        {
            clickCounter--;
            animator.SetBool("slash", false);
            animator.SetBool("chain", true);

        }
        if (Input.GetMouseButtonDown(0) && isJumping)
        {
            animator.SetBool("slash", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isJumping)
        {
            //animator.SetBool("phoenix", true);
            rigidbody.isKinematic = true;
            animator.Play("PhoenixDive");

        }

    }

    private void Run()
    {
        transform.Translate(horizontalMove * Time.fixedDeltaTime, 0f, 0f);
        if (horizontalMove > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);


        }
        else if (horizontalMove < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

        }
        

    }

    private void Jump()
    {
        if(Input.GetButton("Jump") && !isJumping)
        {
            isJumping = true;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed * Time.fixedDeltaTime);
            animator.SetBool("jump", true);

        }
        if(rigidbody.velocity.y <= 0)
        {
            animator.SetBool("jump", false);
        }       
        
    }


    void PhoenixDive()
    {
        if(phoenix)
        {

        }
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name.Contains("Ground"))
        {
            ResetAnimationParameters();
            isJumping = false;
            animator.SetBool("jump", false);
        }

    }




}
