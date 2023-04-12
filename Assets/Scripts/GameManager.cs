using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variables -------------------------------------------------------
    // Health ----------------------------------------------------------
    private int maximumHealth;
    public int maxHealth
    {
        get { return maximumHealth; }
        set { maximumHealth = value; }
    }
    private int currentHealth;
    public int health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    // Health -----------------------------------------------------------

    // Ammo -------------------------------------------------------------
    private int currentAmmo;
    public int ammo
    {
        get { return currentAmmo; }
        set { currentAmmo = value; }
    }
    private int maximumAmmo;
    public int maxAmmo
    {
        get { return maximumAmmo; }
        set { maximumAmmo = value; }
    }
    private int currentBombs;
    public int bombs
    {
        get { return currentBombs; }
        set { currentBombs = value; }
    }
    private int maximumBombs;
    public int maxBombs
    {
        get { return maximumBombs; }
        set { maximumBombs = value; }
    }
    // Ammo -------------------------------------------------------------

    // Shields ----------------------------------------------------------
    private bool shieldTrigger;
    public bool shieldTriggerInput
    {
        get { return shieldTrigger; }
        set { shieldTrigger = value; }
    }
    private int currentShields;
    public int shields
    {
        get { return currentShields; }
        set { currentShields = value; }
    }
    private int shieldMaximum;
    public int shieldMax
    {
        get { return shieldMaximum; }
        set { shieldMaximum = value; }
    }
    // Shields ---------------------------------------------------------

    // Score -----------------------------------------------------------
    private int currentScore;
    public int score
    { 
        get { return currentScore; }
        set { currentScore = value; }
    }
    // Score -----------------------------------------------------------
    private bool isGameCurrentlyActive;
    public bool isGameActive
    {
        get { return isGameCurrentlyActive; }
        set { isGameCurrentlyActive = value; }
    }
    private float spawnRate;
    // Variables -------------------------------------------------------

    public void StartGame(int difficulty)
    {
        if (difficulty == 1)
        {
            maxHealth = 10;
            maxAmmo = 15;
            maxBombs = 5;
            shieldMax = 5;
            spawnRate = 2.0f;
        }
        else if (difficulty == 2)
        {
            maxHealth = 8;
            maxAmmo = 10;
            maxBombs = 4;
            shieldMax = 3;
            spawnRate = 1.5f;
        }
        else if (difficulty == 3)
        {
            maxHealth = 5;
            maxAmmo = 5;
            maxBombs = 3;
            shieldMax = 1;
            spawnRate = 1.0f;
        }

        currentScore = 0;
        health = maxHealth;
        bombs = maxBombs;
        ammo = maxAmmo;
        isGameActive = true;
        Time.timeScale = 1.0f;
    }









    public void ScoreUpdate(int scoreIncrement)
    {
        score += scoreIncrement;
    }
    public void LivesUpdate()
    {
        maxHealth--;
        if (maxHealth <= 0)
            GameOver();
    }
    public void GameOver()
    {
        isGameActive = false;
    }
}
