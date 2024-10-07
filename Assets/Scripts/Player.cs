using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject dashThing;

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
        StartCoroutine("TakingHit");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator TakingHit()
    {
        // Get the player's current scale
        Vector3 originalScale = Vector3.one;

        // Define the target scale after hit (only reducing the x scale)
        Vector3 targetScale = new Vector3(originalScale.x * 0.5f, originalScale.y, originalScale.z);

        // Time duration for the scaling effect
        float duration = 0.05f; // Adjust this to control the speed
        float elapsedTime = 0f;

        // Scale down over the given duration
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is set to the target scale
        transform.localScale = targetScale;

        // You can add a delay if you want before scaling back to normal
        yield return new WaitForSeconds(0.05f);

        // Scale back to the original scale smoothly
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is set back to the original scale
        transform.localScale = originalScale;
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
