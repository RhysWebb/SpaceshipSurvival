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
    private bool playerDamaged = false;
    private Color player;
    // Misc. -------------------------------------------------
    // Variables ---------------------------------------------

    // Start & Update ----------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GameObject.Find("PlayerRenderer").GetComponent<SpriteRenderer>();
        totalPieces = 0;
        player = spriteRenderer.color;
    }
    void Update()
    {
        health = GameManager.Instance.health;
        paused = GameManager.Instance.isGameActive;
        if (!paused)
            PlayerMoving();
        if (Input.GetButtonDown("FireRocket") && !paused)
            FireRocket();
        if (Input.GetButtonDown("Bomb") && !paused)
            DropBombs();
        if (playerDamaged)
        {
            for (int i = 0; i < 3; i++)
            {
                spriteRenderer.material.color = Color.white;
                StartCoroutine(DamageIndicator());
                spriteRenderer.material.color = player;
                StartCoroutine(DamageIndicator());
            }
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
    void DropBombs()
    {
        Instantiate(bomb, transform.position, transform.rotation);
    }
    void FireRocket()
    {
        Instantiate(rocket, transform.position, transform.rotation);
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
        if (collision.CompareTag("Asteroid") || collision.CompareTag("Debris") || collision.gameObject.CompareTag("Explosion") && !playerDamaged) 
        {
            playerDamaged = true;
            health--;
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
