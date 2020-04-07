using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int currentHealth;
    public int maxHealth;
    public bool isInvulnerable = false;
    public HealthSystem healthSystem;

    public HealthSystem(int maxHealth)
    {
   
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        
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
        if (currentHealth < 0) currentHealth = 0;
        
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    private void Start()
    {
        this.healthSystem = new HealthSystem(maxHealth);
    }

}
