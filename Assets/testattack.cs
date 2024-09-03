using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testattack : MonoBehaviour
{
    public GameObject testSlash;

    void isAttack()
    {
        testSlash.SetActive(true);
    }
    void isAttackFinish()
    {
        testSlash.SetActive(false);
    }
}
