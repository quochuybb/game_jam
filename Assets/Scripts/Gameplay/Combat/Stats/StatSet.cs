using UnityEngine;

[System.Serializable]
public class StatSet
{
    public int maxHealth;
    public int currentHealth;
    public int currentArmor;
    public int currentDamage;
    public int currentEnergy;
    public int maxEnergy;

    public void Initialize(CharacterStats stats)
    {
        maxHealth = stats.baseHealth;
        currentHealth = stats.baseHealth;
        currentArmor = stats.baseArmor;
        currentDamage = stats.baseDamage;
        maxEnergy = stats.maxEnergy;
        currentEnergy = stats.baseEnergy;
    }
}