using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [Header("Player Stats")]
    public float currentHealth;
    public float maxHealth;
    public float currentMana;
    public float maxMana;

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
        currentHealth = maxHealth;
        currentMana = maxMana;

        UIManager.Instance.nextHp = HpPercentage();
    }
    public float HpPercentage()
    {
        return currentHealth / maxHealth;
    }
    float MpPercentage()
    {
        return currentMana / maxMana;
    }
    public void UseMana(float amount)
    {
        if (amount > currentMana) return;
        currentMana -= amount;
        UIManager.Instance.nextMp = MpPercentage();

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UIManager.Instance.nextHp = HpPercentage();
        if (currentHealth <= 0)
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
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIManager.Instance.nextHp = HpPercentage();
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
