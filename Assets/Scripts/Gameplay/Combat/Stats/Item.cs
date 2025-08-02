using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Player/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string description;

    [Header("Percentage Stat Bonuses")]
    [Tooltip("e.g., 0.10 for +10%")]
    public float healthBonusPercent = 0f;
    [Tooltip("e.g., 0.10 for +10%")]
    public float armorBonusPercent = 0f;
    [Tooltip("e.g., 0.10 for +10%")]
    public float damageBonusPercent = 0f;
}