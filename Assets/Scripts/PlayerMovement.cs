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
    // Misc. -------------------------------------------------
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject bomb;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private int health;
    [Space, SerializeField] GameObject[] brokenPieces;
    private int totalPieces;
    private bool paused = false;
    [SerializeField] GameObject shield;
    public bool isShieldActive = false;
    private int shieldMaximum;
    private float counter;
    private MainGameUIController mainGameUIController;
    // Misc. -------------------------------------------------
    // Variables ---------------------------------------------

    // Start & Update ----------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GameObject.Find("PlayerRenderer").GetComponent<SpriteRenderer>();
        mainGameUIController = GameObject.Find("Canvas").GetComponent<MainGameUIController>();
        totalPieces = 0;
        health = GameManager.Instance.health;
        shieldMaximum = GameManager.Instance.shieldMax;
    }
    void Update()
    {
        if (isShieldActive)
        {
            counter += Time.deltaTime;
            if (counter > shieldMaximum)
            {
                isShieldActive = false;
                shield.SetActive(false);
                counter = 0;
            }
        }
        Debug.Log(health);
        GameManager.Instance.health = health;
        paused = GameManager.Instance.isGameActive;
        if (!paused)
        {            
            PlayerMoving();
            if (Input.GetButtonDown("FireRocket"))
                FireRocket();
            if (Input.GetButtonDown("Bomb"))
                DropBomb();
        }
    }
    private void FixedUpdate()
    {
        ClampedVelocity();
    }
    // Start & Update ----------------------------------------

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

    // Trigggers & Enumorators -------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("Debris") || collision.gameObject.CompareTag("Explosion"))
        {
            health--;
        }
        else if (collision.CompareTag("Shields"))
        {
            shield.SetActive(true);
            isShieldActive = true;
        }
        else if (collision.CompareTag("InstantShields"))
        {
            if (!isShieldActive)
            {
                Destroy(collision.gameObject);
                shield.SetActive(true);
                isShieldActive = true;
            }
        }
        else if (collision.CompareTag("AmmoBombs"))
        {
            if (GameManager.Instance.bombs < GameManager.Instance.maxBombs)
            {
                Destroy(collision.gameObject);
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                mainGameUIController.BombGUIUpdater();
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
                }
                else if (GameManager.Instance.maxAmmo - GameManager.Instance.ammo < 5)
                {
                    Destroy(collision.gameObject);
                    GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                    mainGameUIController.RocketGGUIUpdater();
                }
            }
        }
        
        if (health <= 0 )
        {
            Destroy(gameObject);
            for (int i = 0; i < 3; i++) 
            {
                Instantiate(brokenPieces[totalPieces], transform.position, transform.rotation);
                totalPieces++;
            }
        }
    }
    private IEnumerator DamageIndicator()
    {
        yield return new WaitForSeconds(1);
    }
    // Trigggers & Enumorators -------------------------------
} // Class
