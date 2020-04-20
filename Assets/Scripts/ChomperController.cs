using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperController : MonoBehaviour
{
    //Positions
    private Vector2 startPosition;
    private Vector2 rightBorderPosition;
    private Vector2 leftBorderPosition;
    public Vector2 patrolOffset;
    private Transform player;    
    
    // Components
    private Animator animator;
    private Rigidbody2D rb2D;
    private HealthSystem chomperHealthSystem;
    
    //Movement
    public float walkSpeed;
    public float lookAroundTime;
    private bool goRight = true;
    public float detectionRange;
    public float stoppingRange;
    private float lookAroundTimeReset;
    private bool isLookingAround = false;
    private Vector3 startScale;


    void Start()
    {
        startPosition = transform.position;
        leftBorderPosition = startPosition - patrolOffset;
        rightBorderPosition = startPosition + patrolOffset;
        player = GameObject.Find("Player").transform;
        rb2D = GetComponent<Rigidbody2D>();
        chomperHealthSystem = GetComponent<HealthSystem>();
        animator = GetComponent<Animator>();
        startScale = transform.localScale;
        lookAroundTimeReset = lookAroundTime;
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetFloat("walk", Mathf.Abs(rb2D.velocity.x));
        Flip();
        Patrol();        
        Attack();
    }

    void Patrol()
    {
        if (IsPlayerDetected())
            return;

        if(isLookingAround)
        {
            lookAroundTime -= Time.fixedDeltaTime;
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
            if (lookAroundTime <= 0f)
            {
                isLookingAround = false;
                lookAroundTime = lookAroundTimeReset;

            }
            return;
        }        

        if(goRight)
        {
            rb2D.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb2D.velocity.y);
        }
        else if(!goRight) 
            rb2D.velocity = new Vector2(-walkSpeed * Time.fixedDeltaTime, rb2D.velocity.y);

        if(goRight && transform.position.x >= rightBorderPosition.x)
        {
            goRight = false;
            isLookingAround = true;
        }

        else if (!goRight && transform.position.x <= leftBorderPosition.x)
        {
            goRight = true;
            isLookingAround = true;
        }
    }

    private void Attack()
    {
        if (IsPlayerInStoppingDistance())
        {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);            
            animator.SetBool("attack", true);
        }

        else if (IsPlayerDetected() && !IsPlayerInStoppingDistance() || chomperHealthSystem.isEnraged)
        {            

            if (player.position.x > transform.position.x)
                rb2D.velocity = new Vector2(walkSpeed * 2f* Time.fixedDeltaTime, rb2D.velocity.y);
            else
                rb2D.velocity = new Vector2(-walkSpeed * 2f * Time.fixedDeltaTime, rb2D.velocity.y);
        }

    }

    void Flip()
    {
        if (rb2D.velocity.x > 2f)
            transform.localScale = startScale;
        

        else if (rb2D.velocity.x < -2f)
            transform.localScale = new Vector3(-startScale.x, startScale.y, startScale.z);            
    }

    private bool IsPlayerDetected()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) <= detectionRange)
            return true;
        else return false;
    }

    private bool IsPlayerInStoppingDistance()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) <= stoppingRange)
            return true;
        else
        {
            animator.SetBool("attack", false);
            return false;
        }

    }
    
   

   
}
