using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Stats/Player Character Stats")]
public class CharacterStats : ScriptableObject
{
    public string characterName = "Player";

    [Header("Base Stats")]
    public int baseHealth = 5;
    public int baseArmor = 5;
    public int baseDamage = 6;
    public int baseEnergy = 6;
    public int maxEnergy = 6;

    [Header("Player Skills")]
    public List<Skill> skills;
}