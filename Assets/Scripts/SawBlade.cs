using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{

    public float rotationSpeed;
    public int damageAmount;
    private GameObject player;
    private HealthSystem playerHealthSystem;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerHealthSystem = player.GetComponent<HealthSystem>();
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
