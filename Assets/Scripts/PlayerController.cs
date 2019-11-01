using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private bool canCombo;
    public float comboTimer;
    public int clickCounter;
    private float horizontalMove;
    private float verticalMove;
    public float moveSpeed;
    public float jumpSpeed;
    private Rigidbody2D rigidbody;
    private bool isJumping;


    void Start()
    {
        animator = GetComponent<Animator>();
        clickCounter = 0;
        comboTimer = -1f;
        canCombo = false;
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
        animator.SetBool("run", false);
        animator.SetBool("combo", false);
        animator.SetBool("slash", false);
        animator.SetBool("chain", false);
    }

    void Attack()
    {
        if (Input.GetMouseButtonUp(0) && !isJumping && clickCounter == 0)
        {
            animator.SetBool("slash", true);
            clickCounter++;
            comboTimer = .90f;
        }
        else if (Input.GetMouseButtonDown(0) && !isJumping && clickCounter == 1 && comboTimer >= 0f && animator.GetFloat("run") < 0.01)
        {
            clickCounter--;
            animator.SetBool("slash", false);
            animator.SetBool("chain", true);

        }
    } 

    private void Run()
    {
        if(horizontalMove > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            
        }
        else if (horizontalMove < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;

        }
        transform.Translate(horizontalMove * Time.fixedDeltaTime, 0f, 0f);

    }

    private void Jump()
    {
        if(Input.GetButton("Jump") && !isJumping)
        {
            rigidbody.velocity = Vector2.up * jumpSpeed;
            animator.SetBool("jump", true);
            isJumping = true;
        }
        if(rigidbody.velocity.y <= 0)
        {
            animator.SetBool("jump", false);
            
        }       
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ResetAnimationParameters();
        isJumping = false;
    }





}
