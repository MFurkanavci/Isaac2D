using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoraSlash : MonoBehaviour
{
    public Animation animator;
    void Start()
    {

    }
    void OnEnable()
    {
        animator.Play("SlashAnim");
    }
}
