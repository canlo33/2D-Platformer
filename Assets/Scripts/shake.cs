using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!transform.name.Contains("grass") && (collision.transform.tag == "Player" || collision.transform.tag == "Attack") )
        {        
            animator.SetBool("shake", true);
        }
        else if (transform.name.Contains("grass") && (collision.transform.tag == "Player" || collision.transform.tag == "Attack") )
        {
            animator.SetBool("grass", true);
        }
           

    }

    void ResetParameter()
    {
        animator.SetBool("shake", false);
        animator.SetBool("grass", false);
    }
}
