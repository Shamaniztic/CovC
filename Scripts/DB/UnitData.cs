using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Unit", menuName = "Game Data/Unit")]
public class UnitData : ScriptableObject
{
    public enum UnitType { Player, Enemy }
    public enum PowerType { Elemental, Energy, Physical, Spatial, Mystical }

    [FoldoutGroup("Basic Information", expanded: false)]
    [EnumToggleButtons, Tooltip("Specify if the unit is a player or an enemy.")]
    public UnitType unitType;

    [FoldoutGroup("Basic Information", expanded: false)]
    [Tooltip("Name of the unit.")]
    public string unitName;

    [FoldoutGroup("Basic Information", expanded: false)]
    [Tooltip("Type of power the unit uses.")]
    [EnumPaging]
    public PowerType powerType;

    [FoldoutGroup("Basic Information")]
    [PreviewField(50), Tooltip("Portrait image for the unit.")]
    public Sprite portraitImage;

    [FoldoutGroup("Basic Information", expanded: false)]
    [PreviewField(50), Tooltip("Map sprite for the unit.")]
    public Sprite mapSprite;

    [FoldoutGroup("Basic Information", expanded: false)]
    [Tooltip("Biography of the unit.")]
    [TextArea(3, 10)] // First parameter is minimum lines, second is maximum lines.
    public string biography;

    [FoldoutGroup("Unit Stats")]
    [Tooltip("The unit's current level.")]
    [Range(1, 20)] // Set the range according to your game's level range.
    public int level;

    [FoldoutGroup("Unit Stats")]
    [Tooltip("The unit's movement range.")]
    [Range(1, 10)] // Set the range according to your game's movement range.
    public int movementRange;

    [FoldoutGroup("Unit Stats")]
    [Tooltip("The unit's attack range.")]
    [Range(1, 5)] // Set the range according to your game's attack range.
    public int attackRange;

    [FoldoutGroup("Unit Stats")]
    [InlineProperty, HideLabel]
    public Stats baseStats;

    [FoldoutGroup("Unit Stats")]
    [InlineProperty, HideLabel]
    public GrowthRates growthRates;


    [FoldoutGroup("Unit Skills")]
    [Tooltip("List of the unit's skills.")]
    [ListDrawerSettings(ShowFoldout = true, DefaultExpandedState = true)]
    public List<Skill> unitSkills;
}
[Title("Base Stats")]
[System.Serializable]
public class Stats
{
    [Range(1, 40), Tooltip("The unit's health points.")]
    public int health;

    [Range(1, 40), Tooltip("The unit's strength.")]
    public int strength;

    [Range(1, 40), Tooltip("The unit's speed.")]
    public int speed;

    [Range(1, 40), Tooltip("The unit's skill.")]
    public int skill;

    [Range(1, 40), Tooltip("The unit's defense.")]
    public int defense;

    [Range(1, 40), Tooltip("The unit's resistance.")]
    public int resistance;

    [Range(1, 40), Tooltip("The unit's mind.")]
    public int mind;

    [Range(1, 40), Tooltip("The unit's luck.")]
    public int luck;
}
[Title("Growth Rate")]
[System.Serializable]
public class GrowthRates
{
    [ValueDropdown("GetPercentages"), Tooltip("Chance for the unit's health to increase upon leveling up.")]
    public int health;

    [ValueDropdown("GetPercentages"), Tooltip("Chance for the unit's strength to increase upon leveling up.")]
    public int strength;

    [ValueDropdown("GetPercentages"), Tooltip("Chance for the unit's speed to increase upon leveling up.")]
    public int speed;

    [ValueDropdown("GetPercentages"), Tooltip("Chance for the unit's skill to increase upon leveling up.")]
    public int skill;

    [ValueDropdown("GetPercentages"), Tooltip("Chance for the unit's defense to increase upon leveling up.")]
    public int defense;

    [ValueDropdown("GetPercentages"), Tooltip("Chance for the unit's resistance to increase upon leveling up.")]
    public int resistance;

    [ValueDropdown("GetPercentages"), Tooltip("Chance for the unit's mind to increase upon leveling up.")]
    public int mind;

    [ValueDropdown("GetPercentages"), Tooltip("Chance for the unit's luck to increase upon leveling up.")]
    public int luck;

    private IEnumerable<int> GetPercentages()
    {
        for (int i = 0; i <= 100; i += 5)
        {
            yield return i;
        }
    }
}