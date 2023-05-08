using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    // Variables --------------------------------------------
    [SerializeField] private float speed;
    [SerializeField] private float textSpeed;
    // Variables --------------------------------------------

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        transform.Translate(new Vector3(0,1,0) * Time.deltaTime * speed);
        if (transform.localScale >= Vector3.one)
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime * textSpeed;

    }

}
