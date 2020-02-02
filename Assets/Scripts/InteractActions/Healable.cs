using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Healable : MonoBehaviour,
                        IPointerClickHandler
{
    // Click handler for action
    public void OnPointerClick(PointerEventData eventData)
    {
        int manaCost = GameplayController.getGameplayOptions().healManaCost;
        GameActor player = GameplayController.getPlayer();

        // Checking if action should be performed
        if (GameplayController.getCurrentAction() == GameplayController.PlayerAction.HEAL_ACTION)
        {
            // Checking available mana and health condition
            if (player.getMana() < manaCost) return;

            // Increasing actor health
            GameActor actor = GetComponent<GameActor>();
            actor.setHealth(actor.getHealth() + 10);
        }

        // Spending mana from player mana pool
        player.setMana(player.getMana() - manaCost);
    }
}
