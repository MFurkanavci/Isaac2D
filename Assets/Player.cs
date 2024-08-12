using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    [Header("Player Stats")]
    public int health;
    public int maxHealth;
    public int money;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //play death animation
        //play death sound
        //reset player position
        //reset player health
        //reset player money
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
        if (money < 0)
        {
            money = 0;
        }
    }
}
