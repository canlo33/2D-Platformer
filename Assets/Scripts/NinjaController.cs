using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    //Components
    private Animator animator;
    private Rigidbody2D rb2D;
    public Transform groundCheck;

    [SerializeField] private LayerMask groundLayer;
    //Movement Related
    private float horizontalMove;
    public float moveSpeed;
    public float jumpSpeed;
    public float gravityScale;
    public float groundCheckRadius;
    private bool isGrounded;
    private bool isRunning;

    //FireBall 
    public GameObject fireBall;
    private Transform firePoint;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        firePoint = GameObject.Find("FirePoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("run", Mathf.Abs(horizontalMove));
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);   
        GravityScale();
        Jump();
        Run();
        Attack();
 

    }

    private void Run()
    {
        //transform.Translate(horizontalMove * Time.deltaTime, 0f, 0f);
        rb2D.velocity = new Vector2(horizontalMove * Time.deltaTime, rb2D.velocity.y);
        
        if (horizontalMove > 0 && transform.rotation.y < -0.1f)
        {
            //transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            transform.Rotate(0f, 180f, 0f);
           

        }
        else if (horizontalMove < 0 && transform.rotation.y > -0.1f)
        {
            //transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            transform.Rotate(0f, 180f, 0f);
        }
        if (Mathf.Abs(horizontalMove) > 0)
        {
            isRunning = true;
        }
        else isRunning = false;

    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb2D.velocity = Vector2.up * jumpSpeed;
            animator.SetTrigger("jump");    
            
        }

    }

    private void GravityScale()
    {
        if (rb2D.velocity.y <= -5f)
        {
            rb2D.gravityScale = 23f;
        }
        else rb2D.gravityScale = 10f;

    }

    private void Attack()
    {
        // Slash Attack
        if (isGrounded && Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("slash");
        }
        // Jump Slash
        if (!isGrounded && Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("jumpSlash"); 
        }
        //Fireball
        if(isGrounded && Input.GetKeyDown(KeyCode.X) && !isRunning)
        {
            animator.SetTrigger("fireball");
        }
        //Jump Fireball
        if (!isGrounded && Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("jumpFireball");
        }

    }


    private void FireBall()
    {
        GameObject fireBallObject = Instantiate(fireBall);
        fireBallObject.transform.position = firePoint.position;
        fireBallObject.transform.rotation = firePoint.rotation;
        //fireBallObject.GetComponent<Rigidbody2D>().velocity =
    }
   
}
