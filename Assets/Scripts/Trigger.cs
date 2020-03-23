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
            Debug.Log(collision.gameObject.name + " Entered");
        }
        if (collision.gameObject.name == "PushableBox")
        {
            animator.SetBool("triggered", true);
            platformScript.isMoving = true;
            boxOn = true;
            Debug.Log(collision.gameObject.name + " Entered");
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOn = false;
            Debug.Log(collision.gameObject.name + " Exited");
        }
        if (collision.gameObject.name == "PushableBox")
        {
            boxOn = false;
            Debug.Log(collision.gameObject.name + " Exited");
        }
        if(!playerOn && !boxOn)
        {
            animator.SetBool("triggered", false);
            platformScript.isMoving = false;
            
        }

    }

   


}
