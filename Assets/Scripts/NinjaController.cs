using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    //Intro Animation Check
    public bool isIntro = true;

    //Components
    private Animator animator;
    private Rigidbody2D rb2D;
    public Transform groundCheck;

    [SerializeField] private LayerMask groundLayer;
    //Movement Related
    private float horizontalMove;
    public float moveSpeed;
    public float jumpSpeed;
    public float groundCheckRadius;
    private bool isGrounded;
    private bool isRunning;

    //FireBall 
    public GameObject fireBall;
    private Transform firePoint;
    public float fireCD;
    private float fireCoolDown;

    //Strike Combo
    public float strikeCT;
    private float strikeComboTimer;
    public int slashDamage;
    public int strikeDamage;

    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        firePoint = GameObject.Find("FirePoint").transform;
        fireCoolDown = fireCD;
        strikeComboTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIntro) return;

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("run", Mathf.Abs(horizontalMove));
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        Timers();
        GravityScale();        
        Attack();
        PlayerHurt();
        Jump();

    }
    void Timers()
    {
        fireCoolDown -= Time.deltaTime;
        if (fireCoolDown < 0)
        {
            fireCoolDown = 0f;
        }

        strikeComboTimer -= Time.deltaTime;
        if (strikeComboTimer < 0)
        {
            strikeComboTimer = 0f;
        }

    }

    private void FixedUpdate()
    {
        if (isIntro) return;
        Run();

    }

    private void Run()
    {

        rb2D.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime, rb2D.velocity.y);
        
        if ((horizontalMove > 0 && transform.rotation.y < -0.1f) || (horizontalMove < 0 && transform.rotation.y > -0.1f))
        {
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
        if (rb2D.velocity.y <= 0f)
        {
            rb2D.gravityScale = 53f;
        }
        else rb2D.gravityScale = 10f;

    }

    private void Attack()
    {
        // Slash Attack
        if (isGrounded && Input.GetKeyDown(KeyCode.K) && strikeComboTimer <= 0.01f)
        {
            animator.SetTrigger("slash");
            strikeComboTimer = strikeCT;
        }
        //Strike Combo
        else if (isGrounded && Input.GetKeyDown(KeyCode.K) && strikeComboTimer > 0.01f)
        {
            animator.SetTrigger("strike");
            strikeComboTimer = 0f;
        }
        // Jump Slash
        if (!isGrounded && Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("jumpSlash"); 
        }
        //Fireball
        if(isGrounded && Input.GetKeyDown(KeyCode.X) && !isRunning && fireCoolDown <= 0)
        {
            animator.SetTrigger("fireball");
            fireCoolDown = fireCD;
        }
        //Jump Fireball
        if (!isGrounded && Input.GetKeyDown(KeyCode.X) && fireCoolDown <= 0)
        {
            animator.SetTrigger("jumpFireball");
            fireCoolDown = fireCD;
        }

    }

    private void PlayerHurt()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("hurt");
        }
    }

    private void FireBall()
    {
        GameObject fireBallObject = Instantiate(fireBall);
        fireBallObject.transform.position = firePoint.position;
        fireBallObject.transform.rotation = firePoint.rotation;       
    }
   
}
