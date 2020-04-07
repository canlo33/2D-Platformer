using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private int slashDamage;
    private int strikeDamage;
    private GameObject player;
    private Animator playerAnimator;
    private HealthSystem enemyHealthSystem;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        slashDamage = GetComponentInParent<NinjaController>().slashDamage;
        strikeDamage = GetComponentInParent<NinjaController>().strikeDamage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" || collision.tag == "Boss")
        {
            enemyHealthSystem = collision.GetComponent<HealthSystem>().healthSystem;

            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ninja_Attack1"))
            {
                enemyHealthSystem.Damage(slashDamage);
            }
            else if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ninja_Strike"))
            {
               enemyHealthSystem.Damage(strikeDamage);

            }
        }  

    }

}
