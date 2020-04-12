using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount;
    public GameObject player;
    public HealthSystem playerHealthSystem;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealthSystem = player.GetComponent<HealthSystem>();
            playerHealthSystem.Damage(damageAmount);           
        }
    }
}
