using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Variables
    public float rotation;
    public float speed = 3.0f;
    public float time;
    public float lifeSpan = 3.0f;
    public GameObject explosion;
    [SerializeField] private GameObject boomer;
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, rotation * Time.deltaTime * speed));
        time += Time.deltaTime;
        if (time > lifeSpan)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(boomer, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            GameManager.Instance.AsteroidStatsIncrease();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Instantiate(boomer, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("SmallAsteroid"))
        {
            GameManager.Instance.SmallAsteroidStatsIncrease();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Instantiate(boomer, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (!collision.gameObject.CompareTag("Player") || !collision.gameObject.CompareTag("Rocket") || !collision.gameObject.CompareTag("Shields"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Instantiate(boomer, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
