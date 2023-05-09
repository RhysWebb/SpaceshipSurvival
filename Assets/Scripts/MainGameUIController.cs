using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Audio;

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
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider gameSoundsVolume;
    [SerializeField] private AudioMixer audioMixer;
    private AudioSource mainGameAudioSource;
    // Pause & Settings -------------------------------------------

    // Spawners ---------------------------------------------------
    private float asteroidSpawnRate;
    private float supportShipSpawnRate;
    [SerializeField] private GameObject[] spawnedItem;
    [SerializeField] private GameObject supportShip;
    // Spawners ---------------------------------------------------

    // Health -----------------------------------------------------
    [SerializeField] private Slider shipsHealth;
    // Health -----------------------------------------------------

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
        gameOverScreen.SetActive(false);
        StartGame(GameManager.Instance.gameDifficultyNumber);
        StartCoroutine(SpawnAsteroids(asteroidSpawnRate));
        StartCoroutine(SpawnSupportShip(supportShipSpawnRate));
        BombGUIUpdater();
        RocketGGUIUpdater();
        Time.timeScale = 1.0f;
        masterVolume.onValueChanged.AddListener(SetVolumeMaster);
        gameSoundsVolume.onValueChanged.AddListener(SetVolumeGameSounds);
        musicVolume.onValueChanged.AddListener(SetVolumeMusic);
        mainGameAudioSource = GetComponent<AudioSource>();
        mainGameAudioSource.clip = GameManager.Instance.buttonPress;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            Pause();
    }
    // Awake, Start & Update -----------------------------------------------------------

    // Game UI -------------------------------------------------------------------------
    public void Pause()
    {
        if (!pauseActive && isGameActive)
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
        mainGameAudioSource.Play();
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

    // Sound ---------------------------------------------------------------------------
    public void MusicOneSelected()
    {
        mainGameAudioSource.Play();
        GameManager.Instance.PlayMusicOne();
    }
    public void MusicTwoSelected()
    {
        mainGameAudioSource.Play();
        GameManager.Instance.PlayMusicTwo();
    }
    public void MusicThreeSelected()
    {
        mainGameAudioSource.Play();
        GameManager.Instance.PlayMusicThree();
    }
    public void MusicFourSelected()
    {
        mainGameAudioSource.Play();
        GameManager.Instance.PlayMusicFour();
    }
    public void MouseHoverSound()
    {
        GameManager.Instance.ButtonHoverSound();
    }
    public void SetVolumeMaster(float volume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
    }
    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
    }
    public void SetVolumeGameSounds(float volume)
    {
        audioMixer.SetFloat("gameSoundsVolume", Mathf.Log10(volume) * 20);
    }
    // Sound ---------------------------------------------------------------------------

    // Health --------------------------------------------------------------------------
    public void LivesUpdate()
    {
        GameManager.Instance.health--;
        PlayerHealthBar(GameManager.Instance.health);
    }
    private void PlayerHealthBar(int inputHealth)
    {
        switch (inputHealth)
        {
            case 6:
                shipsHealth.value = 1.0f;
                break;
            case 5: 
                shipsHealth.value = 0.85f; 
                break;
            case 4: 
                shipsHealth.value = 0.65f;
                break;
            case 3:
                shipsHealth.value = 0.50f;
                break;
            case 2:
                shipsHealth.value = 0.35f;
                break;
            case 1:
                shipsHealth.value = 0.15f;
                break;
            case 0:
                shipsHealth.value = 0.00f;
                GameOver();
                break;
            default:
                shipsHealth.value = 1.0f;
                break;
        }
    }
    // Health --------------------------------------------------------------------------

    // Misc. ---------------------------------------------------------------------------
    public void GameOver()
    {
        isGameActive = false;
        gameOverScreen.SetActive(true);
        GameManager.Instance.HighScoreTable(GameManager.Instance.playerName, GameManager.Instance.score);
        gameOverScore.text = $"Score: {GameManager.Instance.playerName} {GameManager.Instance.score}";
    }
    void RestartGame()
    {
        GameManager.Instance.gameDifficultyNumber = difficultyInput;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
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
