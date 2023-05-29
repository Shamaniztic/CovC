using UnityEngine;

public class ActionManager : MonoBehaviour
{
    // The unit that is currently acting
    private UnitManager currentUnit;

    // The different states that an action can be in
    private enum ActionState
    {
        Waiting,
        Moving,
        Attacking,
        UsingItem
    }

    // The current state of the action
    private ActionState state;

    private void Start()
    {
        state = ActionState.Waiting;
    }

    public void SetCurrentUnit(UnitManager unit)
    {
        // Check if there is already a unit acting
        if (state != ActionState.Waiting)
        {
            Debug.LogError("An action is already in progress.");
            return;
        }

        currentUnit = unit;
    }

    public void MoveUnit(Vector2 targetPosition)
    {
        if (state != ActionState.Waiting)
        {
            Debug.LogError("An action is already in progress.");
            return;
        }

        state = ActionState.Moving;
        currentUnit.Move(targetPosition);
        state = ActionState.Waiting;
    }

    public void AttackUnit(UnitManager targetUnit)
    {
        if (state != ActionState.Waiting)
        {
            Debug.LogError("An action is already in progress.");
            return;
        }

        state = ActionState.Attacking;
        currentUnit.Attack(targetUnit);
        state = ActionState.Waiting;
    }


    public void UseItem(Item item)
    {
        if (state != ActionState.Waiting)
        {
            Debug.LogError("An action is already in progress.");
            return;
        }

        state = ActionState.UsingItem;
        currentUnit.UseItem(item);
        state = ActionState.Waiting;
    }
}
