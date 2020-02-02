using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    // Menu elements
    public InputField playerNameField;
    public Button playButton;
    public Button exitButton;

    // In-game elements
    public Slider playerHealthBar;
    public Slider playerManaBar;
    public Slider knight1HealthBar;
    public Slider knight2HealthBar;
    public Slider knight3HealthBar;
    public Slider knight4HealthBar;
    public Slider knight5HealthBar;
    public Image  currentActionImage;
    public Image  playerImage;
    public Image  knight1Image;
    public Image  knight2Image;
    public Image  knight3Image;
    public Image  knight4Image;
    public Image  knight5Image;

    public void Initialize()
    {
        // Initializing menu
        playButton.gameObject.SetActive(false);

        // Setting healthbar maximum values
        playerHealthBar.maxValue = GameplayController.getGameplayOptions().playerMaxHealth;
        knight1HealthBar.maxValue = GameplayController.getGameplayOptions().knight1MaxHealth;
        knight2HealthBar.maxValue = GameplayController.getGameplayOptions().knight2MaxHealth;
        knight3HealthBar.maxValue = GameplayController.getGameplayOptions().knight3MaxHealth;
        knight4HealthBar.maxValue = GameplayController.getGameplayOptions().knight4MaxHealth;
        knight5HealthBar.maxValue = GameplayController.getGameplayOptions().knight5MaxHealth;

        // Setting mana bar maximum value
        playerManaBar.maxValue = GameplayController.getGameplayOptions().playerMaxMana;
    }

    // Updating GUI controller
    void Update()
    {
        // Check player name availability
        if (playerNameField.text.Length > 0)
        {
            playButton.gameObject.SetActive(true);
        }
        else
        {
            playButton.gameObject.SetActive(false);
        }

        // Updating healthbars
        playerHealthBar.value = GameplayController.getPlayer().getHealth();
        knight1HealthBar.value = GameplayController.getKnight(1).getHealth();
        knight2HealthBar.value = GameplayController.getKnight(2).getHealth();
        knight3HealthBar.value = GameplayController.getKnight(3).getHealth();
        knight4HealthBar.value = GameplayController.getKnight(4).getHealth();
        knight5HealthBar.value = GameplayController.getKnight(5).getHealth();

        // Updating manabar
        playerManaBar.value = GameplayController.getPlayer().getMana();

        // Updating current action sprite
        switch (GameplayController.getCurrentAction())
        {
            case GameplayController.PlayerAction.GHOST_ACTION: currentActionImage.sprite = GameplayController.getGameplayOptions().ghostSprite; break;
            case GameplayController.PlayerAction.HEAL_ACTION: currentActionImage.sprite = GameplayController.getGameplayOptions().healSprite; break;
            case GameplayController.PlayerAction.UNLOCK_ACTION: currentActionImage.sprite = GameplayController.getGameplayOptions().unlockSprite; break;
            case GameplayController.PlayerAction.LEVITATE_ACTION: currentActionImage.sprite = GameplayController.getGameplayOptions().levitateSprite; break;
            case GameplayController.PlayerAction.LIGHT_ACTION: currentActionImage.sprite = GameplayController.getGameplayOptions().lightSprite; break;
            case GameplayController.PlayerAction.EXTINGUISH_ACTION: currentActionImage.sprite = GameplayController.getGameplayOptions().extinguishSprite; break;
        }

        // Updating actor icons
        if(GameplayController.getGameState() == GameplayController.GameState.PLAY_STATE && 
           GameplayController.getPlayer().getHealth() < 0.1)
        {
            playerImage.sprite = GameplayController.getGameplayOptions().skullSprite;
        }
        if (GameplayController.getGameState() == GameplayController.GameState.PLAY_STATE && 
            GameplayController.getKnight(1).getHealth() < 0.1)
        {
            knight1Image.sprite = GameplayController.getGameplayOptions().skullSprite;
        }
        if (GameplayController.getGameState() == GameplayController.GameState.PLAY_STATE && 
            GameplayController.getKnight(2).getHealth() < 0.1)
        {
            knight2Image.sprite = GameplayController.getGameplayOptions().skullSprite;
        }
        if (GameplayController.getGameState() == GameplayController.GameState.PLAY_STATE && 
            GameplayController.getKnight(3).getHealth() < 0.1)
        {
            knight3Image.sprite = GameplayController.getGameplayOptions().skullSprite;
        }
        if (GameplayController.getGameState() == GameplayController.GameState.PLAY_STATE && 
            GameplayController.getKnight(4).getHealth() < 0.1)
        {
            knight4Image.sprite = GameplayController.getGameplayOptions().skullSprite;
        }
        if (GameplayController.getGameState() == GameplayController.GameState.PLAY_STATE && 
            GameplayController.getKnight(5).getHealth() < 0.1)
        {
            knight5Image.sprite = GameplayController.getGameplayOptions().skullSprite;
        }
    }
}
