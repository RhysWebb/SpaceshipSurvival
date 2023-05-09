using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    // Variables --------------------------------------------
    [SerializeField] private float speed;
    [SerializeField] private float textSpeed;
    private float counter;
    private TMP_Text textMeshPro;
    // Variables --------------------------------------------

    private void Start()
    {
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);        
        textMeshPro = GetComponent<TMP_Text>();
        textMeshPro.text = $"{GameManager.Instance.reloadTextInput}";

    }

    private void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime * textSpeed;   
        counter += Time.deltaTime;
        if (counter > 2) { Destroy(gameObject); }
    }
}
