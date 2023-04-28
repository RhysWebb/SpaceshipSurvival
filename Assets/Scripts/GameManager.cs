using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    private string currentPlayerName;
    public string playerName
    {
        get { return currentPlayerName;  }
        set { currentPlayerName = value; }
    }
    // Score -----------------------------------------------------------
    // Game Management -------------------------------------------------
    private bool isGameCurrentlyActive;
    public bool isGameActive
    {
        get { return isGameCurrentlyActive; }
        set { isGameCurrentlyActive = value; }
    }
    private int gameDifficulty;
    public int gameDifficultyNumber
    {
        get { return gameDifficulty; }
        set { gameDifficulty = value; }
    }
    // Game Management -------------------------------------------------
    // High score ------------------------------------------------------
    private string highScoreOne;
    private int highScoreOneInt;
    public string highScoreTwo;
    public int highScoreTwoInt;
    public string highScoreThree;
    public int highScoreThreeInt;
    public string highScoreFour;
    private int highScoreFourInt;
    public string highScoreFive;
    private int highScoreFiveInt;
    // High score ------------------------------------------------------
    // Stats -----------------------------------------------------------
    private int asteroidStats;
    private int smallAsteroidStats;
    private int rocketsFiredStats;
    private int bombsDroppedStats;
    // Stats -----------------------------------------------------------
    // Variables -------------------------------------------------------
    
    // Awake, Start && Update ------------------------------------------
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadSaveFile();
    }
    // Awake, Start && Update ------------------------------------------
    // High score ------------------------------------------------------
    public void HighScoreTable(string name, int score)
    {
        if (score >= highScoreOneInt)
        {
            highScoreFive = highScoreFour;
            highScoreFiveInt = highScoreFourInt;
            highScoreFour = highScoreThree;
            highScoreFourInt = highScoreThreeInt;
            highScoreThree = highScoreTwo;
            highScoreThreeInt = highScoreTwoInt;
            highScoreTwo = highScoreOne;
            highScoreTwoInt = highScoreOneInt;
            highScoreOne = name;
            highScoreOneInt = score;
        }
        else if (score >= highScoreTwoInt && score < highScoreOneInt)
        {
            highScoreFive = highScoreFour;
            highScoreFiveInt = highScoreFourInt;
            highScoreFour = highScoreThree;
            highScoreFourInt = highScoreThreeInt;
            highScoreThree = highScoreTwo;
            highScoreThreeInt = highScoreTwoInt;
            highScoreTwoInt = score;
            highScoreTwo = name;
        }
        else if (score >= highScoreThreeInt && score < highScoreTwoInt)
        {
            highScoreFive = highScoreFour;
            highScoreFiveInt = highScoreFourInt;
            highScoreFour = highScoreThree;
            highScoreFourInt = highScoreThreeInt;
            highScoreThreeInt = score;
            highScoreThree = name;
        }
        else if (score >= highScoreFourInt && score < highScoreThreeInt)
        {
            highScoreFive = highScoreFour;
            highScoreFiveInt = highScoreFourInt;
            highScoreFourInt = score;
            highScoreFour = name;
        }
        else if (score >= highScoreFiveInt &&  score < highScoreFourInt)
        {
            highScoreFiveInt = score;
            highScoreFive = name;
        }
        SaveFile();
    }
    // High score ------------------------------------------------------
    // Statistics ------------------------------------------------------
    public void AsteroidStatsIncrease()
    {
        asteroidStats++;
        SaveFile();
    }
    public void SmallAsteroidStatsIncrease()
    {
        smallAsteroidStats++;
        SaveFile();
    }
    public void RocketsFiredStatsIncrease()
    {
        rocketsFiredStats++;
        SaveFile();
    }
    public void BombsDroppedStatsIncrease()
    {
        bombsDroppedStats++;
        SaveFile();
    }
    // Statistics ------------------------------------------------------
    // Save && Load ----------------------------------------------------
    [System.Serializable]
    class SaveData
    {
        public string highScoreOneString;
        public int highScoreOneInt;
        public string highScoreTwoString;
        public int highScoreTwoInt;
        public string highScoreThreeString;
        public int highScoreThreeInt;
        public string highScoreFourString;
        public int highScoreFourInt;
        public string highScoreFiveString;
        public int highScoreFiveInt;
        public int asteroidStats;
        public int smallAsteroidStats;
        public int rocketsFiredStats;
        public int bombsDroppedStats;
    }
    public void SaveFile()
    {
        SaveData data = new SaveData();
        data.highScoreOneString = highScoreOne;
        data.highScoreOneInt = highScoreOneInt;
        data.highScoreTwoString = highScoreTwo;
        data.highScoreTwoInt = highScoreTwoInt;
        data.highScoreThreeString = highScoreThree;
        data.highScoreThreeInt = highScoreThreeInt;
        data.highScoreFourString = highScoreFour;
        data.highScoreFourInt = highScoreFourInt;
        data.highScoreFiveString = highScoreFive;
        data.highScoreFiveInt = highScoreFiveInt;
        data.asteroidStats = asteroidStats;
        data.smallAsteroidStats = smallAsteroidStats;
        data.rocketsFiredStats = rocketsFiredStats;
        data.bombsDroppedStats = bombsDroppedStats;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "saveFile.json", json);
    }
    public void LoadSaveFile()
    {
        string path = Application.persistentDataPath + "saveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreOne = data.highScoreOneString;
            highScoreOneInt = data.highScoreOneInt;
            highScoreTwo = data.highScoreTwoString;
            highScoreTwoInt = data.highScoreTwoInt;
            highScoreThree = data.highScoreThreeString;
            highScoreThreeInt = data.highScoreThreeInt;
            highScoreFour = data.highScoreFourString;
            highScoreFourInt = data.highScoreFourInt;
            highScoreFive = data.highScoreFiveString;
            highScoreFiveInt = data.highScoreFiveInt;
            asteroidStats = data.asteroidStats;
            smallAsteroidStats = data.smallAsteroidStats;
            rocketsFiredStats = data.rocketsFiredStats;
            bombsDroppedStats = data.bombsDroppedStats;
        }
    }
    // Save && Load ----------------------------------------------------
}