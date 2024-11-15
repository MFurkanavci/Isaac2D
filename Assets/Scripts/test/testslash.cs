using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testslash : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
        }
    }

    //is damage changed? if so, change the damage value
    public void ChangeDamage(int newDamage)
    {
        damage = newDamage;
    }
}
