using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Ghostable : MonoBehaviour
{
    public float remainingGhostDuration;

    public void makeGhosted()
    {
        remainingGhostDuration = GameplayController.getGameplayOptions().ghostEffectDuration;
    }

    // Click handler for action
    public void OnPointerClick(PointerEventData eventData)
    {
        int manaCost = GameplayController.getGameplayOptions().ghostManaCost;
        GameActor player = GameplayController.getPlayer();

        // Checking if action should be performed
        if (GameplayController.getCurrentAction() == GameplayController.PlayerAction.GHOST_ACTION)
        {
            // Checking available mana
            if (player.getMana() < manaCost) return;

            // Making target ghosted
            makeGhosted();

            // Spending mana from player mana pool
            player.setMana(player.getMana() - manaCost);
        }
    }

    public void Update()
    {
        // Decrementing remaining ghost effect duration (min. 0)
        remainingGhostDuration -= Time.deltaTime;
        remainingGhostDuration = Math.Max(0, remainingGhostDuration);
    }
}
