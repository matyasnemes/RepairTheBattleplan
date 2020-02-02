using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameActor : MonoBehaviour
{
    private float regenerationTimer;
    private float maxHealth;
    private float maxMana;
    private Animator animator;
    private float animationTimer;
    private bool animating;
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

    public void doIdleAnimation()
    {
        animator.SetInteger("state", 0);
    }

    public void doMoveAnimation()
    {
        animator.SetInteger("state", 1);
        animationTimer = GameplayController.getGameplayOptions().playerMovePeriod;
    }

    public void doHitAnimation()
    {
        animator.SetInteger("state", 2);
    }

    public void doCastAnimation()
    {
        animator.SetInteger("state", 2);
        animationTimer = GameplayController.getGameplayOptions().playerCastPeriod;
    }

    public void doDeathAnimation()
    {
        animator.SetInteger("state", 3);
    }

    void Start()
    {
        // Getting animator component
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checking death condition
        if(health == 0)
        {
            doDeathAnimation();
            return;
        }

        // Decrementing animation timer
        animationTimer -= Time.deltaTime;
        if(animationTimer <= 0)
        {
            doIdleAnimation();
        }

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
