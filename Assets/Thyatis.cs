using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thyatis : MonoBehaviour
{

    void AnimationStart()
    {
        Player.Instance.gameObject.GetComponent<PlayerShooting>().ShootEffect();
    }
}
