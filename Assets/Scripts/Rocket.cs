using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Rocket : MonoBehaviour
{
    // Variable
    public float speed = 3f;
    public GameObject explosion;
    public float maxDistance = 3.0f;
    private Vector3 lastPosition;
    private float distanceTraveled;
    [SerializeField] private GameObject boomer;

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        DistanceTravelled();
        if (DistanceTravelled() > 4.5f)
        {
            Instantiate(boomer, transform.position, transform.rotation);
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    float DistanceTravelled()
    {
        float distance = Vector3.Distance(transform.position, lastPosition);
        distanceTraveled += distance;
        lastPosition = transform.position;
        return distanceTraveled;
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
        else if (!collision.gameObject.CompareTag("PlayerShield"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            if (!collision.gameObject.CompareTag("Shields"))
            {
                Destroy(collision.gameObject);
            }
            Instantiate(boomer, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
