using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float comboTimer;
    private float phoenixTimer;
    public float phoenixTimerReset;
    private int clickCounter;
    private float horizontalMove;
    private float verticalMove;
    public float moveSpeed;
    public float jumpSpeed;
    private Rigidbody2D rigidbody;
    public BoxCollider2D slashAttackCollider;
    public BoxCollider2D chainAttackCollider;
    private bool isJumping;
    private bool phoenix;


    void Start()
    {        
        animator = GetComponent<Animator>();
        slashAttackCollider.enabled = false;
        chainAttackCollider.enabled = false;
        clickCounter = 0;
        comboTimer = -1f;
        phoenixTimerReset = 10f;
        phoenixTimer = 0f;
        isJumping = false;
        phoenix = false;
        rigidbody = GetComponent<Rigidbody2D>();     
        
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("run", Mathf.Abs(horizontalMove));
        CoolDowns();        

    }

    private void FixedUpdate()
    {
        if(!phoenix)
        {
            Run();
            Attack();
            Jump();
        } 
    }

    void CoolDowns()
    {
        comboTimer -= Time.deltaTime;
        phoenixTimer -= Time.deltaTime;
        if (comboTimer <= 0f)
        {
            clickCounter = 0;
        }
        if (phoenixTimer <= 0f)
        {
            phoenixTimer = 0;
        }
    }

    void ResetAnimationParameters()
    {
        animator.SetBool("jump", false);       
        animator.SetBool("slash", false);
        animator.SetBool("chain", false);
        animator.SetBool("phoenix", false);
        slashAttackCollider.enabled = false;
        transform.rotation = Quaternion.identity;
        rigidbody.isKinematic = false;
        phoenix = false;

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
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isJumping && phoenixTimer <= 0f)
        {
            phoenix = true;
            rigidbody.isKinematic = true;
            animator.SetBool("phoenix", true);
            phoenixTimer = phoenixTimerReset;
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
        if(!isJumping && Input.GetButton("Jump"))
        {
            isJumping = true;
            animator.SetBool("jump", true);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed * Time.fixedDeltaTime);
        }
        if(rigidbody.velocity.y <= -3f)
        {
            animator.SetBool("jump", false);
            
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            ResetAnimationParameters();
            isJumping = false;
            animator.SetBool("jump", false);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isJumping = true;
        }
    }



}
