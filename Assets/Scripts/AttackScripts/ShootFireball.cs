using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireball : MonoBehaviour
{
    public float speed;
    public int damage;
    public GameObject explosion;
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
                enemyHealthSystem = collision.GetComponent<HealthSystem>();
                enemyHealthSystem.Damage(damage);
            }
            GameObject boom = Instantiate(explosion);
            boom.transform.position = transform.position;
            Destroy(gameObject);
        }
            
    }


}
