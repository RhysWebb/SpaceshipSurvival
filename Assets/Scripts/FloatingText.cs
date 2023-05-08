using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    // Variables --------------------------------------------
    [SerializeField] private float speed;
    // Variables --------------------------------------------

    

    private void Update()
    {
        transform.Translate(new Vector3(0,1,0) * Time.deltaTime * speed);    
    }


}
