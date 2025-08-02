using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Game Systems")]
    public CombatManager combatManager; 

    [Header("Test Assets")]
    public Item testItemToAdd; 
    public CharacterStats defaultPlayerStats;
    public Skill testSkillToEquip;

    public int currentLoop = 1; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { combatManager.StartCombat(MonsterDifficulty.Easy, currentLoop); }
        if (Input.GetKeyDown(KeyCode.W)) { combatManager.StartCombat(MonsterDifficulty.Normal, currentLoop); }
        if (Input.GetKeyDown(KeyCode.E)) { combatManager.StartCombat(MonsterDifficulty.Hard, currentLoop); }
        if (Input.GetKeyDown(KeyCode.R)) { combatManager.StartCombat(MonsterDifficulty.MiniBoss, currentLoop); }


        if (Input.GetKeyDown(KeyCode.I)) { combatManager.playerData.AddItem(testItemToAdd); }
        
        if (Input.GetKeyDown(KeyCode.H)) { combatManager.playerData.UpgradeStat("health", 1); }
        if (Input.GetKeyDown(KeyCode.D)) { combatManager.playerData.UpgradeStat("damage", 1); }
        if (Input.GetKeyDown(KeyCode.A)) { combatManager.playerData.UpgradeStat("armor", 1); }
        if (Input.GetKeyDown(KeyCode.S)) { combatManager.playerData.UpgradeStat("energy", 1); }
        
        if (Input.GetKeyDown(KeyCode.O)) { combatManager.playerData.OnPlayerDeath(); }
        if (Input.GetKeyDown(KeyCode.P)) { combatManager.playerData.ResetToDefaults(defaultPlayerStats); }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("--- TEST: SWAPPING SKILL IN SLOT 1 ---");
            combatManager.playerData.EquipNewSkill(testSkillToEquip, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("--- TEST: SWAPPING SKILL IN SLOT 2 ---");
            combatManager.playerData.EquipNewSkill(testSkillToEquip, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("--- TEST: SWAPPING SKILL IN SLOT 3 ---");
            combatManager.playerData.EquipNewSkill(testSkillToEquip, 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("--- TEST: SWAPPING SKILL IN SLOT 4 ---");
            combatManager.playerData.EquipNewSkill(testSkillToEquip, 3);
        }
    }
}