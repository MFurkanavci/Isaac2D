using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public int experience;
    public int tempExperience;
    public int experianceToNextLevel = 100;
    float percentageExp;

    public int level;

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
        //fps limiter
        Application.targetFrameRate = 60;

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
    public float ExpPercentage()
    {
        return (float)experience/(float)experianceToNextLevel;
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void AddMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        UIManager.Instance.nextMp = MpPercentage();
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        if (experience >= experianceToNextLevel)
        {
            LevelUp();
        }
        UIManager.Instance.nextExp = ExpPercentage();
    }

    public void LevelUp()
    {
        //TODO: Implement level up
        print("Ding!");
        experianceToNextLevel = 100 + level * 10;
        experience -= experianceToNextLevel;
        level++;
        

    }
}
