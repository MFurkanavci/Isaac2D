using System;
using UnityEngine;

public class UpgradeCommon : ScriptableObject
{
    public string
    ID,
    Name,
    Description;

    public Upgradetype upgradetype;

    public Sprite
    SpriteBig,
    SpriteNormal,
    SpriteSmall;

    public bool isUnlocked;
}

public enum Upgradetype
{
    Default,
    Global,
    Charcater,
    Weapon,
    Spell,
    Unique
}



public enum Rarity
{
    unCommon,
    Common,
    Rare,
    Epic,
    Legendary
}
