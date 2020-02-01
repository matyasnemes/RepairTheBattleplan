using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameplayController : MonoBehaviour
{
    // Predefined maximum gameplay values
    public const int player_max_health   = 100;
    public const int player_max_mana     = 100;
    public const int knight_1_max_health = 100;
    public const int knight_2_max_health = 100;
    public const int knight_3_max_health = 100;
    public const int knight_4_max_health = 100;
    public const int knight_5_max_health = 100;

    // Enumeration of player actions
    public enum PlayerAction { GHOST_ACTION, HEAL_ACTION, UNLOCK_ACTION, LEVITATE_ACTION, LIGHT_ACTION, EXTINGUISH_ACTION };

    // Actual gameplay values
    public int playerHealth;
    public int playerMana;
    public PlayerAction currentAction;
    public int knight_1_health;
    public int knight_2_health;
    public int knight_3_health;
    public int knight_4_health;
    public int knight_5_health;

    // GUI Slider handles
    public Slider playerHealthBar;
    public Slider playerManaBar;
    public Slider knight_1_healthBar;
    public Slider knight_2_healthBar;
    public Slider knight_3_healthBar;
    public Slider knight_4_healthBar;
    public Slider knight_5_healthBar;

    // GUI Action image handles
    public Sprite actionGhostSprite;
    public Sprite actionHealSprite;
    public Sprite actionUnlockSprite;
    public Sprite actionLevitateSprite;
    public Sprite actionLightSprite;
    public Sprite actionExtinguishSprite;
    public Image  currentActionImage;

    // Initializing gameplay values
    void Start()
    {
        playerHealth    = player_max_health;
        playerMana      = player_max_mana;
        currentAction   = PlayerAction.HEAL_ACTION;
        knight_1_health = knight_1_max_health;
        knight_2_health = knight_2_max_health;
        knight_3_health = knight_3_max_health;
        knight_4_health = knight_4_max_health;
        knight_5_health = knight_5_max_health;

        playerHealthBar.maxValue = player_max_health;
        playerManaBar.maxValue = player_max_mana;
        knight_1_healthBar.maxValue = knight_1_max_health;
        knight_2_healthBar.maxValue = knight_2_max_health;
        knight_3_healthBar.maxValue = knight_3_max_health;
        knight_4_healthBar.maxValue = knight_4_max_health;
        knight_5_healthBar.maxValue = knight_5_max_health;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.value = playerHealth;
        playerManaBar.value = playerMana;
        knight_1_healthBar.value = knight_1_health;
        knight_2_healthBar.value = knight_2_health;
        knight_3_healthBar.value = knight_3_health;
        knight_4_healthBar.value = knight_4_health;
        knight_5_healthBar.value = knight_5_health;

        switch (currentAction)
        {
            case PlayerAction.GHOST_ACTION:         currentActionImage.sprite = actionGhostSprite; break;
            case PlayerAction.HEAL_ACTION:          currentActionImage.sprite = actionHealSprite; break;
            case PlayerAction.UNLOCK_ACTION:        currentActionImage.sprite = actionUnlockSprite; break;
            case PlayerAction.LEVITATE_ACTION:      currentActionImage.sprite = actionLevitateSprite; break;
            case PlayerAction.LIGHT_ACTION:         currentActionImage.sprite = actionLightSprite; break;
            case PlayerAction.EXTINGUISH_ACTION:    currentActionImage.sprite = actionExtinguishSprite; break;
        }
    }
}
