using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
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
    private float asteroidSpawnRate;
    private float supportShipSpawnRate;
    public GameObject[] spawnedItem;
    // UI --------------------------------------------------------------
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI bombText;
    // UI --------------------------------------------------------------

    // Variables -------------------------------------------------------
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        //LoadSaveFile(); Not Implemented yet
    }

    public void ScoreUpdate(int scoreIncrement)
    {
        score += scoreIncrement;
        scoreText.text = $"{score}";
    }
    public void DropBombs()
    {
        bombs--;
        bombText.text = $"{currentBombs}";
    }
    public void FireRockets()
    {
        ammo--;
        ammoText.text = $"{currentAmmo}";
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
    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}