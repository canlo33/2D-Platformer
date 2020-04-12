using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int currentHealth;
    public int maxHealth;
    public bool isEnraged = false;
    public bool isInvulnerable = false;


    private void Start()
    {
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
        isEnraged = true;
        
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

}
