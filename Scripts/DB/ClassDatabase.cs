using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Class", menuName = "Game Data/Class")]
public class ClassData : ScriptableObject
{
    public enum ClassType { Warrior, Mage, Archer, Priest, Rogue }

    [FoldoutGroup("Basic Information", expanded: false)]
    [Tooltip("Name of the class.")]
    public string className;

    [FoldoutGroup("Basic Information", expanded: false)]
    [Tooltip("Type of the class.")]
    [EnumToggleButtons]
    public ClassType classType;

    [FoldoutGroup("Basic Information")]
    [PreviewField(50), Tooltip("Class icon.")]
    public Sprite classIcon;

    [FoldoutGroup("Class Stats")]
    [InlineProperty, HideLabel]
    public ClassStats baseStats;

    [FoldoutGroup("Class Stats")]
    [InlineProperty, HideLabel]
    public ClassGrowthRates growthRates;
}

[Title("Class Base Stats")]
[System.Serializable]
public class ClassStats
{
    [Range(0, 20), Tooltip("The class's bonus health points.")]
    public int health;

    // ... other stats, as in your Unit's Stats class
}

[Title("Class Growth Rates")]
[System.Serializable]
public class ClassGrowthRates
{
    [ValueDropdown("GetPercentages"), Tooltip("The class's bonus growth rate for health.")]
    public int health;

    // ... other growth rates, as in your Unit's GrowthRates class

    private IEnumerable<int> GetPercentages()
    {
        for (int i = 0; i <= 100; i += 5)
        {
            yield return i;
        }
    }
}
