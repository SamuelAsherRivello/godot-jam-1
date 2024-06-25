using Godot;

public partial class Track : Node3D
{
    
    public GridMap GroundGridMap { get { return _groundGridMap; } }
    public GridMap FlowGridMap { get { return _flowGridMap; } }
    
    [Export] 
    private GridMap _groundGridMap;
    
    [Export] 
    private GridMap _flowGridMap;
    
    private const bool IsLogging = false;

    // Use for logging
    private Vector3 _lastPosition;
    private Vector3I _lastCell;
    private int _lastItemIndex;
    private float _lastRotationDegrees;
    
  

    private void GDPrint(string message)
    {
        if (IsLogging)
        {
            GD.Print(message);
        }
    }

    // Get the rotation in degrees for the cell at the given position in the given GridMap
    public float GetMeshRotationInDegreesAtPosition(GridMap gridMap, Vector3 position)
    {
        Vector3I cell = GetGridCellAtPosition(gridMap, position);
        Basis basis = gridMap.GetCellItemBasis(cell);

        // Calculate rotation in degrees from the basis
        Vector3 euler = basis.GetEuler();
        float rotation = Mathf.RadToDeg(euler.Y); // Y-axis rotation
        rotation = NormalizeRotation(rotation);

        if (!Mathf.IsEqualApprox(rotation, _lastRotationDegrees))
        {
            GDPrint($"Rotation Degrees {rotation}");
            _lastRotationDegrees = rotation;
        }

        return rotation;
    }

    // Get the mesh name for the cell at the given position in the given GridMap
    public string GetMeshNameAtPosition(GridMap gridMap, Vector3 position)
    {
        Vector3I cell = GetGridCellAtPosition(gridMap, position);
        int itemIndex = gridMap.GetCellItem(cell);

        if (itemIndex != _lastItemIndex)
        {
            GDPrint($"Item Index {itemIndex}");
            GDPrint($"GetItemName {GetItemMesh(gridMap, itemIndex)}");
            _lastItemIndex = itemIndex;
        }

        return GetItemMesh(gridMap, itemIndex);
    }
    
    private Vector3I GetGridCellAtPosition(GridMap gridMap, Vector3 position)
    {
        // Transform the global position into the local space of the GridMap
        Vector3 localPosition = gridMap.ToLocal(position);
        Vector3 cellSize = gridMap.CellSize;

        // Calculate the cell coordinates directly from the local position and cell size
        Vector3I cell = new Vector3I(
            Mathf.FloorToInt(localPosition.X / cellSize.X),
            Mathf.FloorToInt(localPosition.Y / cellSize.Y),
            Mathf.FloorToInt(localPosition.Z / cellSize.Z)
        );

        if (cell != _lastCell)
        {
            GDPrint($"cell {cell}");
            _lastCell = cell;
        }

        return cell;
    }

    // Normalize the rotation to 0, 90, 180, 270 degrees, swapping 90 and 270
    private float NormalizeRotation(float rotation)
    {
        rotation = Mathf.PosMod(rotation, 360.0f);
        if (Mathf.IsEqualApprox(rotation, 0.0f, 5.0f)) return 0.0f;
        if (Mathf.IsEqualApprox(rotation, 90.0f, 5.0f)) return 270.0f; // Swap 90 and 270
        if (Mathf.IsEqualApprox(rotation, 180.0f, 5.0f)) return 180.0f;
        if (Mathf.IsEqualApprox(rotation, 270.0f, 5.0f)) return 90.0f; // Swap 270 and 90
        return rotation; // Default case, should not normally occur if grid items are aligned correctly
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
