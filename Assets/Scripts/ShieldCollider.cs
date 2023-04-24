using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldCollider : MonoBehaviour
{
    private MainGameUIController mainGameUIController;
    private PlayerMovement playerMovement;

    private void Start()
    {
        mainGameUIController = GameObject.Find("Canvas").GetComponent<MainGameUIController>();
        playerMovement = GameObject.Find("Player").GetComponentInParent<PlayerMovement>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InstantShields"))
        {
            if (!playerMovement.isShieldActive)
            {
                Destroy(collision.gameObject);
                playerMovement.counter = 0;
                playerMovement.isShieldActive = true;
            }
        }
        else if (collision.gameObject.CompareTag("AmmoBombs"))
        {
            if (GameManager.Instance.bombs < GameManager.Instance.maxBombs)
            {
                Destroy(collision.gameObject);
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                mainGameUIController.BombGUIUpdater();
            }
        }
        else if (collision.gameObject.CompareTag("AmmoRockets"))
        {
            if (GameManager.Instance.ammo < GameManager.Instance.maxAmmo)
            {
                if (GameManager.Instance.maxAmmo - GameManager.Instance.ammo >= 5)
                {
                    Destroy(collision.gameObject);
                    GameManager.Instance.ammo += 5;
                    mainGameUIController.RocketGGUIUpdater();
                }
                else if (GameManager.Instance.maxAmmo - GameManager.Instance.ammo < 5)
                {
                    Destroy(collision.gameObject);
                    GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                    mainGameUIController.RocketGGUIUpdater();
                }
            }
        }
    }
}
