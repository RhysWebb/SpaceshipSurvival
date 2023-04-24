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
    private float xRange = 10.0f;
    private float yRange = 10.0f;
    private int count = 3;
    private bool isLTR = false;
    private bool isRTL = false;
    private bool isTTB = false;
    private bool isBTT = false;
    private float miniTimer;
    private int dropCounter;

    // Start is called before the first frame update
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
                transform.position = new Vector3(-7.5f, Random.Range(-ySpawnRange, ySpawnRange));
        }
        if (transform.position.x >= xSpawnRange)
        {
            SpawnedSpeedLTR();
            isLTR = true;
        }
        else if (transform.position.x <= -xSpawnRange)
        {
            SpawnedSpeedRTL();
            isRTL = true;
        }
        else if (transform.position.y >= ySpawnRange)
        {
            SpawnedSpeedTTB();
            isTTB = true;
        }
        else if (transform.position.y <= -ySpawnRange)
        {
            SpawnedSpeedBTT();
            isBTT = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        miniTimer += Time.deltaTime;
        if (transform.position.x < -xRange || transform.position.x > xRange || transform.position.y < -yRange || transform.position.y > yRange)
            Destroy(gameObject);
        if (isLTR)
        {
            SpawnedSpeedLTR();
        }
        else if (isRTL)
        {
            SpawnedSpeedRTL();
        }
        else if (isTTB)
        {
            SpawnedSpeedTTB();
        }
        else if (isBTT)
        {
            SpawnedSpeedBTT();
        }
        if (miniTimer >= 2 && dropCounter < airDropsCount) 
        {
            AirDrops();
            dropCounter++;
            miniTimer = 0;
        }
    }
    void AirDrops()
    {
        airDropsIndex = Random.Range(0, airDrops.Length);
        Instantiate(airDrops[airDropsIndex], transform.position, transform.rotation);
    }
    // Spawn directions ----------------------------------------------------------
    void SpawnedSpeedLTR()
    {
        float speed = Random.Range(1.5f, 2.5f);
        transform.Translate(-Vector2.right * Time.deltaTime * speed);
        transform.Rotate(new Vector3(0.0f, 0.0f, 270.0f));
    }
    void SpawnedSpeedRTL()
    {
        float speed = Random.Range(1.5f, 2.5f);
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f));
    }
    void SpawnedSpeedBTT()
    {
        float speed = Random.Range(1.5f, 2.5f);
        transform.Translate(Vector2.up * Time.deltaTime * speed);
        transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f));
    }
    void SpawnedSpeedTTB()
    {
        float speed = Random.Range(1.5f, 2.5f);
        transform.Translate(-Vector2.up * Time.deltaTime * speed);
        transform.Rotate(new Vector3(0.0f, 0.0f, 180.0f));
    }
    // Spawn directions ----------------------------------------------------------

    // Collisions ----------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
    IEnumerator TimerToWait(int amount)
    {
        yield return new WaitForSeconds(amount); 
    }
    // Collisions ----------------------------------------------------------------
}
