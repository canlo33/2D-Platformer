using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject platform;
    private MovingBridge platformScript;
    private Animator animator;
    private bool playerOn = false;
    private bool boxOn = false;

    void Start()
    {
        platformScript = platform.GetComponent<MovingBridge>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("triggered", true);
            platformScript.isMoving = true;
            playerOn = true;
        }
        if (collision.gameObject.name == "PushableBox")
        {
            animator.SetBool("triggered", true);
            platformScript.isMoving = true;
            boxOn = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOn = false;
        }
        if (collision.gameObject.name == "PushableBox")
        {
            boxOn = false;

        }
        if(!playerOn && !boxOn)
        {
            animator.SetBool("triggered", false);
            platformScript.isMoving = false;
            
        }

    }

   


}
