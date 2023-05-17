using UnityEngine;

[CreateAssetMenu(fileName = "New Status Effect Database", menuName = "Game Data/Status Effect Database")]
public class StatusEffectDatabase : ScriptableObject
{
    public StatusEffect[] statusEffects;

    public StatusEffect GetStatusEffect(string name)
    {
        foreach (StatusEffect statusEffect in statusEffects)
        {
            if (statusEffect.statusEffectName == name)
                return statusEffect;
        }
        return null; // return null if the status effect isn't found
    }
}

[System.Serializable]
public class StatusEffect
{
    [Tooltip("Name of the status effect.")]
    public string statusEffectName;

    [Tooltip("Description of the status effect.")]
    [TextArea]
    public string description;

    [Tooltip("Is this effect beneficial (buff) or harmful (debuff)?")]
    public EffectType effectType;

    [Tooltip("Duration of the status effect in turns.")]
    public int duration;

    [Tooltip("The effect's impact on stats.")]
    public StatImpact statImpact;
}

public enum EffectType
{
    Buff,
    Debuff
}

[System.Serializable]
public class StatImpact
{
    [Tooltip("Impact on the unit's health points.")]
    public int healthImpact;

    [Tooltip("Impact on the unit's strength.")]
    public int strengthImpact;

    [Tooltip("Impact on the unit's speed.")]
    public int speedImpact;

    [Tooltip("Impact on the unit's skill.")]
    public int skillImpact;

    [Tooltip("Impact on the unit's defense.")]
    public int defenseImpact;

    [Tooltip("Impact on the unit's resistance.")]
    public int resistanceImpact;

    [Tooltip("Impact on the unit's mind.")]
    public int mindImpact;

    [Tooltip("Impact on the unit's luck.")]
    public int luckImpact;
}
