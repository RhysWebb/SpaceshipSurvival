using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldCollider : MonoBehaviour
{
    private MainGameUIController mainGameUIController;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement playerMovement;
    private float miniTimer;

    private void Start()
    {
        mainGameUIController = GameObject.Find("Canvas").GetComponent<MainGameUIController>();
        transform.position = player.transform.position;
        miniTimer = 0;
    }
    private void Update()
    {
        if (playerMovement.isShieldActive)
        {
            miniTimer += Time.deltaTime;
        }
        if (miniTimer > GameManager.Instance.shieldMax)
        {
            miniTimer = 0;
            playerMovement.isShieldActive = false;
            gameObject.SetActive(false);
        }
    }
    private void LateUpdate()
    {
        transform.position = player.transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AmmoBombs"))
        {
            if (GameManager.Instance.bombs < GameManager.Instance.maxBombs)
            {
                Destroy(collision.gameObject);
                GameManager.Instance.bombs = GameManager.Instance.maxBombs;
                mainGameUIController.BombGUIUpdater();
                playerMovement.PlayerSoundController(playerMovement.reloadSoundGetter);
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
                    playerMovement.PlayerSoundController(playerMovement.reloadSoundGetter);
                }
                else if (GameManager.Instance.maxAmmo - GameManager.Instance.ammo < 5)
                {
                    Destroy(collision.gameObject);
                    GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
                    mainGameUIController.RocketGGUIUpdater();
                    playerMovement.PlayerSoundController(playerMovement.reloadSoundGetter);
                }
            }


        }
    }
}
