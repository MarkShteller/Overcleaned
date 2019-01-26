using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject Floor;
    public int TilesX;
    public int TilesY;

    private Tile[] _tiles;

    private void Start()
    {
        BuildTileMap(Floor, TilesX, TilesY);
        foreach (var tile in _tiles)
        {
            tile.IsClean = true;
        }
    }

    public float GetCleanlinessLevel()
    {
        float cleanLevel = 0;
        foreach (var tile in _tiles)
        {
            if (tile.IsClean) 
            {
                cleanLevel++;
            }
        }

        return cleanLevel;
    }

    public void ChangeTileState(MessType state, PlayerBehavior playerBehavior)
    {
        switch (state)
        {    case MessType.Clean:
                // base case
                // trigger nice audio :D
                break;
            case MessType.Wet:
                this.HandleWetState(playerBehavior);
                break;
            case MessType.Dirt:
                this.HandleDirtState(playerBehavior);
                break;
            case MessType.Dishes:
                this.HandleDishState(playerBehavior);
                break;
            case MessType.Clothes:
                this.HandleClothesState(playerBehavior);
                break;
            case MessType.Trash:
                this.HandleTrashState(playerBehavior);
                break;
            case MessType.DogShit:
                this.HandleShitState(playerBehavior);
                break;
            case MessType.Mud:
                this.HandleMudState(playerBehavior);
                break;
            default:
                break;
        }
    }

    public MessType GetTileMess(Tile tile)
    {
        return tile.TileObjectReference.Messtype;
    }

    private void HandleWetState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Rag)
        {
            this.ChangeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleDirtState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Broom)
        {
            this.ChangeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleDishState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Hand)
        {
            this.ChangeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleClothesState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Hand)
        {
            this.ChangeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleTrashState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Hand)
        {
            this.ChangeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleShitState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Hand)
        {
            this.ChangeTileState(MessType.Mud, playerBehavior);
            // mud audio from mud source
        }
    }

    private void HandleMudState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().Tooltype == ToolType.Mop)
        {
            this.ChangeTileState(MessType.Wet, playerBehavior);
            // water audio from audio source
        }
    }
    
    // SHOULD BE IN OWN CLASS ->>>
    // MapBuilder.cs

    private void BuildTileMap(GameObject floor, int x, int y)
    {
        Vector3 planeSize = this.GetFloorSize(floor);
        this.CreateTiles(x, y, planeSize);
    }

    private Vector3 GetFloorSize(GameObject floor)
    {
        return floor.GetComponent<Renderer>().bounds.size;
    }

    private void CreateTiles(int cols, int rows, Vector3 planeSize)
    {
        int i = 0;

        for (var x = 0; x < cols; x++)
        {
            for (var y = 0; y < rows; y++)
            {
                Tile t = new Tile(x, y);
                _tiles[i] = t;
                i++;
            }
        }
    }

    private void CalculatePhysicalLocation(Tile[] tiles, Vector3 planeSize)
    {
        float sizeX = planeSize.x;
        float sizeY = planeSize.y;
        float sizeZ = planeSize.z;

        foreach (var tile in tiles)
        {
            tile.PhysicalX = tile.XLocation/planeSize.x;
            tile.PhysicalY = tile.YLocation/planeSize.z;
        }
    }
    
}