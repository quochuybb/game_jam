using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Persistent Base Stats")]
    public int baseHealth = 5;
    public int baseArmor = 5;
    public int baseDamage = 6;
    public int baseEnergy = 6;
    public int maxEnergy = 6;

    [Header("Current Skills (Size of 4)")]
    public List<Skill> equippedSkills;

    [Header("Current Inventory (Lost on Death)")]
    public List<Item> inventory;

    // --- PUBLIC METHODS FOR SHOP/GAMEPLAY ---

    public void UpgradeStat(string statName, int amount)
    {
        switch (statName.ToLower())
        {
            case "health":
                baseHealth += amount;
                Debug.Log($"Player base health upgraded to {baseHealth}.");
                break;
            case "armor":
                baseArmor += amount;
                Debug.Log($"Player base armor upgraded to {baseArmor}.");
                break;
            case "damage":
                baseDamage += amount;
                Debug.Log($"Player base damage upgraded to {baseDamage}.");
                break;
            default:
                Debug.LogWarning($"Attempted to upgrade unknown stat: {statName}");
                break;
        }
    }

    public void AddItem(Item newItem)
    {
        if (newItem != null)
        {
            inventory.Add(newItem);
            Debug.Log($"Player acquired item: {newItem.itemName}.");
        }
    }
    

    public void OnPlayerDeath()
    {
        inventory.Clear();
        Debug.Log("Player died. Inventory cleared. Base stats and skills remain.");
    }


    public void ResetToDefaults(CharacterStats startingStats)
    {
        if (startingStats == null)
        {
            Debug.LogError("Cannot reset PlayerData: The default starting stats file is missing!");
            return;
        }
        
        baseHealth = startingStats.baseHealth;
        baseArmor = startingStats.baseArmor;
        baseDamage = startingStats.baseDamage;
        baseEnergy = startingStats.baseEnergy;
        maxEnergy = startingStats.maxEnergy;

        equippedSkills = new List<Skill>(startingStats.skills);
        inventory.Clear();
        
        Debug.Log("--- PLAYER DATA FULLY RESET TO DEFAULTS ---");
    }
}