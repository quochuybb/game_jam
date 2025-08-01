using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skills/Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    [TextArea]
    public string description;

    [Header("Skill Properties")]
    public int energyCost = 1;

    [Header("Skill Effects")]
    public float damageMultiplier = 1.0f;
    public int healthToRestore = 0;
    public int stunTurns = 0;
}