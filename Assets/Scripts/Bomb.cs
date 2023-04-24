using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Variables
    public float rotation;
    public float speed = 3.0f;
    public float time;
    public float lifeSpan = 3.0f;
    public GameObject explosion;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, rotation * Time.deltaTime * speed));
        time += Time.deltaTime;
        if (time > lifeSpan)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || !collision.gameObject.CompareTag("Rocket") || !collision.gameObject.CompareTag("PlayerShield"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("PlayerShield"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
