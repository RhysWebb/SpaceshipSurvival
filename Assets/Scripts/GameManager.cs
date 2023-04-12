using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variables -------------------------------------------------------
    // Health ----------------------------------------------------------
    private int maximumHealth = 10;
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
    private int maximumAmmo = 10;
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
    private int maximumBombs = 5;
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
    private int shieldMaximum = 5;
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
    // Variables -------------------------------------------------------











    public void ScoreUpdate(int scoreIncrement)
    {
        score += scoreIncrement;
    }



}
