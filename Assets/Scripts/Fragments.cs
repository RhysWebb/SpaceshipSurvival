using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragments : MonoBehaviour
{
    // Variables
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    float randomOne;
    float randomTwo;
    [SerializeField] float torque;

    private void Awake()
    {
        randomOne = RandomNumber();
        randomTwo = RandomNumber();
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(randomOne, randomTwo) * 0.01f);
        rb.AddTorque(torque, ForceMode2D.Impulse);
    }

    void Update()
    {
        Color color = spriteRenderer.color;
        float colorA = color.a;
        colorA -= 0.1f * Time.deltaTime;
        color.a = colorA;
        spriteRenderer.color = color;
        if (colorA <= 0.1)
            Destroy(gameObject);
    }

    float RandomNumber()
    {
        float selector;
        selector = Random.Range(0.0f, 1.0f);
        if (selector < 0.5)
            return Random.Range(0.1f, 0.2f);
        else
            return Random.Range(-0.1f, -0.2f);
    }
}
