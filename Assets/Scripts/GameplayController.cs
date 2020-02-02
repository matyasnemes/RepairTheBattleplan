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

    public static GameState getGameState()
    {
        return instance.currentGameState;
    }

    public static void setGameState(GameState state)
    {
        instance.currentGameState = state;
    }

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

    public static void registerKnight(GameObject knight, int id)
    {
        // Setting max health
        switch(id)
        {
            case 1: knight.GetComponent<GameActor>().setMaxHealth(instance.options.knight1MaxHealth); break;
            case 2: knight.GetComponent<GameActor>().setMaxHealth(instance.options.knight2MaxHealth); break;
            case 3: knight.GetComponent<GameActor>().setMaxHealth(instance.options.knight3MaxHealth); break;
            case 4: knight.GetComponent<GameActor>().setMaxHealth(instance.options.knight4MaxHealth); break;
            case 5: knight.GetComponent<GameActor>().setMaxHealth(instance.options.knight5MaxHealth); break;
        }

        // Setting initial health
        switch (id)
        {
            case 1: knight.GetComponent<GameActor>().setHealth(instance.options.knight1MaxHealth); break;
            case 2: knight.GetComponent<GameActor>().setHealth(instance.options.knight2MaxHealth); break;
            case 3: knight.GetComponent<GameActor>().setHealth(instance.options.knight3MaxHealth); break;
            case 4: knight.GetComponent<GameActor>().setHealth(instance.options.knight4MaxHealth); break;
            case 5: knight.GetComponent<GameActor>().setHealth(instance.options.knight5MaxHealth); break;
        }

        // Setting knight instance
        switch (id)
        {
            case 1: instance.knight1 = knight.GetComponent<GameActor>(); break;
            case 2: instance.knight2 = knight.GetComponent<GameActor>(); break;
            case 3: instance.knight3 = knight.GetComponent<GameActor>(); break;
            case 4: instance.knight4 = knight.GetComponent<GameActor>(); break;
            case 5: instance.knight5 = knight.GetComponent<GameActor>(); break;
        }
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
        instance.guiManager.Initialize();
    }

    // Initializing gameplay controller
    void Initialize()
    {
        // Initializing gamestate
        currentGameState = GameState.MENU_STATE;

        // Initializing actor max healths 
        player.setMaxHealth(options.playerMaxHealth);

        // Initializing actors to max health
        player.setHealth(options.playerMaxHealth);

        // Initializing player max mana
        player.setMaxMana(options.playerMaxMana);

        // Initializing player mana to maximum
        player.setMana(options.playerMaxMana);

        // Initializing current action to HEAL
        currentAction = PlayerAction.HEAL_ACTION;
    }
}
