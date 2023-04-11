using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    // Variables
    public float time;
    public float timeToLive = 3.0f;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeToLive)
            Destroy(gameObject);
    }
}
