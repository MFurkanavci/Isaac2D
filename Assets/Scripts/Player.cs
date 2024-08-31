using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public HeroSO hero;

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

    public Animator anim;

    public List<MissileSpells> missileSpells;

    public List<MissileSpells> selectedMissileSpells;

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

    public void InitializePlayerStats(HeroSO hero)
    {


        GameObject model = Instantiate(hero.heroPrefab, transform);

        anim = model.GetComponent<Animator>();

        this.hero = hero;
        maxHealth = hero.maxHealth;
        currentHealth = maxHealth;
        maxMana = hero.maxMana;
        currentMana = maxMana;
        level = 1;
        experience = 0;
        experianceToNextLevel = 100;
        money = 0;

        UIManager.Instance.nextHp = HpPercentage();
        gameObject.AddComponent<PlayerShooting>();
        gameObject.AddComponent<PlayerMovement>();

        gameObject.GetComponent<PlayerMovement>().InitializePlayerMovement(hero);
        gameObject.GetComponent<PlayerShooting>().InitializePlayerShooting(hero);
    }

    private void Start()
    {
        //fps limiter
        Application.targetFrameRate = 60;
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
        return (float)experience / (float)experianceToNextLevel;
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
        ScreenHandling.instance.ShakeScreen();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponent<PlayerShooting>().enabled = false;
        anim.SetTrigger("Death");
        RoomManager.Instance.cleaner.SetActive(true);
        Invoke("Restart", 5f);

    }

    public void Restart()
    {
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            EventRoomManager.Instance.GenerateEvent();
        }
    }
}
