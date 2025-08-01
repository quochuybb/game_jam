using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "Stats/Character Stats")]
public class CharacterStats : ScriptableObject
{
    [Header("Base Stats")]
    public string characterName;
    public int baseHealth = 5;
    public int baseArmor = 5;
    public int baseDamage = 6;
    public int baseEnergy = 6;
    public int maxEnergy = 6;
}