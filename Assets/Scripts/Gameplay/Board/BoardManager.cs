using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Game Systems")]
    public CombatManager combatManager; 

    [Header("Test Assets")]
    public Item testItemToAdd; 
    public CharacterStats defaultPlayerStats;

    public int currentLoop = 1; 

    void Update()
    {
        // --- Combat Triggers ---
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("BoardManager: Player landed on an Easy square. Starting combat...");
            combatManager.StartCombat(MonsterDifficulty.Easy, currentLoop);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("BoardManager: Player landed on a Normal square. Starting combat...");
            combatManager.StartCombat(MonsterDifficulty.Normal, currentLoop);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("BoardManager: Player landed on a Hard square. Starting combat...");
            combatManager.StartCombat(MonsterDifficulty.Hard, currentLoop);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("BoardManager: Player landed on a Hard square. Starting combat...");
            combatManager.StartCombat(MonsterDifficulty.MiniBoss, currentLoop);
        }

        // --- NEW: Player Data Test Triggers ---

        // Press 'I' to Add an Item
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("--- TEST: ADDING ITEM ---");
            combatManager.playerData.AddItem(testItemToAdd);
        }
        
        // Press 'H' to upgrade Health by +1
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("--- TEST: UPGRADING HEALTH ---");
            combatManager.playerData.UpgradeStat("health", 1);
        }

        // Press 'D' to upgrade Damage by +1
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("--- TEST: UPGRADING DAMAGE ---");
            combatManager.playerData.UpgradeStat("damage", 1);
        }
        // Press 'A' to upgrade Armor by +1
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("--- TEST: UPGRADING ARMOR ---");
            combatManager.playerData.UpgradeStat("Armor", 1);
        }
        
        // Press 'R' to Reset Inventory (Simulate Death)
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("--- TEST: RESETTING INVENTORY (DEATH) ---");
            combatManager.playerData.OnPlayerDeath();
        }

        // Press 'F' for Full Reset (New Game)
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("--- TEST: FULLY RESETTING PLAYER DATA ---");
            combatManager.playerData.ResetToDefaults(defaultPlayerStats);
        }
    }
}