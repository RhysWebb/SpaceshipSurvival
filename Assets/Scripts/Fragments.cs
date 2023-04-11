using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragments : MonoBehaviour
{
    // Variables
    public float time;
    public float timeToLive = 10.0f; 
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeToLive)
            Destroy(gameObject);
    }
}
