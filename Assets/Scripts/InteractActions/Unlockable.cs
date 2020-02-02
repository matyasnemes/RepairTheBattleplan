using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unlockable : MonoBehaviour,
                          IPointerClickHandler
{
    // Unlocked status
    public bool unlocked = false;

    public Sprite unlockedsprite;

    // Click handler for action
    public void OnPointerClick(PointerEventData eventData)
    {
        int manaCost = GameplayController.getGameplayOptions().unlockManaCost;
        GameActor player = GameplayController.getPlayer();

        // Checking if action should be performed
        if (GameplayController.getCurrentAction() == GameplayController.PlayerAction.UNLOCK_ACTION)
        {
            // Checking available mana and unlock status
            if (player.getMana() < manaCost || unlocked) return;

            // Disabling collider
            Collider attachedCollider = GetComponent<Collider>();
            if (attachedCollider) attachedCollider.enabled = false;
            unlocked = true;

            // Spending mana from player mana pool
            player.setMana(player.getMana() - manaCost);

            GetComponent<SpriteRenderer>().sprite = unlockedsprite;
        }
    }
}
