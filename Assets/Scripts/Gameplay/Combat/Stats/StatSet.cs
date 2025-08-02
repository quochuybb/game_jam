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

    public void Initialize(MonsterData stats)
    {
        maxHealth = stats.baseHealth;
        currentHealth = stats.baseHealth;
        currentArmor = stats.baseArmor;
        currentDamage = stats.baseDamage;
        maxEnergy = stats.maxEnergy;
        currentEnergy = stats.baseEnergy;
    }

    public void Initialize(int health, int armor, int damage, int energy, int maxEnergy)
    {
        this.maxHealth = health;
        this.currentHealth = health;
        this.currentArmor = armor;
        this.currentDamage = damage;
        this.maxEnergy = maxEnergy;
        this.currentEnergy = energy;
    }
}