using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lightable : MonoBehaviour,
                         IPointerClickHandler
{
    // Lighted status
    public bool lighted = false;

    // Click handler for action
    public void OnPointerClick(PointerEventData eventData)
    {
        int manaCost = GameplayController.getGameplayOptions().lightManaCost;
        GameActor player = GameplayController.getPlayer();

        // Checking if action should be performed
        if (GameplayController.getCurrentAction() == GameplayController.PlayerAction.LIGHT_ACTION)
        {
            // Checking available mana and unlock status
            if (player.getMana() < manaCost || lighted) return;

            // Disabling collider
            Collider attachedCollider = GetComponent<Collider>();
            if (attachedCollider) attachedCollider.enabled = false;
            lighted = true;

            // Spending mana from player mana pool
            player.setMana(player.getMana() - manaCost);
        }
    }
}
