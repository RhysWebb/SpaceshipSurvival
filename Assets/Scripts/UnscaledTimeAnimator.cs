using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscaledTimeAnimator : MonoBehaviour
{
    // Variables --------------------------------------
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.Update(Time.unscaledDeltaTime);
    }
}
