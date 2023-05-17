using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Game Data/Item Database")]
public class ItemDatabase : ScriptableObject
{
    [InlineEditor]
    public Item[] items;

    public Item GetItem(string name)
    {
        foreach (Item item in items)
        {
            if (item.itemName == name)
                return item;
        }
        return null; // return null if the item isn't found
    }
}

[System.Serializable]
public class Item
{
    [Tooltip("Name of the item.")]
    public string itemName;

    [Tooltip("Description of the item.")]
    [TextArea]
    public string description;

    [Tooltip("The number of uses the item has.")]
    public int uses;

    [Tooltip("The amount of HP the item recovers.")]
    public int hpRecovery;

    [Tooltip("The status effect the item removes.")]
    public StatusEffect statusEffectRemoval;

    // Other properties...
}
