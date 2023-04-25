using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Variables
    private float xRange = 10.0f;
    private float yRange = 10.0f;
    private int count = 3;
    private Rigidbody2D rb;
    [SerializeField] float minSpeed = 500.0f;
    [SerializeField] float maxSpeed = 800.0f;
    [SerializeField] float maxTorque = 60.0f;
    [SerializeField] float xSpawnRange = 7.0f;
    private float ySpawnRange = 5.0f;
    [SerializeField] int scoreValue;
    private MainGameUIController mainGameUIController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainGameUIController = GameObject.Find("Canvas").GetComponent<MainGameUIController>();
        if (transform.position.x < xSpawnRange && transform.position.x > -xSpawnRange && transform.position.y < ySpawnRange && transform.position.y > -ySpawnRange)
        {
            int randomSpawn = Random.Range(0, count);
            if (randomSpawn == 0)
                transform.position = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), 5.5f);
            else if (randomSpawn == 1)
                transform.position = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), -5.5f);
            else if (randomSpawn == 2)
                transform.position = new Vector3(7.5f, Random.Range(-ySpawnRange, ySpawnRange));
            else if (randomSpawn == 3)
                transform.position = new Vector3(-7.5f, Random.Range(-ySpawnRange, ySpawnRange));
        } 
        if (transform.position.x >= xSpawnRange)
            SpawnedSpeedLTR();
        else if (transform.position.x <= -xSpawnRange)
            SpawnedSpeedRTL();
        else if (transform.position.y >= ySpawnRange)
            SpawnedSpeedTTB();
        else if (transform.position.y <= -ySpawnRange)
            SpawnedSpeedBTT();
    }

    void Update()
    {
        if (transform.position.x < -xRange || transform.position.x > xRange || transform.position.y < -yRange || transform.position.y > yRange)
            Destroy(gameObject);
    }

    void SpawnedSpeedLTR()
    {
        float addedForce = Random.Range(minSpeed, maxSpeed);
        float addedTorque = Random.Range(-maxTorque, maxTorque);
        rb.AddForce(-Vector2.right * addedForce, ForceMode2D.Impulse);
        rb.AddTorque(addedTorque, ForceMode2D.Impulse);
    }

    void SpawnedSpeedRTL()
    {
        float addedForce = Random.Range(minSpeed, maxSpeed);
        float addedTorque = Random.Range(-maxTorque, maxTorque);
        rb.AddForce(Vector2.right * addedForce, ForceMode2D.Impulse);
        rb.AddTorque(addedTorque, ForceMode2D.Impulse);
    }

    void SpawnedSpeedBTT()
    {
        float addedForce = Random.Range(minSpeed, maxSpeed);
        float addedTorque = Random.Range(-maxTorque, maxTorque);
        rb.AddForce(Vector2.up * addedForce, ForceMode2D.Impulse);
        rb.AddTorque(addedTorque, ForceMode2D.Impulse);
    }

    void SpawnedSpeedTTB()
    {
        float addedForce = Random.Range(minSpeed, maxSpeed);
        float addedTorque = Random.Range(-maxTorque, maxTorque);
        rb.AddForce(-Vector2.up * addedForce, ForceMode2D.Impulse);
        rb.AddTorque(addedTorque, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Explosion") || collision.gameObject.CompareTag("Rocket"))
        {
            mainGameUIController.ScoreUpdate(scoreValue);
            Destroy(gameObject);
        }
    }
}
