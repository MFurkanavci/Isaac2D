using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName = "Hero")]
public class HeroSO : ScriptableObject
{
    [Header("Hero")]
    public Sprite heroSprite;
    public GameObject heroPrefab;
    
    [Header("Hero Stats")]
    public HeroClass heroClass;
    public string heroName;
    public int maxHealth;
    public int maxMana;

    [Header("Hero Movement")]
    public float moveSpeed;
    public float rollSpeed;
    public float rollDuration;
    public float rollCooldown;
    public float rollInvincibilityDuration;

    [Header("Hero Attack")]
    public float attackSpeed;
    public int attackDamage;
    public float attackRange;
    public float bulletSpeed;


}

public enum HeroClass
{
    Warrior,
    Mage,
    Archer,
    Rogue,
}


