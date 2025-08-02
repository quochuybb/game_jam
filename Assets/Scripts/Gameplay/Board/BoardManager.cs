using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public CombatManager combatManager; // Assign this in the inspector
    public int currentLoop = 1; // You would update this as the player goes around the board

    void Update()
    {
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
    }
}