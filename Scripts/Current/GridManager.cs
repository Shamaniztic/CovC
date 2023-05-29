using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tilemap impassableTilemap;
    public GameObject moveRangeIndicatorPrefab;
    public GameObject unitPrefab;
    public int gridWidth = 33;
    public int gridHeight = 13;

    private UnitController[,] grid;
    private List<GameObject> moveRangeIndicators = new List<GameObject>();
    private List<Vector3Int> validDestinationCells = new List<Vector3Int>();

    void Awake()
    {
        grid = new UnitController[gridWidth, gridHeight];
    }

    public bool IsCellImpassable(Vector3Int targetCell)
    {
        TileBase tile = impassableTilemap.GetTile(targetCell);
        return tile != null;
    }


    public void RegisterUnit(UnitController unit)
    {
        Vector3Int gridPosition = TilemapToGridPosition(unit.CellPosition);
        grid[gridPosition.x, gridPosition.y] = unit;
        Debug.Log("Registered unit at " + gridPosition);
    }

    public void UnregisterUnit(UnitController unit)
    {
        Vector3Int gridPosition = TilemapToGridPosition(unit.CellPosition);
        grid[gridPosition.x, gridPosition.y] = null;
        Debug.Log("Unregistered unit from " + gridPosition);
    }

    public UnitController GetUnitOnCell(Vector3Int cellPosition)
    {
        Vector3Int gridPosition = TilemapToGridPosition(cellPosition);
        return grid[gridPosition.x, gridPosition.y];
    }

    public Vector3Int TilemapToGridPosition(Vector3Int tilemapPosition)
    {
        return tilemapPosition - tilemap.cellBounds.min;
    }

    public Vector3Int GridToTilemapPosition(Vector3Int gridPosition)
    {
        return gridPosition + tilemap.cellBounds.min;
    }

    private void OnDrawGizmos()
    {
        // Other code...
    }

    public void ShowMoveRange(UnitController unit, int range)
    {
        foreach (GameObject indicator in moveRangeIndicators)
        {
            Destroy(indicator);
        }
        moveRangeIndicators.Clear();
        Debug.Log("Cleared old move range indicators");

        Vector3Int unitGridPosition = TilemapToGridPosition(unit.CellPosition);

        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                int distance = Mathf.Abs(x) + Mathf.Abs(y);
                if (distance <= range)
                {
                    Vector3Int gridPosition = new Vector3Int(unitGridPosition.x + x, unitGridPosition.y + y, 0);
                    Vector3Int tilemapPosition = GridToTilemapPosition(gridPosition);
                    if (InBounds(gridPosition) && !IsCellImpassable(tilemapPosition))
                    {
                        // Add to valid destinations
                        validDestinationCells.Add(gridPosition);
                        Vector3 worldPosition = tilemap.GetCellCenterWorld(tilemapPosition);
                        GameObject indicator = Instantiate(moveRangeIndicatorPrefab, worldPosition, Quaternion.identity);
                        moveRangeIndicators.Add(indicator);
                        Debug.Log("Added move range indicator at " + gridPosition);
                    }
                }
            }
        }
    }


    public void ClearMoveRange()
    {
        foreach (GameObject indicator in moveRangeIndicators)
        {
            Destroy(indicator);
        }
        moveRangeIndicators.Clear();
        validDestinationCells.Clear();
        Debug.Log("Cleared move range indicators and valid destination cells");
    }

    public bool ValidDestinationCell(Vector3Int targetCell)
    {
        bool isValid = validDestinationCells.Contains(TilemapToGridPosition(targetCell));

        if (isValid)
        {
            Vector3Int tilemapPosition = GridToTilemapPosition(TilemapToGridPosition(targetCell));
            TileBase tile = impassableTilemap.GetTile(tilemapPosition);
            if (tile != null)
            {
                // There is an impassable tile at the destination
                isValid = false;
            }
        }

        Debug.Log("Cell " + targetCell + " is " + (isValid ? "valid" : "invalid") + " destination");
        return isValid;
    }


    bool InBounds(Vector3Int position)
    {
        bool inBounds = position.x >= 0 && position.y >= 0 && position.x < gridWidth && position.y < gridHeight;
        Debug.Log("Position " + position + " is " + (inBounds ? "in" : "out of") + " bounds");
        return inBounds;
    }
}
