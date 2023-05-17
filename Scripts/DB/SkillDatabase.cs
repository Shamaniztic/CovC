using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Skill Database", menuName = "Game Data/Skill Database")]
public class SkillDatabase : ScriptableObject
{
    public Skill[] skills;

    public Skill GetSkill(string name)
    {
        foreach (Skill skill in skills)
        {
            if (skill.skillName == name)
                return skill;
        }
        return null; // return null if the skill isn't found
    }
}

[System.Serializable]
public class Skill
{
    [Tooltip("Name of the skill.")]
    public string skillName;

    [Tooltip("Description of the skill.")]
    [TextArea]
    public string description;

    [Tooltip("The skill's effect.")]
    public SkillEffect effect;

    // Other properties...
}

public enum SkillEffect
{
    // Enum values corresponding to the different effects a skill can have.
    // You'll need to define these based on your game's mechanics.
    IncreaseAttack,
    IncreaseDefense,
    HealSelf,
    // etc.
}
