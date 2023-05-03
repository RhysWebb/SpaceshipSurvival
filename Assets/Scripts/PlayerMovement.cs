using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables ---------------------------------------------
    // Speed -------------------------------------------------
    private float speed = 1.2f;
    private float maxVelocity = 1.2f;
    private float rotationalSpeed = 125f;
    // Speed -------------------------------------------------
    // Input -------------------------------------------------
    private float playerInputY;
    private float playerInputX;
    // Input -------------------------------------------------
    // Weapons -----------------------------------------------
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject bomb;
    // Weapons -----------------------------------------------
    // Player health -----------------------------------------
    [SerializeField] private Sprite[] playerHealthSprite;
    [SerializeField] private GameObject playerShields;
    [Space] public bool isShieldActive;
    private bool playerDamaged;
    // Player health -----------------------------------------
    // Misc. -------------------------------------------------
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float damageTimer;
    private int damageCounter;
    [Space, SerializeField] GameObject[] brokenPieces;
    private int totalPieces;
    private bool paused = false;
    private AudioSource playerAudioSource;
    public float counter;
    private MainGameUIController mainGameUIController;
    [SerializeField] private AudioClip playerDamagedSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip shieldSound;
    // Misc. -------------------------------------------------
    // Variables ---------------------------------------------

    // Start & Update ----------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GameObject.Find("PlayerRenderer").GetComponent<SpriteRenderer>();
        mainGameUIController = GameObject.Find("Canvas").GetComponent<MainGameUIController>();
        playerAudioSource = GetComponent<AudioSource>();
        totalPieces = 0;
    }
    void Update()
    {
        PlayerHealthSprite(GameManager.Instance.health);
        paused = GameManager.Instance.isGameActive;
        if (!paused && mainGameUIController.isGameActive)
        {
            PlayerMoving();
            if (Input.GetButtonDown("FireRocket") && GameManager.Instance.ammo > 0)
            {
                GameManager.Instance.RocketsFiredStatsIncrease();
                FireRocket();
            }
            if (Input.GetButtonDown("Bomb") && GameManager.Instance.bombs > 0)
            {
                GameManager.Instance.BombsDroppedStatsIncrease();
                DropBomb();
            }
        }
        if (isShieldActive)
        {
            playerShields.SetActive(true);
        }
        PlayerDamaged();
        if (Input.GetKey(KeyCode.Escape))
            mainGameUIController.Pause();
        
    }
    private void FixedUpdate()
    {
        ClampedVelocity();
    }
    // Start & Update ----------------------------------------
    // Damage ------------------------------------------------
    void PlayerDamaged()
    {
        if (playerDamaged)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer > 0.2)
            {
                spriteRenderer.enabled = false;
            }
            if (damageTimer > 0.4)
            {
                spriteRenderer.enabled = true;
                damageCounter++;
                damageTimer = 0;
            }
            if (damageCounter == 5)
                playerDamaged = false;
        }
    }
    // Damage ------------------------------------------------
    // Sound -------------------------------------------------
    void PlayerSoundController(AudioClip soundInput)
    {
        playerAudioSource.Stop();
        playerAudioSource.clip = soundInput;
        playerAudioSource.Play();
    }
    // Sound -------------------------------------------------
    // Player Functions --------------------------------------
    void PlayerMoving()
    {
        playerInputY = Input.GetAxis("Vertical");
        playerInputX = Input.GetAxis("Horizontal");
        Vector2 movement = transform.up * playerInputY;
        rb.AddForce(movement * speed);
        transform.Rotate(Vector3.back * Time.deltaTime * rotationalSpeed * playerInputX);
    }
    void DropBomb()
    {
        if (GameManager.Instance.bombs > 0)
        {
            Instantiate(bomb, transform.position, transform.rotation);
            mainGameUIController.DropBombs();
        }
    }
    void FireRocket()
    {
        if (GameManager.Instance.ammo > 0)
        {
            Instantiate(rocket, transform.position, transform.rotation);
            mainGameUIController.FireRockets();
        }
    }
    void ClampedVelocity()
    {
        Vector3 clampedVelocity = rb.velocity;
        clampedVelocity.x = Mathf.Clamp(clampedVelocity.x, -maxVelocity, maxVelocity);
        clampedVelocity.y = Mathf.Clamp(clampedVelocity.y, -maxVelocity, maxVelocity);
        clampedVelocity.z = Mathf.Clamp(clampedVelocity.z, -maxVelocity, maxVelocity);
        rb.velocity = clampedVelocity;
    }
    // Player Functions --------------------------------------
    
    // Player Health -----------------------------------------
    void PlayerHealthSprite(float inputHealth)
    {
        switch(inputHealth)
        {
            case 6:
                spriteRenderer.sprite = playerHealthSprite[0];
                break;
            case 5:
                spriteRenderer.sprite = playerHealthSprite[1];
                break;
            case 4:
                spriteRenderer.sprite = playerHealthSprite[2];
                break;
            case 3: 
                spriteRenderer.sprite = playerHealthSprite[3];
                break;
            case 2: 
                spriteRenderer.sprite = playerHealthSprite[4];
                break;
            case 1: 
                spriteRenderer.sprite = playerHealthSprite[5];
                break;
            case 0:           
                Destroy(gameObject);
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(brokenPieces[totalPieces], transform.position, transform.rotation);
                    totalPieces++;
                }
                break;
            default: 
                spriteRenderer.sprite = playerHealthSprite[0]; 
                break;
        }
    }
    // Player Health -----------------------------------------
    
    // Trigggers & Enumerators -------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Asteroid") || collision.CompareTag("Debris") || collision.gameObject.CompareTag("Explosion")))
        {
            if (!playerDamaged)
            {
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection = pushDirection.normalized;
                rb.AddForce(pushDirection * 5, ForceMode2D.Impulse);
                mainGameUIController.LivesUpdate();
                playerDamaged = true;
                PlayerSoundController(playerDamagedSound);
            }
        }
        else if (collision.CompareTag("Shields") && !isShieldActive)
        {
            isShieldActive = true;
            PlayerSoundController(shieldSound);
        }
        else if (collision.CompareTag("InstantShields") && !isShieldActive)
        {
            Destroy(collision.gameObject);
            isShieldActive = true;
            PlayerSoundController(shieldSound);
        }
        else if (collision.CompareTag("AmmoBombs"))
        {
            if (GameManager.Instance.bombs < GameManager.Instance.maxBombs)
            {
                Destroy(collision.gameObject);
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                mainGameUIController.BombGUIUpdater();
                PlayerSoundController(reloadSound);
            }
        }
        else if (collision.CompareTag("AmmoRockets"))
        {
            if (GameManager.Instance.ammo < GameManager.Instance.maxAmmo)
            {
                if (GameManager.Instance.maxAmmo - GameManager.Instance.ammo >= 5)
                {
                    Destroy(collision.gameObject);
                    GameManager.Instance.ammo += 5;
                    mainGameUIController.RocketGGUIUpdater();
                    PlayerSoundController(reloadSound);
                }
                else if (GameManager.Instance.maxAmmo - GameManager.Instance.ammo < 5)
                {
                    Destroy(collision.gameObject);
                    GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                    mainGameUIController.RocketGGUIUpdater();
                    PlayerSoundController(reloadSound);
                }
            }
        }
    }
    // Trigggers & Enumerators -------------------------------
} // Class
