using UnityEngine;
using System.Collections.Generic;

public class Combatant : MonoBehaviour
{
    [HideInInspector]
    public StatSet currentStats;
    [HideInInspector]
    public List<Skill> skills;
    [HideInInspector]
    public string characterName;

    private int stunDuration = 0;
    private int poisonDuration = 0;
    private int poisonDamagePerTurn = 0;

    public void Setup(StatSet initialStats, List<Skill> initialSkills, string name, Sprite sprite = null)
    {
        currentStats = initialStats;
        skills = initialSkills;
        characterName = name;
        if (this.TryGetComponent<SpriteRenderer>(out var spriteRenderer) && sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
        stunDuration = 0;
        poisonDuration = 0;
        poisonDamagePerTurn = 0;
    }

    public bool TakeDamage(int damage)
    {
        int damageTaken = Mathf.Max(1, damage - currentStats.currentArmor);
        currentStats.currentHealth -= damageTaken;
        Debug.Log($"{characterName} takes {damageTaken} damage from an attack! Health is now {currentStats.currentHealth}");
        return currentStats.currentHealth <= 0;
    }

    // <<< THIS IS THE ONLY METHOD THAT HAS CHANGED >>>
    public void Heal(int amount)
    {
        int finalAmount = amount; // Start with the original healing amount.

        // Check if the combatant is poisoned.
        if (IsPoisoned())
        {
            Debug.Log($"{characterName}'s healing is reduced by poison!");
            // Reduce the healing by 30% (multiply by 0.7) and round to the nearest whole number.
            finalAmount = Mathf.RoundToInt(amount * 0.7f);
        }
        
        // Apply the final (potentially reduced) healing amount.
        currentStats.currentHealth = Mathf.Min(currentStats.maxHealth, currentStats.currentHealth + finalAmount);
        Debug.Log($"{characterName} heals for {finalAmount}! Health is now {currentStats.currentHealth}");
    }

    public void ApplyStun(int turns)
    {
        stunDuration = turns;
        Debug.Log($"{characterName} is stunned for {turns} turn(s)!");
    }

    public void ApplyPoison(int damage, int turns)
    {
        poisonDamagePerTurn = damage;
        poisonDuration = turns;
        Debug.Log($"{characterName} is poisoned, taking {damage} damage for {turns} turns!");
    }

    public bool OnTurnStartAndCheckForDeath()
    {
        Debug.Log($"{characterName} turn start. Energy: {currentStats.currentEnergy}, Health: {currentStats.currentHealth}");
        if (currentStats.currentEnergy < currentStats.maxEnergy) { currentStats.currentEnergy++; }
        if (poisonDuration > 0)
        {
            currentStats.currentHealth -= poisonDamagePerTurn;
            Debug.Log($"{characterName} takes {poisonDamagePerTurn} damage from poison! Health is now {currentStats.currentHealth}.");
        }
        return currentStats.currentHealth <= 0;
    }
    
    public void TickDownStatusEffects()
    {
        if (stunDuration > 0) stunDuration--;
        if (poisonDuration > 0) poisonDuration--;
    }

    public bool IsStunned() { return stunDuration > 0; }
    public bool IsPoisoned() { return poisonDuration > 0; }
}