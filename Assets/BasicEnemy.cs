using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public void TakeDamage(int damage)
    {
        // Decrease enemy health by damage amount
    }

    public void OnDeath()
    {
        // Play death animation
    }

    public void OnSpawn()
    {
        health = 1;
    }

    public void OnAttack()
    {
        // Play attack animation
    }
}
