using UnityEngine;
using System.Collections.Generic;

public class Combatant : MonoBehaviour
{
    [Header("Component References")]
    public SpriteRenderer spriteRenderer;
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

        // Reset status effects
        stunDuration = 0;
        poisonDuration = 0;
        poisonDamagePerTurn = 0;

        // <<< NEW: Swap the sprite if one is provided
        if (spriteRenderer != null && sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }

    public bool TakeDamage(int damage)
    {
        int damageTaken = Mathf.Max(1, damage - currentStats.currentArmor);
        currentStats.currentHealth -= damageTaken;
        Debug.Log($"{characterName} takes {damageTaken} damage from an attack! Health is now {currentStats.currentHealth}");
        return currentStats.currentHealth <= 0;
    }

    public void Heal(int amount)
    {
        currentStats.currentHealth = Mathf.Min(currentStats.maxHealth, currentStats.currentHealth + amount);
        Debug.Log($"{characterName} heals for {amount}! Health is now {currentStats.currentHealth}");
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
        
        if (currentStats.currentEnergy < currentStats.maxEnergy)
        {
            currentStats.currentEnergy++;
        }
        
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