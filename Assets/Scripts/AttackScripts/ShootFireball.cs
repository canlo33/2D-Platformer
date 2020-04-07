using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireball : MonoBehaviour
{
    public float speed;
    public int damage;
    private HealthSystem enemyHealthSystem;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            if(collision.tag == "Enemy")
            {
                enemyHealthSystem = collision.GetComponent<HealthSystem>().healthSystem;
                enemyHealthSystem.Damage(damage);
            }
            Destroy(gameObject);
        }
            
    }


}
