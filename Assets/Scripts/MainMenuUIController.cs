using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    // Variables --------------------------------------------------------------------
    // Tutorial ---------------------------------------------------------------------
    [SerializeField] private GameObject tutorialHolder;
    private bool isTutorialActive;
    [SerializeField] private GameObject tutorialMovementHolder;
    [SerializeField] private GameObject player;
    private Vector2 screenBounds;
    private bool isMovementActive;
    [SerializeField] private GameObject tutorialCombatHolder;
    private bool isCombatActive;
    [SerializeField] private Animator rocketAnimator;
    [SerializeField] private Animator bombAnimator;
    // Tutorial ---------------------------------------------------------------------
    // High Score -------------------------------------------------------------------
    [Space, SerializeField] private GameObject highscoreHolder;
    private bool isStatiticsActive;
    [SerializeField] private GameObject highscoresHolder;
    [SerializeField] private TextMeshProUGUI highscoreOneText;
    [SerializeField] private TextMeshProUGUI highscoreOneInt;
    [SerializeField] private TextMeshProUGUI highscoreTwoText;
    [SerializeField] private TextMeshProUGUI highscoreTwoInt;
    [SerializeField] private TextMeshProUGUI highscoreThreeText;
    [SerializeField] private TextMeshProUGUI highscoreThreeInt;
    [SerializeField] private TextMeshProUGUI highscoreFourText;
    [SerializeField] private TextMeshProUGUI highscoreFourInt;
    [SerializeField] private TextMeshProUGUI highscoreFiveText;
    [SerializeField] private TextMeshProUGUI highscoreFiveInt;
    private bool isHighscoreActive;
    [SerializeField] private GameObject statsHolder;
    private bool isStatsActive;
    // High Score --------------------------------------------------------------------
    // Stats -------------------------------------------------------------------------
    [Space, SerializeField] private TextMeshProUGUI asteroidStats;
    [SerializeField] private TextMeshProUGUI smallAsteroidStats;
    [SerializeField] private TextMeshProUGUI rocketStats;
    [SerializeField] private TextMeshProUGUI bombStats;
    // Stats -------------------------------------------------------------------------
    // Player ------------------------------------------------------------------------
    private TMP_InputField playerNameInput;
    private GameObject playerNameInputObject;
    // Player ------------------------------------------------------------------------
    // Difficulty --------------------------------------------------------------------
    [Space, SerializeField] private Animator menus;
    private int difficultyInput;
    private bool selected = false;
    // Difficulty --------------------------------------------------------------------
    // Variables ---------------------------------------------------------------------

    // Start & Updates ---------------------------------------------------------------
    void Start()
    {
        StartingCaller();

    }
    void Update()
    {
        MovementUpdater();
    }
    // Start & Updates -------------------------------------------------------------

    // Player ----------------------------------------------------------------------
    public void PlayerNameUpdate(string playerName)
    {
        GameManager.Instance.playerName = playerName;
    }
    // Player ----------------------------------------------------------------------

    // Tutorials -------------------------------------------------------------------------------
    // Open and Close Tutorials/Controls -------------------------------------------
    public void OpenControls()
    {
        if (!isTutorialActive)
        {
            tutorialHolder.SetActive(true);
            isTutorialActive = true;
        }
        else if (isTutorialActive)
        {
            tutorialHolder.SetActive(false);
            isTutorialActive = false;
            tutorialCombatHolder.SetActive(false);
            isCombatActive = false;
            tutorialMovementHolder.SetActive(false);
            isMovementActive = false;
        }
    }
    public void ControlsCloseButton()
    {
        tutorialHolder.SetActive(false);
        isTutorialActive = false;
        tutorialCombatHolder.SetActive(false);
        isCombatActive = false;
        tutorialMovementHolder.SetActive(false);
        isMovementActive = false;
    }
    // Open and Close Tutorials/Controls -------------------------------------------
    // Combat On & Off -------------------------------------------------------------
    public void OpenCombat()
    {
        if (!isCombatActive)
        {
            if (isMovementActive)
            {
                tutorialMovementHolder.SetActive(false);
                isMovementActive = false;
                tutorialCombatHolder.SetActive(true);
                isCombatActive = true;
            }
            else
            {
                tutorialCombatHolder.SetActive(true);
                isCombatActive = true;
            }
        }
        else if (isCombatActive)
        {
            tutorialCombatHolder.SetActive(false);
            isCombatActive = false;
        }
    }
    // Combat On & Off -------------------------------------------------------------
    // Combat Buttons --------------------------------------------------------------
    public void CombatSpaceButton()
    {
        bombAnimator.SetTrigger("DropBomb");
    }
    public void CombatLMBButton()
    {
        rocketAnimator.SetTrigger("FireRocket");
    }
    // Combat Buttons --------------------------------------------------------------
    // Movement On & Off -----------------------------------------------------------
    public void OpenMovement()
    {
        if (!isMovementActive)
        {
            if (isCombatActive)
            {
                tutorialCombatHolder.SetActive(false);
                isCombatActive = false;
                tutorialMovementHolder.SetActive(true);
                isMovementActive = true;
            } 
            else
            {
                tutorialMovementHolder.SetActive(true);
                isMovementActive = true;
            }
        }
        else if (isMovementActive)
        {
            tutorialMovementHolder.SetActive(false);
            isMovementActive = false;
        }
    }
    // Movement On & Off -----------------------------------------------------------    
    // Movement WASD ---------------------------------------------------------------
    public void MovementButtonW()
    {
        float speed = 5.0f;
        player.transform.Translate(Vector3.up * speed);
    }
    public void MovementButtonA()
    {
        float rotationalSpeed = 15.0f;
        player.transform.eulerAngles += new Vector3(0, 0, 1) * rotationalSpeed;
    }
    public void MovementButtonS()
    {
        float speed = 5.0f;
        player.transform.Translate(-Vector3.up * speed);
    }
    public void MovementButtonD()
    {
        float rotationalSpeed = 15.0f;
        player.transform.eulerAngles += new Vector3(0, 0, -1) * rotationalSpeed;
    }
    void MovementUpdater()
    {
        Vector3 newPos = player.transform.localPosition;
        if (newPos.x < -screenBounds.x)
            newPos.x = screenBounds.x;
        else if (newPos.x > screenBounds.x)
            newPos.x = -screenBounds.x;
        if (newPos.y < -screenBounds.y)
            newPos.y = screenBounds.y;
        else if (newPos.y > screenBounds.y)
            newPos.y = -screenBounds.y;
        player.transform.localPosition = newPos;
    }
    // Movement WASD ---------------------------------------------------------------
    // Tutorials -------------------------------------------------------------------------------

    // High Scores -----------------------------------------------------------------------------
    // Open and Close Statistics ---------------------------------------------------
    public void OpenStatistics()
    {
        if (!isStatiticsActive)
        {
            highscoreHolder.SetActive(true);
            isStatiticsActive = true;
        }
        else if (isStatiticsActive)
        {
            highscoreHolder.SetActive(false);
            isStatiticsActive = false;
            highscoresHolder.SetActive(false);
            isHighscoreActive = false;
            statsHolder.SetActive(false);
            isStatsActive = false;
        }
    }
    public void CloseStatisticsButton() 
    {
        highscoreHolder.SetActive(false);
        isStatiticsActive = false;
        highscoresHolder.SetActive(false);
        isHighscoreActive = false;
        statsHolder.SetActive(false);
        isStatsActive = false;
    }
    // Open and Close Statistics ---------------------------------------------------
    // High scores On & Off --------------------------------------------------------
    public void OpenHighScores()
    {
        if (!isHighscoreActive)
        {
            if (isStatsActive)
            {
                statsHolder.SetActive(false);
                isStatsActive = false;
                highscoresHolder.SetActive(true);
                isHighscoreActive = true;
            }
            else
            {
                highscoresHolder.SetActive(true);
                isHighscoreActive = true;
            }

        }
        else if (isHighscoreActive)
        {
            highscoresHolder.SetActive(false);
            isHighscoreActive = false;
        }
    }
    // High scores On & Off --------------------------------------------------------
    // Stats On & Off --------------------------------------------------------------
    public void OpenStatisticsMini()
    {
        if (!isStatsActive)
        {
            if (isHighscoreActive)
            {
                highscoresHolder.SetActive(false);
                isHighscoreActive = false;
                statsHolder.SetActive(true);
                isStatsActive = true;
            }
            else
            {
                statsHolder.SetActive(true);
                isStatsActive = true;
            }
        }
        else if (isStatsActive)
        {
            statsHolder.SetActive(false);
            isStatsActive = false;
        }
    }
    // Stats On & Off --------------------------------------------------------------
    // High Scores -----------------------------------------------------------------

    // Difficulty ------------------------------------------------------------------
    public void NewGame()
    {
        if (!selected)
        {
            selected = true;
            menus.SetTrigger("DifficultySelection");
            CloseStatisticsButton();
            ControlsCloseButton();
            StartCoroutine(WaitingForSeconds(2));

        }
        else
        {
            menus.SetTrigger("Reselected");
            playerNameInput.onValueChanged.RemoveListener(PlayerNameUpdate);
        }
    }
    public void ReturnToMenus()
    {
        menus.SetTrigger("BackToMenu");
    }
    public void DifficultyEasy()
    {
        if (string.IsNullOrEmpty(playerNameInput.text))
        {
            playerNameInput.text = "Swabbie";
        }
        else
        {
            difficultyInput = 1;
            StartGame();
        }
    }
    public void DifficultyMedium()
    {
        if (string.IsNullOrEmpty(playerNameInput.text))
        {
            playerNameInput.text = "Lt. Serge";
        }
        else
        {
            difficultyInput = 2;
            StartGame();
        }
    }
    public void DifficultyHard()
    {
        if (string.IsNullOrEmpty(playerNameInput.text))
        {
            playerNameInput.text = "Cptn. Hornswaggler";
        }
        else
        {
            difficultyInput = 3;
            StartGame();
        }
    }
    // Difficulty ------------------------------------------------------------------
    // Enumerators -----------------------------------------------------------------
    IEnumerator WaitingForSeconds(int input)
    {
        yield return new WaitForSeconds(input);
        playerNameInputObject = GameObject.Find("PlayerNameInput");
        playerNameInput = playerNameInputObject.GetComponent<TMP_InputField>();
        playerNameInput.onValueChanged.AddListener(PlayerNameUpdate);
    }
    // Enumerators -----------------------------------------------------------------
    // Misc ------------------------------------------------------------------------
    void StartingCaller()
    {
        screenBounds = new Vector2(160, 110);
        tutorialHolder.SetActive(false);
        highscoreHolder.SetActive(false);
        isTutorialActive = false;
        isMovementActive = false;
        isCombatActive = false;
        isStatiticsActive = false;
        isHighscoreActive = false;
        isStatsActive = false;
        // Text fields ------------------------------------------------------------
        highscoreOneText.text = GameManager.Instance.highScoreOneInput;
        highscoreOneInt.text = GameManager.Instance.highScoreOneIntInput.ToString();
        highscoreTwoText.text = GameManager.Instance.highScoreTwoInput;
        highscoreTwoInt.text = GameManager.Instance.highScoreThreeIntInput.ToString();
        highscoreThreeText.text = GameManager.Instance.highScoreThreeInput;
        highscoreThreeInt.text = GameManager.Instance.highScoreThreeIntInput.ToString();
        highscoreFourText.text = GameManager.Instance.highScoreThreeInput;
        highscoreFourInt.text = GameManager.Instance.highScoreThreeIntInput.ToString();
        highscoreFiveText.text = GameManager.Instance.highScoreFiveInput;
        highscoreFiveInt.text = GameManager.Instance.highScoreFiveIntInput.ToString();
        asteroidStats.text = GameManager.Instance.asteroidStatsInput.ToString();
        smallAsteroidStats.text = GameManager.Instance.smallAsteroidStatsInput.ToString();
        rocketStats.text = GameManager.Instance.rocketsFiredStatsInput.ToString();
        bombStats.text = GameManager.Instance.bombsDroppedStatsInput.ToString();
    }
    void StartGame()
    {
        GameManager.Instance.gameDifficultyNumber = difficultyInput;
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        //GameManager.Instance.SaveFile();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    // Misc ------------------------------------------------------------------------
}
