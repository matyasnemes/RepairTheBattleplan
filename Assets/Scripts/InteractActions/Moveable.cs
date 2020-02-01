using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Moveable : MonoBehaviour,
                        IPointerClickHandler
{
    // Move status
    public bool moving = false;

    // Move directions
    public Vector3 moveOne;
    public Vector3 moveTwo;

    // Move counters
    public int moveCounter = 0;
    public int moveRemaining = 0;
    private double moveTimer = 0.0f;
    public double moveTimerPeriod = 0.05f;

    // Click handler for action
    public void OnPointerClick(PointerEventData eventData)
    {
        int manaCost = GameplayController.getGameplayOptions().levitateManaCost;
        GameActor player = GameplayController.getPlayer();

        // Checking if action should be performed
        if (GameplayController.getCurrentAction() == GameplayController.PlayerAction.LEVITATE_ACTION)
        {
            // Checking available mana and move status
            if (player.getMana() < manaCost || moving) return;

            // Initiating move
            moveRemaining = 50;
            moveCounter++;
            moving = true;

            // Spending mana from player mana pool
            player.setMana(player.getMana() - manaCost);
        }
    }

    public void Update()
    {
        // Moving time forwards
        moveTimer += Time.deltaTime;

        // Moving object
        if (moveRemaining > 0 && moveTimer > moveTimerPeriod)
        {
            Vector3 moveVector = moveCounter % 2 == 0 ? moveOne : moveTwo;
            this.transform.Translate(moveVector / 50);
            moveRemaining--;
            moveTimer = 0.0f;
        }

        if (moveRemaining == 0) moving = false;
    }
}
