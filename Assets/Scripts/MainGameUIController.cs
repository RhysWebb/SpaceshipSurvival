using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

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

    // Variables --------------------------------------------------

    // Start game function -------------------------------------------------------------
    public void StartGame(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                GameManager.Instance.maxHealth = 10;
                GameManager.Instance.maxAmmo = 15;
                GameManager.Instance.maxBombs = 5;
                GameManager.Instance.shieldMax = 5;
                asteroidSpawnRate = 2.0f;
                supportShipSpawnRate = 45.0f;
                GameManager.Instance.score = 0;
                scoreText.text = $"{GameManager.Instance.score}";
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                StartCoroutine(SpawnAsteroids(asteroidSpawnRate));
                StartCoroutine(SpawnSupportShip(supportShipSpawnRate));
                isGameActive = true;
                Time.timeScale = 1.0f;
                break;
            case 2:
                GameManager.Instance.maxHealth = 8;
                GameManager.Instance.maxAmmo = 10;
                GameManager.Instance.maxBombs = 4;
                GameManager.Instance.shieldMax = 3;
                asteroidSpawnRate = 1.5f;
                supportShipSpawnRate = 60.0f;
                GameManager.Instance.score = 0;
                scoreText.text = $"{GameManager.Instance.score}";
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                StartCoroutine(SpawnAsteroids(asteroidSpawnRate));
                StartCoroutine(SpawnSupportShip(supportShipSpawnRate));
                isGameActive = true;
                Time.timeScale = 1.0f;
                break;
            case 3:
                GameManager.Instance.maxHealth = 5;
                GameManager.Instance.maxAmmo = 5;
                GameManager.Instance.maxBombs = 3;
                GameManager.Instance.shieldMax = 1;
                asteroidSpawnRate = 1.0f;
                supportShipSpawnRate = 75.0f;
                GameManager.Instance.score = 0;
                scoreText.text = $"{GameManager.Instance.score}";
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                StartCoroutine(SpawnAsteroids(asteroidSpawnRate));
                StartCoroutine(SpawnSupportShip(supportShipSpawnRate));
                isGameActive = true;
                Time.timeScale = 1.0f;
                break;
            default:
                GameManager.Instance.maxHealth = 10;
                GameManager.Instance.maxAmmo = 15;
                GameManager.Instance.maxBombs = 5;
                GameManager.Instance.shieldMax = 5;
                asteroidSpawnRate = 2.0f;
                supportShipSpawnRate = 45.0f;
                GameManager.Instance.score = 0;
                scoreText.text = $"{GameManager.Instance.score}";
                GameManager.Instance.health = GameManager.Instance.maxHealth;
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                StartCoroutine(SpawnAsteroids(asteroidSpawnRate));
                StartCoroutine(SpawnSupportShip(supportShipSpawnRate));
                isGameActive = true;
                Time.timeScale = 1.0f;
                break;
        }
    }
    // Start game function -------------------------------------------------------------

    // Game UI -------------------------------------------------------------------------
    public void Pause()
    {
        if (!pauseActive)
        {
            pauseScreen.gameObject.SetActive(true);
            pauseAnimator.SetTrigger("Paused");
            pauseActive = true;
            Time.timeScale = 0.0f;
            /*
            if (isGameActive)
            {
                pauseScreen.gameObject.SetActive(true);
                isGameActive = false;
                Time.timeScale = 0.0f;
            }
            else if (!isGameActive)
            {
                pauseScreen.gameObject.SetActive(false);
                isGameActive = true;
                Time.timeScale = 1.0f;
            }*/
        }
        else if (pauseActive) 
        {
            pauseAnimator.SetTrigger("UnPause");
            pauseScreen.gameObject.SetActive(false);
            pauseActive = false;
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
    // Game UI -------------------------------------------------------------------------
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