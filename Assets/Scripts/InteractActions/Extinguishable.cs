using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Extinguishable : MonoBehaviour                        
{
    // Extinguished status
    public bool extinguished = false;

    public void Start()
    {
        if (!GetComponent<Collider2D>()) Debug.Log("DAMN");
    }

    // Click handler for action
    public void OnMouseDown()
    {
        int manaCost = GameplayController.getGameplayOptions().extinguishManaCost;
        GameActor player = GameplayController.getPlayer();

        // Checking if action should be performed
        if (GameplayController.getCurrentAction() == GameplayController.PlayerAction.EXTINGUISH_ACTION)
        {
            // Checking available mana and extinguish status
            if (player.getMana() < manaCost || extinguished || player.getHealth() < 0.1f) return;

            // Disabling collider
            Collider2D attachedCollider = GetComponent<Collider2D>();
            if (attachedCollider)
            {
                attachedCollider.enabled = false;
                Debug.Log("YAK");
            }
            extinguished = true;

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer) spriteRenderer.enabled = false;

            // Spending mana from player mana pool
            player.setMana(player.getMana() - manaCost);
        }
    }
}
