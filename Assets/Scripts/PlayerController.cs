using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rigidbody;
    private Transform particlePosition;
    private float comboTimer;
    private float phoenixTimer;
    public float phoenixTimerReset;
    private int clickCounter;
    private float horizontalMove;
    public float moveSpeed;
    public float jumpSpeed;
    public GameObject dustParticle;
    public GameObject runParticle;
    public BoxCollider2D slashAttackCollider;
    public BoxCollider2D chainAttackCollider;
    private int jumpCount = 2;
    private bool phoenix;
    private bool spawnParticle = false;


    void Start()
    {        
        animator = GetComponent<Animator>();
        particlePosition = transform.Find("ParticlePosition").GetComponent<Transform>();
        slashAttackCollider.enabled = false;
        chainAttackCollider.enabled = false;
        clickCounter = 0;
        comboTimer = -1f;
        phoenixTimerReset = 10f;
        phoenixTimer = 0f;    
        phoenix = false;
        rigidbody = GetComponent<Rigidbody2D>();

    }


    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("run", Mathf.Abs(horizontalMove));
        CoolDowns();
        GravityScale();
        Jump();
    }

    private void FixedUpdate()
    {
        if(!phoenix)
        {
            Run();
            Attack();            
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
        animator.SetBool("doubleJump", false);
        animator.SetBool("slash", false);
        animator.SetBool("chain", false);
        animator.SetBool("phoenix", false);
        slashAttackCollider.enabled = false;
        transform.rotation = Quaternion.identity;
        rigidbody.isKinematic = false;
        phoenix = false;
    }

    void CreateRunParticles()
    {
        Instantiate(runParticle, particlePosition.position, particlePosition.rotation);
    }

    void ShakeCam()
    {
        GameMaster.gameMaster.StartCoroutine(GameMaster.gameMaster.ShakeCamera(15, .25f));
    }
       

    void Attack()
    {
        //Slash Attack
        if (Input.GetMouseButtonDown(0) && jumpCount > 0 && clickCounter == 0)
        {
            animator.SetBool("slash", true);
            clickCounter++;
            comboTimer = .90f;
        }
        //Combo Chain Attack
        else if (Input.GetMouseButtonDown(0) && jumpCount == 2 && clickCounter == 1 && comboTimer >= 0f && animator.GetFloat("run") < 0.01)
        {
            clickCounter--;
            animator.SetBool("slash", false);
            animator.SetBool("chain", true);

        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && jumpCount == 2 && phoenixTimer <= 0f)
        {
            phoenix = true;
            rigidbody.isKinematic = true;            
            phoenixTimer = phoenixTimerReset;
            animator.SetBool("phoenix", true); 
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
        if (jumpCount > 0 && Input.GetButtonDown("Jump") && !phoenix)
        {
            
            if (jumpCount == 2)
            {
                animator.SetBool("jump", true);
            }
            else if (jumpCount == 1)
            {
                animator.SetBool("jump", false);
                animator.SetBool("doubleJump", true);
            }
            jumpCount--;
            rigidbody.velocity = Vector2.up * jumpSpeed;
            spawnParticle = true;

        } 


    }
    
    private void GravityScale()
    {
        if (rigidbody.velocity.y <= -5f)
        {
            rigidbody.gravityScale = 23f;                       
        }
        else rigidbody.gravityScale = 10f;

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            if(spawnParticle)
            {
                Instantiate(dustParticle, particlePosition.position, particlePosition.rotation);
                if(jumpCount == 0)
                {
                    GameMaster.gameMaster.StartCoroutine(GameMaster.gameMaster.ShakeCamera(10, .25f));
                }                
            }
            ResetAnimationParameters();
            jumpCount = 2;
            spawnParticle = false;
        }

    }


}
