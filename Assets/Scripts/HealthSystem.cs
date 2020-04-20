using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int currentHealth;
    public int maxHealth;
    public bool isEnraged = false;
    public bool isInvulnerable = false;
    public GameObject hurtParticle;
    private Animator animator;


    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void Damage(int damageAmount)
    {
        if (isInvulnerable)
            return;

        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        { 
            currentHealth = 0;
            animator.SetTrigger("die");            
        }

        isEnraged = true;
        if(gameObject.layer == 15)
        {
            GameObject go = Instantiate(hurtParticle);
            go.transform.position = transform.position + new Vector3(0f, transform.localScale.y, 0f);

        }
        
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

}
