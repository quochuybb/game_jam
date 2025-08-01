using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public Combatant player;
    public Combatant monster;
    public Transform combatCameraPosition; 

    private CombatState state;
    private List<Button> playerSkillButtons;

    [Header("UI Elements")]
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI playerEnergyText;
    public TextMeshProUGUI monsterHPText;
    public Button skill1Button;
    public Button skill2Button;
    public Button skill3Button;
    public Button skill4Button;
    public Button skipTurnButton;

    void Start()
    {
        playerSkillButtons = new List<Button> { skill1Button, skill2Button, skill3Button, skill4Button };
        if (combatCameraPosition != null) { Camera.main.transform.position = combatCameraPosition.position; Camera.main.transform.rotation = combatCameraPosition.rotation; }
        SetupBattle();
    }
    public void OnSkill1Pressed() { if (state == CombatState.PLAYERTURN) PlayerAction(0); }
    public void OnSkill2Pressed() { if (state == CombatState.PLAYERTURN) PlayerAction(1); }
    public void OnSkill3Pressed() { if (state == CombatState.PLAYERTURN) PlayerAction(2); }
    public void OnSkill4Pressed() { if (state == CombatState.PLAYERTURN) PlayerAction(3); }
    public void OnSkipTurnPressed()
    {
        if (state != CombatState.PLAYERTURN) return;
        Debug.Log("Player skips turn, gaining extra energy.");
        player.currentStats.currentEnergy += 1;
        player.currentStats.currentEnergy = Mathf.Min(player.currentStats.maxEnergy, player.currentStats.currentEnergy);
        UpdateUI();
        EndPlayerTurn();
    }
    void SetupBattle()
    {
        state = CombatState.START;
        Debug.Log("--- BATTLE START ---");
        for (int i = 0; i < playerSkillButtons.Count; i++)
        {
            if (i < player.skills.Count)
            {
                playerSkillButtons[i].gameObject.SetActive(true);
                string skillName = player.skills[i].skillName;
                int energyCost = player.skills[i].energyCost;
                playerSkillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = $"{skillName} ({energyCost})";
            }
            else { playerSkillButtons[i].gameObject.SetActive(false); }
        }
        UpdateUI();
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        bool wasStunned = player.OnTurnStart();
        UpdateUI();
        
        if (wasStunned)
        {
            yield return new WaitForSeconds(1f);
            EndPlayerTurn();
            yield break;
        }

        yield return new WaitForSeconds(0.5f);
        state = CombatState.PLAYERTURN;
        SetAllButtonsInteractable(true);
        Debug.Log("--- PLAYER'S TURN ---");
    }

    void PlayerAction(int skillIndex)
    {
        if (skillIndex >= player.skills.Count) return;
        Skill skill = player.skills[skillIndex];
        if (player.currentStats.currentEnergy < skill.energyCost) { Debug.Log($"Not enough energy! Need {skill.energyCost}, have {player.currentStats.currentEnergy}"); return; }
        player.currentStats.currentEnergy -= skill.energyCost;
        UpdateUI();
        Debug.Log($"Player used {skill.skillName}!");
        ExecuteSkill(skill, player, monster);
        if (monster.currentStats.currentHealth <= 0) { state = CombatState.WON; EndBattle(); }
        else { EndPlayerTurn(); }
    }
    void EndPlayerTurn()
    {
        state = CombatState.MONSTERTURN;
        SetAllButtonsInteractable(false);
        StartCoroutine(MonsterTurn());
    }
    
    IEnumerator MonsterTurn()
    {
        Debug.Log("--- MONSTER'S TURN ---");
        yield return new WaitForSeconds(1f);

        bool wasStunned = monster.OnTurnStart();
        UpdateUI();

        if (wasStunned)
        {
            StartCoroutine(PlayerTurn());
            yield break;
        }

        Skill chosenSkill = null;
        for (int i = 0; i < 10; i++) 
        {
            int randomIndex = Random.Range(0, monster.skills.Count);
            if (monster.currentStats.currentEnergy >= monster.skills[randomIndex].energyCost)
            {
                chosenSkill = monster.skills[randomIndex];
                break;
            }
        }
        if (chosenSkill != null)
        {
            monster.currentStats.currentEnergy -= chosenSkill.energyCost;
            Debug.Log($"Monster used {chosenSkill.skillName}!");
            ExecuteSkill(chosenSkill, monster, player);
        }
        else { Debug.Log("Monster couldn't find a skill to use."); }

        if (player.currentStats.currentHealth <= 0) { state = CombatState.LOST; EndBattle(); }
        else { StartCoroutine(PlayerTurn()); }
    }

    void ExecuteSkill(Skill skill, Combatant user, Combatant target)
    {
        if (skill.damageMultiplier > 0) { int damage = Mathf.RoundToInt(user.currentStats.currentDamage * skill.damageMultiplier); target.TakeDamage(damage); }
        if (skill.healthToRestore > 0) { user.Heal(skill.healthToRestore); }
        if (skill.stunTurns > 0) { target.ApplyStun(skill.stunTurns); }
        UpdateUI();
    }
    void EndBattle()
    {
        SetAllButtonsInteractable(false);
        if (state == CombatState.WON) { Debug.Log("--- YOU WON THE BATTLE! ---"); }
        else if (state == CombatState.LOST) { Debug.Log("--- YOU WERE DEFEATED ---"); }
    }
    void UpdateUI()
    {
        playerHPText.text = $"HP: {player.currentStats.currentHealth} / {player.currentStats.maxHealth}";
        playerEnergyText.text = $"Energy: {player.currentStats.currentEnergy} / {player.currentStats.maxEnergy}";
        monsterHPText.text = $"HP: {monster.currentStats.currentHealth} / {monster.currentStats.maxHealth}";
        if(state == CombatState.PLAYERTURN) { SetAllButtonsInteractable(true); }
    }
    void SetAllButtonsInteractable(bool isInteractable)
    {
        for (int i = 0; i < playerSkillButtons.Count; i++)
        {
            if (isInteractable && i < player.skills.Count) { playerSkillButtons[i].interactable = player.currentStats.currentEnergy >= player.skills[i].energyCost; }
            else { playerSkillButtons[i].interactable = false; }
        }
        if (skipTurnButton != null) { skipTurnButton.interactable = isInteractable; }
    }
}