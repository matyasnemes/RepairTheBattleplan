using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameplayOptions
{
    // Durations & periods
    [Header("Durations & periods")]
    public float regenerationPeriod;
    public float ghostEffectDuration;
    public float levitateStepPeriod;

    // Maximum health values
    [Header("Maximum health values")]
    public int playerMaxHealth = 100;
    public int knight1MaxHealth = 100;
    public int knight2MaxHealth = 100;
    public int knight3MaxHealth = 100;
    public int knight4MaxHealth = 100;
    public int knight5MaxHealth = 100;

    // Maximum mana value
    [Header("Maximum mana value")]
    public int playerMaxMana = 100;

    // Action mana costs
    [Header("Action mana costs")]
    public int ghostManaCost = 10;
    public int healManaCost = 10;
    public int unlockManaCost = 10;
    public int levitateManaCost = 10;
    public int lightManaCost = 10;
    public int extinguishManaCost = 10;

    // Action specific values
    [Header("Action specific values")]
    public int healValue = 20;
    public int healthRegenerationValue = 1;
    public int manaRegenerationValue = 1;
    public int levitateStepCount = 50;

    // Icon sprites
    [Header("Icon sprites")]
    public Sprite ghostSprite;
    public Sprite healSprite;
    public Sprite unlockSprite;
    public Sprite levitateSprite;
    public Sprite lightSprite;
    public Sprite extinguishSprite;

    // Frame sprites
    [Header("NOT USED Frame sprites")]
    public Sprite actorFrameSprite;
    public Sprite actionFrameSprite;
    public Sprite currentActionFrameSprite;
}
