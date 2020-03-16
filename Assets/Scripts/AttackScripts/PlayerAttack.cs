using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damageAmount;
    private EnemyController enemyController;
    private PlayerController playerController;

    private void Start()
    {
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            enemyController = collision.GetComponent<EnemyController>();
            enemyController.enemyHealth.Damage(damageAmount);
            enemyController.isHurt = true;
            playerController.ShakeCam();            
       
        }
    }


}
