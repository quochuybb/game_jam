using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    [Header("Combatants")]
    public Combatant player;
    public Combatant monster;
    [Header("Scene Setup")]
    public Transform combatCameraPosition;
    public CombatUI combatUI;
    private CombatState state;


    void Start()
    {
        if (combatCameraPosition != null) { Camera.main.transform.position = combatCameraPosition.position; Camera.main.transform.rotation = combatCameraPosition.rotation; }
        SetupBattle();
    }
    public void OnSkillPressed(int skillIndex) { if (state == CombatState.PLAYERTURN) { PlayerAction(skillIndex); } }
    public void OnSkipTurnPressed() {
        if (state != CombatState.PLAYERTURN) return;
        Debug.Log("Player skips turn, gaining extra energy.");
        player.currentStats.currentEnergy += 1;
        player.currentStats.currentEnergy = Mathf.Min(player.currentStats.maxEnergy, player.currentStats.currentEnergy);
        combatUI.UpdateAllUI(player, monster);
        EndPlayerTurn();
    }
    void SetupBattle() {
        state = CombatState.START;
        Debug.Log("--- BATTLE START ---");
        combatUI.SetupSkillButtons(player.skills);
        combatUI.UpdateAllUI(player, monster);
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        player.OnTurnStart();
        combatUI.UpdateAllUI(player, monster);
        
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

    void PlayerAction(int skillIndex) {
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
        monster.OnTurnStart();
        combatUI.UpdateAllUI(player, monster);

        if (monster.IsStunned())
        {
            Debug.Log("Monster is stunned and misses its turn!");
            monster.TickDownStatusEffects(); 
            combatUI.UpdateAllUI(player, monster);
            StartCoroutine(PlayerTurn());
            yield break;
        }

        int numberOfSkills = monster.skills.Count;
        int chosenActionIndex = Random.Range(0, numberOfSkills + 1);
        if (chosenActionIndex < numberOfSkills)
        {
            Skill chosenSkill = monster.skills[chosenActionIndex];
            if (monster.currentStats.currentEnergy >= chosenSkill.energyCost)
            {
                monster.currentStats.currentEnergy -= chosenSkill.energyCost;
                Debug.Log($"Monster used {chosenSkill.name}!");
                ExecuteSkill(chosenSkill, monster, player);
            }
            else
            {
                Debug.Log($"Monster wanted to use {chosenSkill.name}, but couldn't afford it. Skipping turn.");
                monster.currentStats.currentEnergy += 1;
                monster.currentStats.currentEnergy = Mathf.Min(monster.currentStats.maxEnergy, monster.currentStats.currentEnergy);
                combatUI.UpdateAllUI(player, monster);
            }
        }
        else
        {
            Debug.Log("Monster chose to skip its turn to save energy.");
            monster.currentStats.currentEnergy += 1;
            monster.currentStats.currentEnergy = Mathf.Min(monster.currentStats.maxEnergy, monster.currentStats.currentEnergy);
            combatUI.UpdateAllUI(player, monster);
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