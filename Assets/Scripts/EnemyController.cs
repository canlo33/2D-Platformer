using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector2 rightBorderPosition;
    private Vector2 leftBorderPosition;
    private Vector3 previousPosition;
    private Vector3 scale;
    private Vector3 currentFrameVelocity;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigidbody;
    public HealthSystem enemyHealth;
    public float attackCoolDown;
    private float attackCoolDownTimer;
    public Vector3 offset;
    public int health;
    public float walkSpeed;
    public float StoppingDistance;
    public float detectionRange;
    private Transform player;
    public bool goRight = true;
    public bool playerInRange = false;
    private bool attack = false;

    void Start()
    {
        startPosition = transform.position;
        rightBorderPosition = startPosition + offset;
        leftBorderPosition = startPosition - offset;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        enemyHealth = new HealthSystem(health);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackCoolDownTimer = attackCoolDown;
        Debug.Log(rightBorderPosition);
    }

    void Update()
    {
        VelocityCalculator();
        Rotation();
        animator.SetFloat("walk", Math.Abs(currentFrameVelocity.x));
        Patrol();
        ChaseAndAttack();
        attackCoolDownTimer -= Time.deltaTime;
                
    }


    void Patrol()
    {
        if (goRight && !playerInRange)
        {
            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
            
            if ((transform.position.x - rightBorderPosition.x) >= 1f)
            {
                goRight = false;
            }
        }
        if (!goRight && !playerInRange)
        {
            transform.Translate(Vector2.left * walkSpeed * Time.deltaTime);
            
            if ((transform.position.x - leftBorderPosition.x) <= 1f)
            {
                goRight = true;
            }
        }
        
    }

    void ChaseAndAttack()
    {
        if(!attack &&(Math.Abs(transform.position.x - player.position.x)) <= detectionRange)
        {
            playerInRange = true;
            transform.position = Vector2.MoveTowards(transform.position, player.position, walkSpeed * 2f * Time.deltaTime);
        }
        if (Math.Abs(transform.position.x - player.position.x) > detectionRange)
        {
            playerInRange = false;
            attack = false;
        }
        if(Math.Abs(transform.position.x - player.position.x) <= StoppingDistance)
        {            
            attack = true;
            if(attackCoolDownTimer <= 0)
            {
                animator.SetBool("attack", true);
                attackCoolDownTimer = attackCoolDown;
            }
            
        }
        if (Math.Abs(transform.position.x - player.position.x) > StoppingDistance)
        {
            attack = false;
        }


    }

    void VelocityCalculator()
    {
        currentFrameVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;  
    }

    void Rotation()
    {
        if (currentFrameVelocity.x < -5f && !attack)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);    
        }
        else if (currentFrameVelocity.x > 5f && !attack)
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        else if(currentFrameVelocity.x == 0 && attack)
        {
            if(player.position.x - transform.position.x > 5f)
            {
                transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            }
            else if (player.position.x - transform.position.x < -5f)
            {
                transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }
        }
    }

    void ResetAnimationParameters()
    {
        animator.SetBool("attack", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
    }

}
