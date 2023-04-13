using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    private float speed = 1.2f;
    private float maxVelocity = 1.2f;
    private float rotationalSpeed = 125f;
    private float playerInputY;
    private float playerInputX;
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject bomb;
    private Rigidbody2D rb;
    public int health;
    [SerializeField] GameObject[] brokenPieces;
    private int totalPieces;
    GameManager gameManager;

    public virtual void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        totalPieces = 0;
        health = 15;
    }

    // Update is called once per frame
    void Update()
    {
        playerInputY = Input.GetAxis("Vertical");
        playerInputX = Input.GetAxis("Horizontal");
        Vector2 movement = transform.up * playerInputY;
        rb.AddForce(movement * speed);
        transform.Rotate(Vector3.back * Time.deltaTime * rotationalSpeed * playerInputX);
        if (Input.GetButtonDown("FireRocket"))
            FireRocket();
        if (Input.GetButtonDown("Bomb"))
            DropBombs();
    }

    private void FixedUpdate()
    {
        ClampedVelocity();
    }

    void DropBombs()
    {
        Instantiate(bomb, transform.position, transform.rotation);
        gameManager.DropBombs();
    }

    void FireRocket()
    {
        Instantiate(rocket, transform.position, transform.rotation);
        gameManager.FireRockets();
    }

    void ClampedVelocity()
    {
        Vector3 clampedVelocity = rb.velocity;
        clampedVelocity.x = Mathf.Clamp(clampedVelocity.x, -maxVelocity, maxVelocity);
        clampedVelocity.y = Mathf.Clamp(clampedVelocity.y, -maxVelocity, maxVelocity);
        clampedVelocity.z = Mathf.Clamp(clampedVelocity.z, -maxVelocity, maxVelocity);
        rb.velocity = clampedVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("Debris") || collision.gameObject.CompareTag("Explosion"))
        health--;
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
}
