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


    public void UpgradeStat(string statName, int amount)
    {
        switch (statName.ToLower())
        {
            case "health":
                baseHealth += amount;
                break;
            case "armor":
                baseArmor += amount;
                break;
            case "damage":
                baseDamage += amount;
                break;
        }
    }

    public void EquipNewSkill(Skill newSkill, int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < equippedSkills.Count)
        {
            equippedSkills[slotIndex] = newSkill;
        }
    }

    public void AddItem(Item newItem)
    {
        inventory.Add(newItem);
    }

    public void OnPlayerDeath()
    {
        inventory.Clear();
        Debug.Log("Player died. Inventory cleared.");
    }

    public void ResetToDefaults(CharacterStats startingStats)
    {
        baseHealth = startingStats.baseHealth;
        baseArmor = startingStats.baseArmor;
        baseDamage = startingStats.baseDamage;
        baseEnergy = startingStats.baseEnergy;
        maxEnergy = startingStats.maxEnergy;

        equippedSkills = new List<Skill>(startingStats.skills);
        inventory.Clear();
    }
}