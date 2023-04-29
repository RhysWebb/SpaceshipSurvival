using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Variables
    public float time;
    public float timeToLive = 10.0f;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeToLive)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            GameManager.Instance.AsteroidStatsIncrease();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("SmallAsteroid"))
        {
            GameManager.Instance.SmallAsteroidStatsIncrease();
            Destroy(collision.gameObject);
        }
    }
}
