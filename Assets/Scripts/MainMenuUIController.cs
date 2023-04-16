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
    // Tutorial ---------------------------------------------------------------------
    // High Score -------------------------------------------------------------------
    [Space, SerializeField] private GameObject highscoreHolder;
    [SerializeField] private GameObject highscoreMovementHolder;
    [SerializeField] private TextMeshProUGUI highscoreOneText;
    [SerializeField] private TextMeshProUGUI highscoreTwoText;
    [SerializeField] private TextMeshProUGUI highscoreThreeText;
    [SerializeField] private TextMeshProUGUI highscoreFourText;
    [SerializeField] private TextMeshProUGUI highscoreFiveText;
    private bool isHighscoreActive;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = new Vector2(160, 110);
        isMovementActive = false;
    }

    // Update is called once per frame
    void Update()
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

    }
    public void CombatLMBButton()
    {

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
    // Movement WASD ---------------------------------------------------------------
    
    public void StartGame()
    {

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
}
