using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public MissileSpells missileSpells;

    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * missileSpells.speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.ApplyEffect(missileSpells);

            Destroy(gameObject);
        }
    }
}