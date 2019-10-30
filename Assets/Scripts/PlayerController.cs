using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private bool canCombo;
    private int clickCounter;
 

    void Start()
    {
        animator = GetComponent<Animator>();
        clickCounter = 0;
        canCombo = false;
        
    }

    void Update()
    {
        Attack();
        Run();
    }

    void ResetAnimationParameters()
    {
        animator.SetBool("jump", false);
        animator.SetBool("idle", false);        
        animator.SetBool("walk", false);
        animator.SetBool("attack", false);
        animator.SetBool("attack1", false);
        animator.SetBool("attack2", false);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            ResetAnimationParameters();
            animator.SetBool("attack1", true);
            
        }

        else if (Input.GetMouseButtonDown(0))
        {
            ResetAnimationParameters();
            animator.SetBool("attack", true);
            
        }

        
    }

    void Run()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            ResetAnimationParameters();
            animator.Play("Run");   
            
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            ResetAnimationParameters();
            animator.Play("Run");
        }
        else
        {
            ResetAnimationParameters();
            animator.Play("Idle");
        }

    }

    public void Idle()
    {
        ResetAnimationParameters();
        animator.SetBool("idle", true);
    }
      


}
