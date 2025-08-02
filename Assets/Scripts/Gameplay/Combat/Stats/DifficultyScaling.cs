using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty Scaling", menuName = "Stats/Difficulty Scaling")]
public class DifficultyScaling : ScriptableObject
{
    public MonsterDifficulty difficulty;

    [Header("Flat Stat Bonuses")]
    public int flatHealthBonus = 0;
    public int flatArmorBonus = 0;
    public int flatDamageBonus = 0;
    public int flatEnergyBonus = 0;

    [Header("Per-Loop Scaling")]
    public int healthPerLoop = 0;
    public int armorPerLoop = 0;
    public int damagePerLoop = 0;
    public int energyPerLoop = 0;
}