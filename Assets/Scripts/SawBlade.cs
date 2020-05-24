using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{

    public float rotationSpeed;
    public float pushBackSpeed;
    public int damageAmount;
    private GameObject player;
    private HealthSystem playerHealthSystem;
    private Rigidbody2D rb2D;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerHealthSystem = player.GetComponent<HealthSystem>();
        rb2D = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealthSystem.Damage(damageAmount);
        }
    }
}
