using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject platform;
    private MovingBridge platformScript;
    private Animator animator;

    void Start()
    {
        platformScript = platform.GetComponent<MovingBridge>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("triggered", true);
        platformScript.isMoving = true;
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("triggered", false);
        platformScript.isMoving = false;
    }



}
