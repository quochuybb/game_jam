using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    [Header("Combatant Prefabs")]
    public Combatant player;
    public Combatant monster;
    [Header("Persistent Player Data")]
    public PlayerData playerData;
    [Header("Combat Setup")]
    public MonsterData monsterToFight;
    public List<DifficultyScaling> difficultySettings;
    public int loopCount = 0;
    [Header("Scene Setup")]
    public Transform combatCameraPosition; 
    public CombatUI combatUI;
    private CombatState state;
    private Dictionary<MonsterDifficulty, DifficultyScaling> difficultyMap;

    void Awake()
    {
        difficultyMap = new Dictionary<MonsterDifficulty, DifficultyScaling>();
        foreach (var setting in difficultySettings)
        {
            difficultyMap[setting.difficulty] = setting;
        }
    }
    void Start()
    {
        if (combatCameraPosition != null) { Camera.main.transform.position = combatCameraPosition.position; Camera.main.transform.rotation = combatCameraPosition.rotation; }
        SetupBattle();
    }
    void SetupBattle()
    {
        StatSet playerStats = CalculatePlayerStats();
        player.Setup(playerStats, playerData.equippedSkills, "Player");
        StatSet monsterStats = CalculateMonsterStats();
        monster.Setup(monsterStats, monsterToFight.skills, monsterToFight.characterName);
        state = CombatState.START;
        Debug.Log($"--- BATTLE START (Loop {loopCount}) ---");
        combatUI.SetupSkillButtons(player.skills);
        combatUI.UpdateAllUI(player, monster);
        StartCoroutine(PlayerTurn());
    }
    StatSet CalculatePlayerStats()
    {
        float totalHealthBonus = 0f, totalArmorBonus = 0f, totalDamageBonus = 0f;
        foreach (Item item in playerData.inventory)
        {
            totalHealthBonus += item.healthBonusPercent;
            totalArmorBonus += item.armorBonusPercent;
            totalDamageBonus += item.damageBonusPercent;
        }
        int finalHealth = Mathf.RoundToInt(playerData.baseHealth * (1f + totalHealthBonus));
        int finalArmor = Mathf.RoundToInt(playerData.baseArmor * (1f + totalArmorBonus));
        int finalDamage = Mathf.RoundToInt(playerData.baseDamage * (1f + totalDamageBonus));
        StatSet stats = new StatSet();
        stats.Initialize(finalHealth, finalArmor, finalDamage, playerData.baseEnergy, playerData.maxEnergy);
        return stats;
    }
    StatSet CalculateMonsterStats()
    {
        StatSet stats = new StatSet();
        DifficultyScaling scaling = difficultyMap[monsterToFight.difficulty];
        int finalHealth = monsterToFight.baseHealth + scaling.flatHealthBonus;
        int finalArmor = monsterToFight.baseArmor + scaling.flatArmorBonus;
        int finalDamage = monsterToFight.baseDamage + scaling.flatDamageBonus;
        int finalEnergy = monsterToFight.baseEnergy + scaling.flatEnergyBonus;
        finalHealth += scaling.healthPerLoop * loopCount;
        finalArmor += scaling.armorPerLoop * loopCount;
        finalDamage += scaling.damagePerLoop * loopCount;
        finalEnergy += scaling.energyPerLoop * (loopCount / 3);
        if (monsterToFight.difficulty == MonsterDifficulty.MiniBoss)
        {
            finalHealth += Mathf.RoundToInt(playerData.baseHealth * 0.8f) + Mathf.RoundToInt(playerData.baseDamage * 0.2f);
            finalArmor += Mathf.RoundToInt(playerData.baseDamage * 0.3f);
            finalDamage += Mathf.RoundToInt(playerData.baseArmor * 0.1f) + Mathf.RoundToInt(playerData.baseHealth * 0.1f);
            finalEnergy += Mathf.RoundToInt(playerData.maxEnergy * 0.3f);
        }
        stats.Initialize(finalHealth, finalArmor, finalDamage, finalEnergy, monsterToFight.maxEnergy);
        return stats;
    }
    public void OnSkillPressed(int skillIndex) { if (state == CombatState.PLAYERTURN) { PlayerAction(skillIndex); } }
    public void OnSkipTurnPressed()
    {
        if (state != CombatState.PLAYERTURN) return;
        Debug.Log("Player skips turn, gaining extra energy.");
        player.currentStats.currentEnergy += 1;
        player.currentStats.currentEnergy = Mathf.Min(player.currentStats.maxEnergy, player.currentStats.currentEnergy);
        combatUI.UpdateAllUI(player, monster);
        EndPlayerTurn();
    }
    IEnumerator PlayerTurn()
    {
        bool diedFromEffects = player.OnTurnStartAndCheckForDeath();
        combatUI.UpdateAllUI(player, monster);
        if (diedFromEffects)
        {
            Debug.Log("Player succumbed to poison at the start of their turn.");
            state = CombatState.LOST;
            EndBattle();
            yield break;
        }
        if (player.IsStunned())
        {
            Debug.Log("Player is stunned and misses their turn!");
            yield return new WaitForSeconds(1f);
            EndPlayerTurn();
            yield break;
        }
        yield return new WaitForSeconds(0.5f);
        state = CombatState.PLAYERTURN;
        combatUI.SetActionButtonsInteractable(true, player);
    }
    void PlayerAction(int skillIndex)
    {
        if (skillIndex >= player.skills.Count) return;
        Skill skill = player.skills[skillIndex];
        if (player.currentStats.currentEnergy < skill.energyCost) { Debug.Log($"Not enough energy! Need {skill.energyCost}, have {player.currentStats.currentEnergy}"); return; }
        player.currentStats.currentEnergy -= skill.energyCost;
        combatUI.UpdateAllUI(player, monster);
        Debug.Log($"Player used {skill.skillName}!");
        ExecuteSkill(skill, player, monster);
        if (monster.currentStats.currentHealth <= 0) { state = CombatState.WON; EndBattle(); }
        else { EndPlayerTurn(); }
    }
    void EndPlayerTurn()
    {
        state = CombatState.MONSTERTURN;
        combatUI.SetActionButtonsInteractable(false, player);
        player.TickDownStatusEffects();
        combatUI.UpdateAllUI(player, monster);
        StartCoroutine(MonsterTurn());
    }

    IEnumerator MonsterTurn()
    {
        Debug.Log("--- MONSTER'S TURN ---");
        yield return new WaitForSeconds(0.5f);
        
        bool diedFromEffects = monster.OnTurnStartAndCheckForDeath();
        combatUI.UpdateAllUI(player, monster);

        if (diedFromEffects)
        {
            Debug.Log("Monster succumbed to poison at the start of its turn.");
            state = CombatState.WON;
            EndBattle();
            yield break;
        }

        if (monster.IsStunned())
        {
            Debug.Log("Monster is stunned and misses its turn!");
            monster.TickDownStatusEffects();
            combatUI.UpdateAllUI(player, monster);
            StartCoroutine(PlayerTurn());
            yield break;
        }

        List<Skill> affordableSkills = new List<Skill>();
        foreach (var s in monster.skills)
        {
            if (monster.currentStats.currentEnergy >= s.energyCost)
            {
                affordableSkills.Add(s);
            }
        }

        bool isLowOnEnergy = monster.currentStats.currentEnergy <= (monster.currentStats.maxEnergy / 2f);

        if (affordableSkills.Count > 0 && !isLowOnEnergy)
        {
            Debug.Log("Monster has high energy and MUST attack.");
            // Pick a random skill from the affordable list and use it.
            Skill skillToUse = affordableSkills[Random.Range(0, affordableSkills.Count)];
            monster.currentStats.currentEnergy -= skillToUse.energyCost;
            Debug.Log($"Monster used {skillToUse.name}!");
            ExecuteSkill(skillToUse, monster, player);
        }
        else
        {

            bool canConsiderSkipping = isLowOnEnergy || affordableSkills.Count == 0;
            
            int totalOptions = affordableSkills.Count;
            if (canConsiderSkipping)
            {
                totalOptions++; 
            }

            int randomChoice = Random.Range(0, totalOptions);

            if (randomChoice < affordableSkills.Count)
            {
                Debug.Log("Monster chose to use a skill despite being low on energy.");
                Skill skillToUse = affordableSkills[randomChoice];
                monster.currentStats.currentEnergy -= skillToUse.energyCost;
                Debug.Log($"Monster used {skillToUse.name}!");
                ExecuteSkill(skillToUse, monster, player);
            }
            else
            {
                if (affordableSkills.Count == 0)
                {
                    Debug.Log("Monster cannot afford any skills and is forced to skip.");
                }
                else
                {
                    Debug.Log("Monster is low on energy and chose to skip.");
                }
                monster.currentStats.currentEnergy += 1;
                monster.currentStats.currentEnergy = Mathf.Min(monster.currentStats.maxEnergy, monster.currentStats.currentEnergy);
                combatUI.UpdateAllUI(player, monster);
            }
        }
        
        monster.TickDownStatusEffects();
        combatUI.UpdateAllUI(player, monster);
        if (player.currentStats.currentHealth <= 0) { state = CombatState.LOST; EndBattle(); }
        else { StartCoroutine(PlayerTurn()); }
    }

    void ExecuteSkill(Skill skill, Combatant user, Combatant target) {
        if (skill.damageMultiplier > 0) { int damage = Mathf.RoundToInt(user.currentStats.currentDamage * skill.damageMultiplier); target.TakeDamage(damage); }
        if (skill.healthToRestore > 0) { user.Heal(skill.healthToRestore); }
        if (skill.stunTurns > 0) { target.ApplyStun(skill.stunTurns); }
        if (skill.poisonTurns > 0) { target.ApplyPoison(skill.poisonDamage, skill.poisonTurns); }
        combatUI.UpdateAllUI(player, monster);
    }
    void EndBattle() {
        combatUI.SetActionButtonsInteractable(false, player);
        if (state == CombatState.WON) { Debug.Log("--- YOU WON THE BATTLE! ---"); }
        else if (state == CombatState.LOST) { Debug.Log("--- YOU WERE DEFEATED ---"); }
    }
}