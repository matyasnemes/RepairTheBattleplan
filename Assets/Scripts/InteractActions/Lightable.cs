using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lightable : MonoBehaviour,
                         IPointerClickHandler
{
    public GameplayController gameplayController;

    // Mana cost of action
    public const int lightManaCost = 20;

    // Click handler for action
    public void OnPointerClick(PointerEventData eventData)
    {
        // Checking if action should be performed
        if (gameplayController.currentAction == GameplayController.PlayerAction.LIGHT_ACTION)
        {
            // Spending mana from player mana pool
            gameplayController.playerMana -= lightManaCost;

            // Rest of the action...
        }
    }
}
