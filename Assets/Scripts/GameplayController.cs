using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameplayController : MonoBehaviour
{
    // Enumeration of gamestate
    public enum GameState { MENU_STATE, PLAY_STATE };

    // Enumeration of player actions
    public enum PlayerAction { GHOST_ACTION, HEAL_ACTION, UNLOCK_ACTION, LEVITATE_ACTION, LIGHT_ACTION, EXTINGUISH_ACTION };


    // -----------------------------------------------------


    // Static singleton instace
    public static GameplayController instance = null;
    

    // -----------------------------------------------------

    
    // All assets and global config values are contained here
    public GameplayOptions options;

    // GUI Manager
    [Header("GUI")]
    public GUIManager guiManager;

    // Current gamestate
    public GameState currentGameState;

    // Current action
    public PlayerAction currentAction;

    // Main Actors
    [Header("Main Actors")]
    public GameActor player;
    public GameActor knight1;
    public GameActor knight2;
    public GameActor knight3;
    public GameActor knight4;
    public GameActor knight5;


    // -----------------------------------------------------


    // Static getters & setters

    public static GameplayOptions getGameplayOptions() {
        return instance.options;
    }

    public static void setCurrentAction(PlayerAction action)
    {
        instance.currentAction = action;
    }

    public static PlayerAction getCurrentAction()
    {
        return instance.currentAction;
    }

    public static GameActor getPlayer()
    {
        return instance.player;
    }

    public static GameActor getKnight(int id)
    {
        switch(id)
        {
            case 1: return instance.knight1;
            case 2: return instance.knight2;
            case 3: return instance.knight3;
            case 4: return instance.knight4;
            case 5: return instance.knight5;
            default: return null;
        }
    }

    // -----------------------------------------------------

    void Awake()
    {
        instance = this;
        instance.Initialize();
        instance.options = new GameplayOptions();
        instance.guiManager.Initialize();
    }

    // Initializing gameplay controller
    void Initialize()
    {
        // Initializing gamestate
        currentGameState = GameState.MENU_STATE;

        // Initializing actor max healths
        player.setMaxHealth(options.playerMaxHealth);
        knight1.setMaxHealth(options.knight1MaxHealth);
        knight2.setMaxHealth(options.knight2MaxHealth);
        knight3.setMaxHealth(options.knight3MaxHealth);
        knight4.setMaxHealth(options.knight4MaxHealth);
        knight5.setMaxHealth(options.knight5MaxHealth);

        // Initializing actors to max health
        player.setHealth(options.playerMaxHealth);
        knight1.setHealth(options.knight1MaxHealth);
        knight2.setHealth(options.knight2MaxHealth);
        knight3.setHealth(options.knight3MaxHealth);
        knight4.setHealth(options.knight4MaxHealth);
        knight5.setHealth(options.knight5MaxHealth);

        // Initializing player max mana
        player.setMaxMana(options.playerMaxMana);

        // Initializing player mana to maximum
        player.setMana(options.playerMaxMana);

        // Initializing current action to HEAL
        currentAction = PlayerAction.HEAL_ACTION;
    }

    // Updating gameplayer controller
    void Update()
    {

    }
}
