using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportShip : MonoBehaviour
{
    // Variables
    private float speed = 2.0f;
    private float rotation = 2.0f;
    public GameObject[] airDrops;
    private int airDropsIndex;
    private int airDropsCount;
    [SerializeField] public int maxDrops = 5;

    // Start is called before the first frame update
    void Start()
    {
        airDropsCount = Random.Range(0, maxDrops);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
        transform.Rotate(new Vector3(0.0f, 0.0f, rotation * Time.deltaTime * speed));
        airDropsIndex = Random.Range(0, airDrops.Length);
    }

    void AirDrops()
    {
        for (int i = 0; i < airDropsCount; i++)
        {
            Instantiate(airDrops[airDropsIndex], transform.position, transform.rotation);
        }
    }
}
