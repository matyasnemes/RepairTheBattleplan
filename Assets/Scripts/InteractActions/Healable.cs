using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Healable : MonoBehaviour,
                        IPointerClickHandler
{
    public GameplayController gameplayController;

    // Mana cost of action
    public const int healManaCost = 20;
    public const int healValue = 10;
    public int knightNumber;

    // Click handler for action
    public void OnPointerClick(PointerEventData eventData)
    {
        // Checking if action should be performed
        if (gameplayController.currentAction == GameplayController.PlayerAction.HEAL_ACTION)
        {
            // Checking available mana and health condition
            if (gameplayController.playerMana < healManaCost) return;

            // Healing actor
            switch(knightNumber)
            {
                // Player
                case 0: gameplayController.playerHealth = Math.Min(GameplayController.player_max_health, 
                                                                   gameplayController.playerHealth + healValue); break;
                // Knights
                case 1: gameplayController.knight_1_health = Math.Min(GameplayController.knight_1_max_health,
                                                                      gameplayController.knight_1_health + healValue); break; 
                case 2: gameplayController.knight_2_health = Math.Min(GameplayController.knight_2_max_health,
                                                                      gameplayController.knight_2_health + healValue); break;
                case 3: gameplayController.knight_3_health = Math.Min(GameplayController.knight_3_max_health,
                                                                      gameplayController.knight_3_health + healValue); break;
                case 4: gameplayController.knight_4_health = Math.Min(GameplayController.knight_4_max_health,
                                                                      gameplayController.knight_4_health + healValue); break;
                case 5: gameplayController.knight_5_health = Math.Min(GameplayController.knight_5_max_health,
                                                                      gameplayController.knight_5_health + healValue); break;
                default: return;
            }

            // Spending mana from player mana pool
            gameplayController.playerMana -= healManaCost;
        }
    }
}
