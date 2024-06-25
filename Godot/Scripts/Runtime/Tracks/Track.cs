using System;
using Godot;

public partial class Track : Node3D
{
    public GridMap GroundGridMap { get { return _groundGridMap; } }
    public GridMap FlowGridMap { get { return _flowGridMap; } }
    
    [Export] 
    private GridMap _groundGridMap;
    
    [Export] 
    private GridMap _flowGridMap;

    private Vector3 _lastPosition;
    private Vector3I _lastCell;
    private int _lastItemIndex;

    // Convert the global position to grid coordinates for a given GridMap
    private Vector3I GetGridCellAtPosition(GridMap gridMap, Vector3 position)
    {
        // Transform the global position into the local space of the GridMap
        Vector3 localPosition = gridMap.ToLocal(position);

        Vector3 cellSize = gridMap.CellSize;

        // Round the local position
        localPosition = localPosition.Round();

        if (localPosition != _lastPosition)
        {
            GD.Print("Local Position: ", localPosition);
            _lastPosition = localPosition;
        }
       
        Vector3I cell = new Vector3I(
            Mathf.FloorToInt(localPosition.X / cellSize.X),
            Mathf.FloorToInt(localPosition.Y / cellSize.Y),
            Mathf.FloorToInt(localPosition.Z / cellSize.Z)
        );

        if (cell != _lastCell)
        {
            GD.Print("Grid Cell: ", cell);
            _lastCell = cell;
        }
        
        return cell;
    }
    
    private int GetCellItemOrientation(GridMap gridMap, Vector3 position)
    {
        Vector3 localPosition = gridMap.ToLocal(position);

        Vector3 cellSize = gridMap.CellSize;
        
        Vector3I cell = new Vector3I(
            Mathf.FloorToInt(localPosition.X / cellSize.X),
            Mathf.FloorToInt(localPosition.Y / cellSize.Y),
            Mathf.FloorToInt(localPosition.Z / cellSize.Z)
        );

        return gridMap.GetCellItemOrientation(cell);;
    }

    // Get the mesh name for the cell at the given position in the given GridMap
    public string GetMeshNameAtPosition(GridMap gridMap, Vector3 position)
    {
        Vector3I cell = GetGridCellAtPosition(gridMap, position);
        int itemIndex = gridMap.GetCellItem(cell);

        if (itemIndex != _lastItemIndex)
        {
            //GD.Print("Item Index: ", itemIndex);
            //GD.Print("GetItemName: ", GetItemMesh(gridMap, itemIndex));
            _lastItemIndex = itemIndex;
        }

        return GetItemMesh(gridMap, itemIndex);
    }
    

    private string GetItemMesh(GridMap gridMap, int itemIndex)
    {
        if (itemIndex != GridMap.InvalidCellItem)
        {
            return gridMap.MeshLibrary.GetItemMesh(itemIndex).ResourceName;
        }
        else
        {
            return "No mesh in this cell";
        }
    }
}

