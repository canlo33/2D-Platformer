using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCapsule : MonoBehaviour
{    
    private GameObject player;
    private HealthSystem healthSystem;
    public int healAmount;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //healthSystem = player.GetComponent<HealthSystem>().healthSystem;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.tag == "Player" && healthSystem.GetHealth() != healthSystem.maxHealth)
        //{
        //    healthSystem.Heal(healAmount);
        //    // Particles Effects Here //
        //    Destroy(gameObject);


        //}
    }

}
