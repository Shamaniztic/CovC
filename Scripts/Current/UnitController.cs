using UnityEngine;

public class UnitController : MonoBehaviour
{
    public int moveRange = 5;
    public ActionPanelController actionPanelController;
    private GridManager gridManager;
    private bool isInMovementMode = false;

    public Vector3Int CellPosition
    {
        get
        {
            return gridManager.tilemap.WorldToCell(transform.position);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedCell = gridManager.tilemap.WorldToCell(mouseWorldPosition);

            if (clickedCell != CellPosition)
            {
                if (isInMovementMode)
                {
                    if (gridManager.ValidDestinationCell(clickedCell))
                    {
                        MoveToCell(clickedCell);
                        isInMovementMode = false;
                        gridManager.ClearMoveRange();
                        FindObjectOfType<CameraFollow>().StopFollowingUnit();

                        // Show the action panel
                        actionPanelController.ShowPanel(transform.position + new Vector3(1, 0, 0));
                    }
                    else
                    {
                        gridManager.ClearMoveRange();
                        isInMovementMode = false;
                        FindObjectOfType<CameraFollow>().StopFollowingUnit();
                        actionPanelController.HidePanel();
                    }
                }
            }
            else
            {
                if (gridManager.GetUnitOnCell(clickedCell) == this)
                {
                    gridManager.ShowMoveRange(this, moveRange);
                    isInMovementMode = true;
                    FindObjectOfType<CameraFollow>().SetTarget(this.transform);
                }
            }
        }
    }

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            gridManager.RegisterUnit(this);
            Vector3Int cellPosition = gridManager.tilemap.WorldToCell(transform.position);
            Vector3 worldPosition = gridManager.tilemap.GetCellCenterWorld(cellPosition);
            worldPosition.y += 0.25f;
            transform.position = worldPosition;
        }
        else
        {
            Debug.LogError("No GridManager found in the scene. Please add one.");
        }
    }

    void OnMouseDown()
    {
        if (!isInMovementMode)
        {
            gridManager.ShowMoveRange(this, moveRange);
            isInMovementMode = true;
            CameraFollow cameraFollow = Camera.main.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.SetTarget(transform);
            }
        }
    }

    private void MoveToCell(Vector3Int targetCell)
    {
        if (gridManager.ValidDestinationCell(targetCell))
        {
            Vector3 worldPosition = gridManager.tilemap.GetCellCenterWorld(targetCell);
            worldPosition.y += 0.25f;
            transform.position = worldPosition;
            gridManager.RegisterUnit(this);
        }
    }
}
