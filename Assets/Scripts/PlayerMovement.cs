using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    private float speed = 2f;
    private float maxVelocity = 2f;
    private float rotationalSpeed = 25f;
    private float playerInputY;
    private float playerInputX;
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject bomb;
    private Rigidbody2D rb;
    public int health;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        Velocity();
    }

    void DropBombs()
    {
        Instantiate(bomb, transform.position, transform.rotation);
    }

    void FireRocket()
    {
        Instantiate(rocket, transform.position, transform.rotation);
    }

    void Velocity()
    {
        float speed = rb.velocity.magnitude;
        Debug.Log("Speed: " + speed);
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
        Debug.Log("Health Lowered");
        health--;
        if (health <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
