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

    public void StartGame(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                maxHealth = 10;
                maxAmmo = 15;
                maxBombs = 5;
                shieldMax = 5;
                asteroidSpawnRate = 2.0f;
                supportShipSpawnRate = 45.0f;
                break;
            case 2:
                maxHealth = 8;
                maxAmmo = 10;
                maxBombs = 4;
                shieldMax = 3;
                asteroidSpawnRate = 1.5f;
                supportShipSpawnRate = 60.0f;
                break;
            case 3:
                maxHealth = 5;
                maxAmmo = 5;
                maxBombs = 3;
                shieldMax = 1;
                asteroidSpawnRate = 1.0f;
                supportShipSpawnRate = 75.0f;
                break;
            default:
                // Endless gamemode
                /* 
                In endless gamemode there is no health variable but a counter that constantly rolls down. Each asteroid destroyed increaes this gamemodes time. This is 3 seconds for every small asteroid and 5 for larger asteroids (same as their score values in the normal gamemode. Over time less and less will spawn and ammo will slowly decrease. I'm tempted to add fuel here as it will be a minor update to my current script but would mean a lot of work on player movement.

                Time would also be their highscore. So score will be total time played and the countdown will just be a roll down. 
                 */
                break;
        }
        currentScore = 0;
        scoreText.text = $"{currentScore}";
        health = maxHealth;
        bombs = maxBombs;
        ammo = maxAmmo;
        isGameActive = true;
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        bombs = 5;
        ammo = 15;
        health = maxHealth;
    }

    IEnumerator SpawnTarget(float inputSpawnRate)
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(inputSpawnRate);
            int spawnerRandomiser = Random.Range(0, spawnedItem.Length);
            Instantiate(spawnedItem[spawnerRandomiser]);
        }
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