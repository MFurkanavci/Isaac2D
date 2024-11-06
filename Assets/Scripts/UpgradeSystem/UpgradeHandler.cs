using System.Collections.Generic;
using UnityEngine;

public enum UpgradePoolType
{
    Global,
    Weapon,
    Spell,
    Unique
}

public class UpgradeHandler : MonoBehaviour
{
    private Dictionary<UpgradePoolType, List<Upgrade>> upgradePools;


    public void Awake()
    {
        InitializePools();
        LoadGlobalUpgrades();
    }

    private void InitializePools()
    {
        upgradePools = new Dictionary<UpgradePoolType, List<Upgrade>>();
        foreach (UpgradePoolType type in System.Enum.GetValues(typeof(UpgradePoolType)))
        {
            upgradePools[type] = new List<Upgrade>();
        }
    }

    public void LoadGlobalUpgrades()
    {
        // Load all upgrades of type Global from resources
        Upgrade[] globalUpgrades = Resources.LoadAll<Upgrade>("Upgrades/Global");

        foreach (var upgrade in globalUpgrades)
        {
            AddUpgradeToPool(upgrade, UpgradePoolType.Global);
        }

        Debug.Log($"{globalUpgrades.Length} Global upgrades loaded.");
    }

    public void AddUpgradeToPool(Upgrade upgrade, UpgradePoolType poolType)
    {
        if (upgradePools.ContainsKey(poolType))
        {
            upgradePools[poolType].Add(upgrade);
        }
        else
        {
            Debug.LogWarning($"Pool type {poolType} not found.");
        }
    }

    public List<Upgrade> GetUpgradesByType(UpgradePoolType poolType)
    {
        if (upgradePools.ContainsKey(poolType))
        {
            return new List<Upgrade>(upgradePools[poolType]);
        }
        else
        {
            Debug.LogWarning($"Pool type {poolType} not found.");
            return new List<Upgrade>();
        }
    }

    public void PopulateAllUpgradesPool(WeaponSO weapon = null, SpellSO spell = null)
    {
        // Ensure the Weapon pool is populated if a weapon exists
        if (weapon != null)
        {
            foreach (Upgrade upgrade in weapon.upgrades)
            {
                AddUpgradeToPool(upgrade, UpgradePoolType.Weapon);
            }
        }

        // Ensure the Spell pool is populated if a spell exists
        if (spell != null)
        {
            foreach (Upgrade upgrade in spell.upgrades)
            {
                AddUpgradeToPool(upgrade, UpgradePoolType.Spell);
            }
        }

        // Now populate the All Upgrades pool with the available upgrades
        foreach (UpgradePoolType poolType in System.Enum.GetValues(typeof(UpgradePoolType)))
        {
            List<Upgrade> availableUpgrades = GetUpgradesByType(poolType);

            foreach (Upgrade upgrade in availableUpgrades)
            {
                if (!upgrade.preRequesties.Exists(preReq => !preReq.isUnlocked)) // Ensure prerequisites are unlocked
                {
                    // Check max level constraints and only add upgrades that are not at max level
                    if (upgrade.maximumLevel > 0 && upgrade.valueforEachLevel.Count > 0)
                    {
                        AddUpgradeToPool(upgrade, poolType);
                    }
                }
            }
        }
    }

    public void SelectRandomUpgradesOnLevelUp(int count)
    {
        List<Upgrade> randomUpgrades = new List<Upgrade>();
        List<Upgrade> allUpgrades = new List<Upgrade>();

        // Get all upgrades from all pools
        foreach (UpgradePoolType poolType in System.Enum.GetValues(typeof(UpgradePoolType)))
        {
            allUpgrades.AddRange(GetUpgradesByType(poolType));
        }

        allUpgrades = ShuffleList(allUpgrades); // Shuffle all available upgrades

        int choicesCount = Mathf.Min(count, allUpgrades.Count); // Make sure we don't exceed available upgrades

        for (int i = 0; i < choicesCount; i++)
        {
            Upgrade selectedUpgrade = allUpgrades[i];

            // Get a random rarity based on weights
            Rarity randomRarity = GetRandomRarityForUpgrade(selectedUpgrade);

            // Display each upgrade with its assigned rarity in the console
            Debug.Log($"Choice {i + 1}: {selectedUpgrade.Name} (Rarity: {randomRarity})\n" +
                      $"Description: {selectedUpgrade.Description}");

            // Add selected upgrade with assigned rarity to list
            randomUpgrades.Add(selectedUpgrade);
        }
    }


    public void SelectRandomUpgradesOnLevelUp(int count, UpgradePoolType upgradePoolType = UpgradePoolType.Global)
    {
        List<Upgrade> randomUpgrades = new List<Upgrade>();
        List<Upgrade> availableUpgrades = GetUpgradesByType(upgradePoolType);

        // Shuffle the list to randomize selection
        availableUpgrades = ShuffleList(availableUpgrades);

        Debug.Log($"Leveling up! Available choices from {upgradePoolType} pool:");

        int choicesCount = Mathf.Min(count, availableUpgrades.Count); // Make sure we don't exceed available upgrades

        for (int i = 0; i < choicesCount; i++)
        {
            Upgrade selectedUpgrade = availableUpgrades[i];

            // Get a random rarity based on weights
            Rarity randomRarity = GetRandomRarityForUpgrade(selectedUpgrade);

            // Display each upgrade with its assigned rarity in the console
            Debug.Log($"Choice {i + 1}: {selectedUpgrade.Name} (Rarity: {randomRarity})\n" +
                      $"Description: {selectedUpgrade.Description}");

            // Add selected upgrade with assigned rarity to list
            randomUpgrades.Add(selectedUpgrade);
        }
    }


    private Rarity GetRandomRarityForUpgrade(Upgrade upgrade)
    {
        // Calculate the random rarity based on the rarity weights
        int totalWeight = 0;
        foreach (var rarityWeight in upgrade.rarityWeights)
        {
            totalWeight += rarityWeight.weight;
        }

        int randomValue = Random.Range(0, totalWeight);

        foreach (var rarityWeight in upgrade.rarityWeights)
        {
            randomValue -= rarityWeight.weight;
            if (randomValue < 0)
            {
                return rarityWeight.rarity;
            }
        }

        return Rarity.Common; // Default fallback
    }

    private List<Upgrade> ShuffleList(List<Upgrade> list)
    {
        List<Upgrade> shuffledList = new List<Upgrade>(list);
        for (int i = 0; i < shuffledList.Count; i++)
        {
            Upgrade temp = shuffledList[i];
            int randomIndex = Random.Range(i, shuffledList.Count);
            shuffledList[i] = shuffledList[randomIndex];
            shuffledList[randomIndex] = temp;
        }
        return shuffledList;
    }

    internal void PrintAllUpgradePools()
    {
        foreach (var pool in upgradePools)
        {
            UpgradePoolType poolType = pool.Key;
            List<Upgrade> upgrades = pool.Value;

            Debug.Log($"--- {poolType} Pool ---");
            if (upgrades.Count > 0)
            {
                foreach (var upgrade in upgrades)
                {
                    Debug.Log($" ID: {upgrade.ID}, Upgrade: {upgrade.Name}, Rarity: {upgrade.rarity}");
                }
            }
            else
            {
                Debug.Log($"No upgrades available for pool {poolType}.");
            }
        }
    }
}