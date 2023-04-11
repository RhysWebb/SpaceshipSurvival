using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables
    public float speed = 2f;
    public float maxVelocity = 2f;
    public float rotationalSpeed = 25f;
    public float playerInputY;
    public float playerInputX;
    public GameObject rocket;
    public GameObject bomb;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Destroy(gameObject);
    }
}
