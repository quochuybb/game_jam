using UnityEngine;
using System.Collections.Generic;

public class Combatant : MonoBehaviour
{
    public CharacterStats statsData;
    public List<Skill> skills;

    [HideInInspector]
    public StatSet currentStats;
    private int stunDuration = 0;

    void Awake()
    {
        currentStats = new StatSet();
        currentStats.Initialize(statsData);
    }

    public bool TakeDamage(int damage)
    {
        int damageTaken = Mathf.Max(1, damage - currentStats.currentArmor);
        currentStats.currentHealth -= damageTaken;
        Debug.Log($"{statsData.characterName} takes {damageTaken} damage! Health is now {currentStats.currentHealth}");
        return currentStats.currentHealth <= 0;
    }

    public void Heal(int amount)
    {
        currentStats.currentHealth = Mathf.Min(currentStats.maxHealth, currentStats.currentHealth + amount);
        Debug.Log($"{statsData.characterName} heals for {amount}! Health is now {currentStats.currentHealth}");
    }

    public void ApplyStun(int turns)
    {
        stunDuration = turns;
        Debug.Log($"{statsData.characterName} is stunned for {turns} turn(s)!");
    }

    public bool OnTurnStart()
    {
        Debug.Log($"{statsData.characterName} turn start. Energy: {currentStats.currentEnergy}, Health: {currentStats.currentHealth}");

        if (currentStats.currentEnergy < currentStats.maxEnergy)
        {
            currentStats.currentEnergy++;
        }

        if (stunDuration > 0)
        {
            Debug.Log($"{statsData.characterName} is stunned and misses their turn!");
            stunDuration--;
            return true;
        }

        return false;
    }
    
    public bool IsStunned()
    {
        return stunDuration > 0;
    }
}