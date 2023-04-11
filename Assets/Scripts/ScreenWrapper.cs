using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ScreenWrapper : MonoBehaviour
{
    // Variables
    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        Vector3 playerPos = transform.position;
        Vector3 playerVelocity = GetComponent<Rigidbody2D>().velocity;
        Vector3 newPos = playerPos + playerVelocity * Time.deltaTime;
        
        if (newPos.x < -screenBounds.x)
            newPos.x = screenBounds.x;
        else if (newPos.x > screenBounds.x)
            newPos.x = -screenBounds.x;

        if (newPos.y < -screenBounds.y)
            newPos.y = screenBounds.y;
        else if (newPos.y > screenBounds.y)
            newPos.y = -screenBounds.y;

        transform.position = newPos;
    }
}




