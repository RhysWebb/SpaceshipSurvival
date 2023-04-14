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
        InvokeRepeating("SpawnObject", 1.0f, 10.0f);
    }

    void SpawnObject()
    {
        spawnerRandomiser = Random.Range(0, spawnedItem.Length);
        Instantiate(spawnedItem[spawnerRandomiser]);
    }
}
