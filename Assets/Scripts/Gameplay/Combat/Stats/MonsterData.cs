using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Monster Data", menuName = "Stats/Monster Data")]
public class MonsterData : ScriptableObject
{
    [Header("Basic Info")]
    public string characterName;
    public MonsterDifficulty difficulty;

    [Header("Base Stats (Unique to this monster)")]
    public int baseHealth = 7;
    public int baseArmor = 2;
    public int baseDamage = 6;
    public int baseEnergy = 6;
    public int maxEnergy = 8;

    [Header("Combat")]
    public List<Skill> skills;
}