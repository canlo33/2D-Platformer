using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject player;
    private EnemyController self;
    private PlayerController playerController;
    public int damageAmount;

    // Start is called before the first frame update
    void Start()
    {
        self = gameObject.GetComponent<EnemyController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {            
            playerController.playerHealth.Damage(damageAmount);    
            
        }
    }
}
