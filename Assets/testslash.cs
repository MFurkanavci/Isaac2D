using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testslash : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnEnable()
    {
        anim.SetTrigger("Slash");
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }
}
