using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Objects
    public GameObject[] spawnedItem;
    private int spawnerRandomiser;

    private void Start()
    {
        InvokeRepeating("SpawnObject", 1f, Random.Range(3.5f, 6.0f));
    }

    void SpawnObject()
    {
        spawnerRandomiser = Random.Range(0, spawnedItem.Length);
        Instantiate(spawnedItem[spawnerRandomiser]);
    }
}
