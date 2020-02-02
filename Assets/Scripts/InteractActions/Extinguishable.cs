using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Extinguishable : MonoBehaviour,
                              IPointerClickHandler
{
    // Extinguished status
    public bool extinguished = false;

    // Click handler for action
    public void OnPointerClick(PointerEventData eventData)
    {
        int manaCost = GameplayController.getGameplayOptions().extinguishManaCost;
        GameActor player = GameplayController.getPlayer();

        // Checking if action should be performed
        if (GameplayController.getCurrentAction() == GameplayController.PlayerAction.EXTINGUISH_ACTION)
        {
            // Checking available mana and extinguish status
            if (player.getMana() < manaCost || extinguished) return;

            // Disabling collider
            Collider attachedCollider = GetComponent<Collider>();
            if(attachedCollider) attachedCollider.enabled = false;
            extinguished = true;

            // Spending mana from player mana pool
            player.setMana(player.getMana() - manaCost);
        }
    }
}
