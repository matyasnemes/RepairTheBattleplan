using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lightable : MonoBehaviour
{
    // Lighted status
    public bool lighted = false;

    // Click handler for action
    public void OnMouseDown()
    {
        int manaCost = GameplayController.getGameplayOptions().lightManaCost;
        GameActor player = GameplayController.getPlayer();

        // Checking if action should be performed
        if (GameplayController.getCurrentAction() == GameplayController.PlayerAction.LIGHT_ACTION)
        {
            // Checking available mana and unlock status
            if (player.getMana() < manaCost || lighted || player.getHealth() < 0.1f) return;

            // Disabling collider
            Collider attachedCollider = GetComponent<Collider>();
            if (attachedCollider) attachedCollider.enabled = false;
            lighted = true;

            // Change sprite
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(1.0f, 1.0f, 1.0f, 0.1f);

            // Spending mana from player mana pool
            player.setMana(player.getMana() - manaCost);
        }
    }
}
