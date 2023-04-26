using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportShip : MonoBehaviour
{
    // Variables
    [SerializeField] GameObject[] airDrops;
    private int airDropsIndex;
    private int airDropsCount;
    private int maxDrops = 5;
    private float xSpawnRange = 7.0f;
    private float ySpawnRange = 5.0f;
    private float xRange = 8.0f;
    private float yRange = 8.0f;
    private int count = 3;
    private float miniTimer;
    private int dropCounter;


    void Start()
    {
        airDropsCount = Random.Range(1, maxDrops);
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
                transform.position = new Vector3(7.5f, Random.Range(-ySpawnRange, ySpawnRange));
        }
        if (transform.position.x >= xSpawnRange)
            transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
        else if (transform.position.x <= -xSpawnRange)
            transform.Rotate(new Vector3(0.0f, 0.0f, 270.0f));
        else if (transform.position.y >= ySpawnRange)
            transform.Rotate(new Vector3(0.0f, 0.0f, 180.0f));
        else if (transform.position.y <= -ySpawnRange)
            transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f));
    }
    void Update()
    {
        miniTimer += Time.deltaTime;
        SuppportShipMoving();
        if (transform.position.x < -xRange || transform.position.x > xRange || transform.position.y < -yRange || transform.position.y > yRange)
            Destroy(gameObject);
        if (miniTimer >= 2 && dropCounter < airDropsCount) 
        {
            AirDrops();
            dropCounter++;
            miniTimer = 1.2f;
        }
    }
    void AirDrops()
    {
        airDropsIndex = Random.Range(0, airDrops.Length);
        Instantiate(airDrops[airDropsIndex], transform.position, transform.rotation);
    }
    // Spawn directions ----------------------------------------------------------
    void SuppportShipMoving()
    {
        float speed = Random.Range(1.5f, 2.5f);
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
    // Spawn directions ----------------------------------------------------------

    // Collisions ----------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
    // Collisions ----------------------------------------------------------------
}
