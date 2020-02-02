using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameActor : MonoBehaviour
{
    private float regenerationTimer;
    private float maxHealth;
    private float maxMana;
    public float health;
    public float mana;

    public void setMaxHealth(float value)
    {
        maxHealth = value;
    }

    public void setMaxMana(float value)
    {
        maxMana = value;
    }

    public float getHealth()
    {
        return health;
    }

    public void setHealth(float value)
    {
        health = value;
    }

    public float getMana()
    {
        return mana;
    }

    public void setMana(float value)
    {
        mana = value;
    }

    public bool damagable()
    {
        Ghostable ghostableComponent = GetComponent<Ghostable>();
        if (ghostableComponent) return ghostableComponent.remainingGhostDuration == 0;
        else return true;
    }

    // Update is called once per frame
    void Update()
    {
        // Incrementing regeneration timer
        regenerationTimer += Time.deltaTime;

        // Checking regeneration
        if(regenerationTimer > GameplayController.getGameplayOptions().regenerationPeriod)
        {
            // Reset timer
            regenerationTimer = 0.0f;

            // Regenerating resources
            health = Math.Min(health + GameplayController.getGameplayOptions().healthRegenerationValue, maxHealth);
            mana   = Math.Min(mana + GameplayController.getGameplayOptions().manaRegenerationValue, maxMana);
        }
    }
}
