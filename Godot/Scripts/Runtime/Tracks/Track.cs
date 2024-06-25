using Godot;

public partial class Track : Node3D
{
    public GridMap GroundGridMap { get { return _groundGridmap; } }
    public GridMap FlowGridMap { get { return _flowGridMap; } }
    
    [Export] 
    private GridMap _groundGridmap;
    
    [Export] 
    private GridMap _flowGridMap;

    // Convert the player's global position to grid coordinates
    private Vector3I GetPlayerGridCell(Vector3 position)
    {
        Vector3 cellSize = _groundGridmap.CellSize;

        // Debugging output to verify the cell size and position
        //GD.Print("Cell Size: ", cellSize);
        //GD.Print("Player Position: ", position);

        Vector3I cell = new Vector3I(
            Mathf.FloorToInt(position.X / cellSize.X),
            Mathf.FloorToInt(position.Y / cellSize.Y),
            Mathf.FloorToInt(position.Z / cellSize.Z)
        );

        // Debugging output to verify the calculated cell coordinates
        //GD.Print("Grid Cell: ", cell);
        
        return cell;
    }

    // Get the mesh name for the cell the player is in
    public string GetPlayerCurrentMeshName(Vector3 position)
    {
        Vector3I cell = GetPlayerGridCell(position);
        int itemIndex = _groundGridmap.GetCellItem(cell);
        
        // Debugging output to verify the item index
        //GD.Print("Item Index: ", itemIndex); //_mesh_corner_30d_, return float 30;
        
        if (itemIndex != GridMap.InvalidCellItem)
        {
            return _groundGridmap.MeshLibrary.GetItemName(itemIndex);
        }
        else
        {
            return "No mesh in this cell";
        }
    }
}