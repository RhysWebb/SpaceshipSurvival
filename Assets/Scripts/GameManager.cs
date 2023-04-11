using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variables
    public int health;
    public int maxHealth = 10;
    public int ammo;
    public int maxAmmo = 10;
    public int bombs;
    public int maxBombs = 5;
    public float fuel;
    public float maxFuel = 100;
    public float score;
    public bool shieldTrigger;
    public int shieldHealth;
    public int shieldMaxHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        ammo = maxAmmo;
        bombs = maxBombs;
        fuel = maxFuel;
        shieldTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
