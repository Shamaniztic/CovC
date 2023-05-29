using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // List of all units participating in the battle, player and enemy alike
    public List<UnitData> allUnits;

    // Queue of units waiting for their turn
    private Queue<UnitData> turnQueue;

    // The unit that is currently taking its turn
    private UnitData currentUnit;

    // State of the battle
    private enum BattleState
    {
        BattleStart,
        PlayerTurnStart,
        PlayerTurn,
        EnemyTurnStart,
        EnemyTurn,
        Won,
        Lost
    }

    private BattleState state;

    private void Start()
    {
        state = BattleState.BattleStart;
        SetupBattle();
    }

    private void SetupBattle()
    {
        // Initialize the turn queue and populate it based on unit speed, etc.
        turnQueue = new Queue<UnitData>();
        // Sort allUnits based on their speed. The fastest unit goes first.
        allUnits.Sort((a, b) => b.baseStats.speed.CompareTo(a.baseStats.speed));

        foreach (UnitData unit in allUnits)
        {
            turnQueue.Enqueue(unit);
        }

        // Display "Defeat all enemies!" animation
        // Transition to PlayerTurnStart after animation ends
        // You will need to implement this
    }

    private void Update()
    {
        switch (state)
        {
            case BattleState.PlayerTurnStart:
                // Display "Player Turn" animation
                // Transition to PlayerTurn after animation ends
                // You will need to implement this
                break;
            case BattleState.PlayerTurn:
                // Let player select a unit to act
                // If all units have acted, transition to EnemyTurnStart
                // You will need to implement this
                break;
            case BattleState.EnemyTurnStart:
                // Display "Enemy Turn" animation
                // Transition to EnemyTurn after animation ends
                // You will need to implement this
                break;
            case BattleState.EnemyTurn:
                // AI selects and acts with its units
                // If all units have acted, transition back to PlayerTurnStart
                // You will need to implement this
                break;
            default:
                break;
        }
    }

    // Call this method when a unit ends its turn.
    public void EndTurn()
    {
        if (state == BattleState.PlayerTurn || state == BattleState.EnemyTurn)
            NextTurn();
    }

    private void NextTurn()
    {
        if (turnQueue.Count == 0)
        {
            // End of round; start a new one.
            foreach (UnitData unit in allUnits)
            {
                turnQueue.Enqueue(unit);
            }
        }

        currentUnit = turnQueue.Dequeue();

        if (currentUnit.unitType == UnitData.UnitType.Player)
        {
            state = BattleState.PlayerTurnStart;
        }
        else
        {
            state = BattleState.EnemyTurnStart;
        }
    }

    // Call this method when a unit is defeated.
    public void UnitDefeated(UnitData unit)
    {
        allUnits.Remove(unit);
        if (turnQueue.Contains(unit))
        {
            Queue<UnitData> newQueue = new Queue<UnitData>();
            while (turnQueue.Count > 0)
            {
                UnitData queuedUnit = turnQueue.Dequeue();
                if (queuedUnit != unit)
                {
                    newQueue.Enqueue(queuedUnit);
                }
            }
            turnQueue = newQueue;
        }

        CheckBattleEnd();
    }

    private void CheckBattleEnd()
    {
        bool allEnemiesDefeated = true;
        bool allPlayersDefeated = true;

        foreach (UnitData unit in allUnits)
        {
            if (unit.unitType == UnitData.UnitType.Player)
            {
                allEnemiesDefeated = false;
            }
            else
            {
                allPlayersDefeated = false;
            }
        }

        if (allEnemiesDefeated)
        {
            // Player won
            state = BattleState.Won;
        }
        else if (allPlayersDefeated)
        {
            // Player lost
            state = BattleState.Lost;
        }
    }
}
