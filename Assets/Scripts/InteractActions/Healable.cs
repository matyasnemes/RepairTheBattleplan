using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Healable : MonoBehaviour,
                        IPointerClickHandler
{
    public GameplayController gameplayController;

    // Mana cost of action
    public const int healManaCost = 20;

    // Click handler for action
    public void OnPointerClick(PointerEventData eventData)
    {
        // Checking if action should be performed
        if (gameplayController.currentAction == GameplayController.PlayerAction.HEAL_ACTION)
        {
            // Spending mana from player mana pool
            gameplayController.playerMana -= healManaCost;

            // Rest of the action...
        }
    }
}
