using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Healable : MonoBehaviour
{
    public GameObject healEffect;

    // Click handler for action
    public void OnMouseDown()
    {
        int manaCost = GameplayController.getGameplayOptions().healManaCost;
        int healValue = GameplayController.getGameplayOptions().healValue;
        GameActor player = GameplayController.getPlayer();

        // Checking if action should be performed
        if (GameplayController.getCurrentAction() == GameplayController.PlayerAction.HEAL_ACTION)
        {
            // Checking available mana and health condition
            if (player.getMana() < manaCost || player.getHealth() < 0.1f) return;

            // Starting animation
            player.doCastAnimation();

            // Increasing actor health
            GameActor actor = GetComponentInParent<GameActor>();
            if(actor.getHealth() >= 1.0f) actor.setHealth(actor.getHealth() + healValue);

            // Instantiating effect
            Instantiate(healEffect, actor.transform);
        }

        // Spending mana from player mana pool
        player.setMana(player.getMana() - manaCost);
    }
}
