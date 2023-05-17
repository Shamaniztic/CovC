using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Unit Database", menuName = "Game Data/Unit Database")]
public class UnitDatabase : ScriptableObject
{
    [ShowInInspector]
    public List<UnitData> unitList = new List<UnitData>();
}
