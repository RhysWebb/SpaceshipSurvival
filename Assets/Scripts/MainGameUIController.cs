using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainGameUIController : MonoBehaviour
{
    // Variables --------------------------------------------------
    // Running Game UI --------------------------------------------
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI bombText;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Animator pauseAnimator;
    [SerializeField] GameObject settingsScreen;
    [SerializeField] TextMeshProUGUI gameOverScore;
    [SerializeField] GameObject gameOverScreen;
    [Space]
    // Running Game UI --------------------------------------------

    // Pause & Settings -------------------------------------------
    private bool isSettingsActive = false;
    private bool pauseActive = false;
    private bool isGameCurrentlyActive;
    public bool isGameActive
    {
        get { return isGameCurrentlyActive; }
        set { isGameCurrentlyActive = value; }
    }
    // Pause & Settings -------------------------------------------

    // Spawners ---------------------------------------------------
    private float asteroidSpawnRate;
    private float supportShipSpawnRate;
    [SerializeField] private GameObject[] spawnedItem;
    [SerializeField] private GameObject supportShip;
    // Spawners ---------------------------------------------------

    // Game Over --------------------------------------------------
    private int difficultyInput;
    // Game Over --------------------------------------------------
    // Variables --------------------------------------------------

    // Start game function -------------------------------------------------------------
    public void StartGame(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                GameManager.Instance.maxHealth = 6;
                GameManager.Instance.maxAmmo = 15;
                GameManager.Instance.maxBombs = 5;
                GameManager.Instance.shieldMax = 15;
                asteroidSpawnRate = 2.0f;
                supportShipSpawnRate = 25.0f;  
                GameManager.Instance.score = 0;
                scoreText.text = $"{GameManager.Instance.score}";
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                isGameActive = true;
                Time.timeScale = 1.0f;
                break;
            case 2:
                GameManager.Instance.maxHealth = 6;
                GameManager.Instance.maxAmmo = 12;
                GameManager.Instance.maxBombs = 4;
                GameManager.Instance.shieldMax = 10;
                asteroidSpawnRate = 1.5f;
                supportShipSpawnRate = 22.0f;
                GameManager.Instance.score = 0;
                scoreText.text = $"{GameManager.Instance.score}";
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                isGameActive = true;
                Time.timeScale = 1.0f;
                break;
            case 3:
                GameManager.Instance.maxHealth = 6;
                GameManager.Instance.maxAmmo = 8;
                GameManager.Instance.maxBombs = 3;
                GameManager.Instance.shieldMax = 5;
                asteroidSpawnRate = 1.0f;
                supportShipSpawnRate = 20.0f;
                GameManager.Instance.score = 0;
                scoreText.text = $"{GameManager.Instance.score}";
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                isGameActive = true;
                Time.timeScale = 1.0f;
                break;
            default:
                GameManager.Instance.maxHealth = 6;
                GameManager.Instance.maxAmmo = 15;
                GameManager.Instance.maxBombs = 5;
                GameManager.Instance.shieldMax = 15;
                asteroidSpawnRate = 2.0f;
                supportShipSpawnRate = 25.0f;
                GameManager.Instance.score = 0;
                scoreText.text = $"{GameManager.Instance.score}";
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                isGameActive = true;
                Time.timeScale = 1.0f;
                break;
        }
    }
    // Start game function -------------------------------------------------------------

    // Awake, Start & Update -----------------------------------------------------------
    private void Start()
    {
        StartGame(GameManager.Instance.gameDifficultyNumber);
        StartCoroutine(SpawnAsteroids(asteroidSpawnRate));
        StartCoroutine(SpawnSupportShip(supportShipSpawnRate));
        BombGUIUpdater();
        RocketGGUIUpdater();
        Time.timeScale = 1.0f;
    }
    // Awake, Start & Update -----------------------------------------------------------

    // Game UI -------------------------------------------------------------------------
    public void Pause()
    {
        if (!pauseActive)
        {
            pauseScreen.gameObject.SetActive(true);
            pauseAnimator.SetTrigger("Paused");
            pauseActive = true;
            isGameActive = false;
            Time.timeScale = 0.0f;
        }
        else if (pauseActive)
        {
            pauseAnimator.SetTrigger("UnPause");
            pauseScreen.gameObject.SetActive(false);
            pauseActive = false;
            isGameActive = true;
            Time.timeScale = 1.0f;
        }
    }
    public void Settings()
    {
        if (pauseActive && !isSettingsActive)
        {
            pauseAnimator.SetTrigger("SettingsSelected");
            isSettingsActive = true;
        }
        else if (pauseActive && isSettingsActive)
        {
            pauseAnimator.SetTrigger("SettingsClosed");
            isSettingsActive = false;
        }
    }
    public void ScoreUpdate(int scoreIncrement)
    {
        GameManager.Instance.score += scoreIncrement;
        scoreText.text = $"{GameManager.Instance.score}";
    }
    public void DropBombs()
    {
        GameManager.Instance.bombs--;
        BombGUIUpdater();
    }
    public void BombGUIUpdater()
    {
        bombText.text = $"{GameManager.Instance.bombs}";
    }
    public void FireRockets()
    {
        GameManager.Instance.ammo--;
        RocketGGUIUpdater();
    }
    public void RocketGGUIUpdater()
    {
        ammoText.text = $"{GameManager.Instance.ammo}";
    }
    // Game UI -------------------------------------------------------------------------

    // Misc. ---------------------------------------------------------------------------
    public void LivesUpdate()
    {
        GameManager.Instance.health--;
        if (GameManager.Instance.health <= 0)
            GameOver();
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
        GameManager.Instance.HighScoreTable(GameManager.Instance.playerName, GameManager.Instance.score);
    }
    public void RestartGame()
    {
        GameManager.Instance.gameDifficultyNumber = difficultyInput;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Misc. ---------------------------------------------------------------------------

    // Game Over -----------------------------------------------------------------------
    public void DifficultyEasy()
    {
        difficultyInput = 1;
        RestartGame();
    }
    public void DifficultyMedium()
    {
        difficultyInput = 2;
        RestartGame();
    }
    public void DifficultyHard()
    {
        difficultyInput = 3;
        RestartGame();
    }

    // Game Over -----------------------------------------------------------------------

    // Spawners ------------------------------------------------------------------------
    IEnumerator SpawnAsteroids(float inputSpawnRate)
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(inputSpawnRate);
            int spawnerRandomiser = Random.Range(0, spawnedItem.Length);
            Instantiate(spawnedItem[spawnerRandomiser]);
        }
    }
    IEnumerator SpawnSupportShip(float inputSpawnRate)
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(inputSpawnRate);
            Instantiate(supportShip);
        }
    }
    // Spawners ------------------------------------------------------------------------
}
