using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData unitData; // Assign the UnitData asset in the editor.

    public int health; // Current health of this unit.
    public int strength; // Current strength of this unit.
    // Other stats...

    void Start()
    {
        // Initialize the unit's stats based on the unitData.
        health = unitData.baseStats.health;
        strength = unitData.baseStats.strength;
        // Initialize other stats...

        // You could also initialize other properties of the Unit here, like its name, type, etc.
    }

    // Other methods for this Unit...
}
