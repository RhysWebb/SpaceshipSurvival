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
    public int maxDrops = 5;
    private PlayerMovement playerMovement;
    [SerializeField] private GameObject shields;

    // Start is called before the first frame update
    void Start()
    {
        airDropsCount = Random.Range(0, maxDrops);
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        shields = GameObject.FindGameObjectWithTag("Shields");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
        transform.Rotate(new Vector3(0.0f, 0.0f, rotation * Time.deltaTime * speed));
        airDropsIndex = Random.Range(0, airDrops.Length);
        if (playerMovement.isShieldActive)
        {
            shields.SetActive(false);
        }
    }

    void AirDrops()
    {
        for (int i = 0; i < airDropsCount; i++)
        {
            Instantiate(airDrops[airDropsIndex], transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}
