using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
}
