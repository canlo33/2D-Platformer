using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBridge : MonoBehaviour
{
    private GameObject player;
    private bool moveRight = false;
    public bool isMoving = false;
    public float moveSpeed;
    public float moveRange;
    private float rightBorder;
    private float leftBorder;    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rightBorder = transform.position.x + moveRange;
        leftBorder = transform.position.x;
    }

    private void Update()
    {
        Move();
        CheckBorder();
    }


    void Move()
    {
        if(isMoving)
        {
            if(moveRight)
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            if(!moveRight)
            {
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
        }
    }

    void CheckBorder()
    {
        if(rightBorder - transform.position.x <= .25f)
        {
            moveRight = false;
        }
        if (transform.position.x - leftBorder <= .25f)
        {
            moveRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.transform.parent = null;
        }
    }



}
