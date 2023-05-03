using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrusters : MonoBehaviour
{
    [SerializeField] GameObject[] thrusters;
    [SerializeField] Rigidbody2D parentRb;
    float testSpeed;
    private AudioSource engines;

    private void Start()
    {
        engines = GetComponent<AudioSource>();
    }

    private void Update()
    {
        testSpeed = parentRb.velocity.magnitude;
        ChangingThrusters();
    }

    void ChangingThrusters()
    {
        if (testSpeed < 0)
        {
            thrusters[0].SetActive(false);
            engines.Stop();
        } 
        else if (testSpeed > 0 && testSpeed <= 0.33)
        {
            thrusters[0].SetActive(true);
            thrusters[1].SetActive(false);
            engines.Play();
            engines.volume = 0.1f;
        }
        else if (testSpeed > 0.33 && testSpeed <= 0.66)
        {
            thrusters[0].SetActive(false);
            thrusters[1].SetActive(true);
            thrusters[2].SetActive(false);
            engines.volume = 0.3f;
        }
        else if (testSpeed > 0.66 && testSpeed <= 1)
        {
            thrusters[1].SetActive(false);
            thrusters[2].SetActive(true);
            thrusters[3].SetActive(false);
            engines.volume = 0.4f;
        }
        else if (testSpeed > 1)
        {
            thrusters[2].SetActive(false);
            thrusters[3].SetActive(true);
            engines.volume = 0.6f;
        }
    }


}
