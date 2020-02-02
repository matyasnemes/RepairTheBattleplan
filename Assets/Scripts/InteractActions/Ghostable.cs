using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Ghostable : MonoBehaviour
{
    public float remainingGhostDuration;

    // Click handler for action
    public void OnMouseDown()
    {
        int manaCost = GameplayController.getGameplayOptions().ghostManaCost;
        GameActor player = GameplayController.getPlayer();

        // Checking if action should be performed
        if (GameplayController.getCurrentAction() == GameplayController.PlayerAction.GHOST_ACTION)
        {
            // Checking available mana
            if (player.getMana() < manaCost || player.getHealth() < 0.1f) return;

            // Making target ghosted
            remainingGhostDuration = GameplayController.getGameplayOptions().ghostEffectDuration;

            // Spending mana from player mana pool
            player.setMana(player.getMana() - manaCost);
        }
    }

    public void Update()
    {
        // Decrementing remaining ghost effect duration (min. 0)
        remainingGhostDuration -= Time.deltaTime;
        remainingGhostDuration = Math.Max(0, remainingGhostDuration);

        // Updating opacity effect
        SpriteRenderer renderer = GetComponentInParent<SpriteRenderer>();
        float alpha = 1.1f - (remainingGhostDuration / GameplayController.getGameplayOptions().ghostEffectDuration);
        renderer.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        Debug.Log(alpha);
                                   
    }
}
