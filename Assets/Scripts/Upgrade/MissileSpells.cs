using System;
using UnityEngine;

[System.Serializable]
public class MissileSpells
{
    public MissileType missileType;

    public int damage;
    public float speed;
    public float lifeTime;
    public float manaCost;
    public float cooldown;
    public float range;
    public float castTime;
    public float radius;
    public float DOTDuration;
    public float freezeDuration;
    public float stunDuration;
    public float slowDuration;
}

public enum MissileType
{
    Fireball,
    Iceball,
    Lightningball,
    MagicMissile
}
