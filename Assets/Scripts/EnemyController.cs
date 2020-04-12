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
    private Rigidbody2D rb2D;
    public GameObject bloodEffect;
    public float attackCoolDown;
    private float attackCoolDownTimer;
    public Vector3 offset;
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public float StoppingDistance;
    public float detectionRange;
    private Transform player;
    private bool goRight = true;
    private bool playerInRange = false;
    private bool attack = false;
    public bool isHurt = false;
    private HealthSystem enemyHealthSystem;

    void Start()
    {
        startPosition = transform.position;
        rightBorderPosition = startPosition + offset;
        leftBorderPosition = startPosition - offset;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        scale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        attackCoolDownTimer = attackCoolDown;
        enemyHealthSystem = gameObject.GetComponent<HealthSystem>();
           
    }

    void Update()
    {
        VelocityCalculator();
        Rotation();
        animator.SetFloat("walk", Math.Abs(rb2D.velocity.x));        
        Die();
        
    }
    private void FixedUpdate()
    {        
        Patrol();
        ChaseAndAttack();
    }


    void Patrol()
    {
        if (goRight && !playerInRange)
        {
            rb2D.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb2D.velocity.y);
                        
            if ((transform.position.x - rightBorderPosition.x) >= 1f)
            {
                goRight = false;
            }
        }
        if (!goRight && !playerInRange)
        {
            rb2D.velocity = new Vector2(-walkSpeed * Time.fixedDeltaTime, rb2D.velocity.y);

            if ((transform.position.x - leftBorderPosition.x) <= 1f)
            {
                goRight = true;
            }
        }
        
    }



    void ChaseAndAttack()
    {
        if (Math.Abs(transform.position.x - player.position.x) > StoppingDistance + 1f)
        {
            attack = false;
            attackCoolDownTimer = attackCoolDown;            
        }

        if (Math.Abs(transform.position.x - player.position.x) > detectionRange)
        {
            playerInRange = false;
            attack = false;            
        }

        if ((!attack && (Math.Abs(transform.position.x - player.position.x)) <= detectionRange ) || enemyHealthSystem.isEnraged)
        {
            playerInRange = true;            

            if (player.position.x > transform.position.x)
            {
                rb2D.velocity = new Vector2(walkSpeed * 3f * Time.fixedDeltaTime, rb2D.velocity.y);
            }
            if (player.position.x < transform.position.x)
            {
                rb2D.velocity = new Vector2(-walkSpeed * 3f * Time.fixedDeltaTime, rb2D.velocity.y);
            }
        }

        if (Math.Abs(transform.position.x - player.position.x) <= StoppingDistance)
        {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
            attack = true;
            attackCoolDownTimer -= Time.fixedDeltaTime;
            if (attackCoolDownTimer <= 0 && !isHurt)
            {
                animator.SetTrigger("attack");
                attackCoolDownTimer = attackCoolDown;
            }
        }


    }


    void Die()
    {
        if (enemyHealthSystem.GetHealth() <= 0)
        {
            Instantiate(bloodEffect, transform.position, transform.rotation);
            Destroy(gameObject);
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
    }

}
