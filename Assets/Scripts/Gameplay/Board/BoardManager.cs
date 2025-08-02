using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Game Systems")]
    public CombatManager combatManager; 

    [Header("Test Assets")]
    public Item testItemToAdd; // Assign an Item SO here to test adding it.
    public CharacterStats defaultPlayerStats; // Assign your original Player_Stats SO here.

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

        // --- NEW: Player Data Test Triggers ---
        
        // Press 'A' to Add an Item
        if (Input.GetKeyDown(KeyCode.A))
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
        
        // Press 'R' to Reset Inventory (Simulate Death)
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("--- TEST: RESETTING INVENTORY (DEATH) ---");
            combatManager.playerData.OnPlayerDeath();
        }

        // Press 'F' for Full Reset (New Game)
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("--- TEST: FULLY RESETTING PLAYER DATA ---");
            combatManager.playerData.ResetToDefaults(defaultPlayerStats);
        }
    }
}