using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEditor.Rendering;

public class CombatUI : MonoBehaviour
{
    [Header("Manager Reference")]
    public CombatManager combatManager;

    [Header("UI Elements")]
    public Slider playerHPText;
    public TextMeshProUGUI playerEnergyText;
    public Slider monsterHPText;
    public Image playerStunIcon;
    public Image monsterStunIcon;
    public Image playerPoisonIcon;
    public Image monsterPoisonIcon; 

    [Header("Action Buttons")]
    public Button skill1Button;
    public Button skill2Button;
    public Button skill3Button;
    public Button skill4Button;
    public Button skipTurnButton;

    private List<Button> playerSkillButtons;

    void Start()
    {
        playerSkillButtons = new List<Button> { skill1Button, skill2Button, skill3Button, skill4Button };

        skill1Button.onClick.AddListener(() => combatManager.OnSkillPressed(0));
        skill2Button.onClick.AddListener(() => combatManager.OnSkillPressed(1));
        skill3Button.onClick.AddListener(() => combatManager.OnSkillPressed(2));
        skill4Button.onClick.AddListener(() => combatManager.OnSkillPressed(3));
        skipTurnButton.onClick.AddListener(() => combatManager.OnSkipTurnPressed());

        playerStunIcon.gameObject.SetActive(false);
        monsterStunIcon.gameObject.SetActive(false);
        playerPoisonIcon.gameObject.SetActive(false); 
        monsterPoisonIcon.gameObject.SetActive(false);
    }

    public void SetupSkillButtons(List<Skill> playerSkills)
    {
        for (int i = 0; i < playerSkillButtons.Count; i++)
        {
            if (i < playerSkills.Count)
            {
                playerSkillButtons[i].gameObject.SetActive(true);
                string skillName = playerSkills[i].skillName;
                int energyCost = playerSkills[i].energyCost;
                playerSkillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = $"{skillName} ({energyCost})";
            }
            else
            {
                playerSkillButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateAllUI(Combatant player, Combatant monster)
    {
        playerHPText.maxValue = player.currentStats.maxHealth;
        playerHPText.value = player.currentStats.currentHealth;
        
        playerEnergyText.text = $"Energy: {player.currentStats.currentEnergy} / {player.currentStats.maxEnergy}";
        
        monsterHPText.maxValue = monster.currentStats.maxHealth;
        monsterHPText.value = monster.currentStats.currentHealth;

        playerStunIcon.gameObject.SetActive(player.IsStunned());
        monsterStunIcon.gameObject.SetActive(monster.IsStunned());
        
        playerPoisonIcon.gameObject.SetActive(player.IsPoisoned());
        monsterPoisonIcon.gameObject.SetActive(monster.IsPoisoned());
    }

    public void SetActionButtonsInteractable(bool isPlayerTurn, Combatant player)
    {
        for (int i = 0; i < playerSkillButtons.Count; i++)
        {
            if (isPlayerTurn && i < player.skills.Count)
            {
                playerSkillButtons[i].interactable = player.currentStats.currentEnergy >= player.skills[i].energyCost;
            }
            else
            {
                playerSkillButtons[i].interactable = false;
            }
        }

        if (skipTurnButton != null)
        {
            skipTurnButton.interactable = isPlayerTurn;
        }
    }
}