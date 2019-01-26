using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Tile[] tiles;
    public GameObject Floor;

    private void Start()
    {
        BuildTileMap(Floor);
        foreach (var tile in tiles)
        {
            tile.IsClean = true;
        }
    }

    public float GetCleanlinessLevel()
    {
        float cleanLevel = 0;
        foreach (var tile in tiles)
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

    private void BuildTileMap(GameObject floor)
    {
        Vector3 planeSize = this.GetFloorSize(floor);
        Debug.Log(planeSize);

        this.CreateTiles(10, planeSize);
    }

    private Vector3 GetFloorSize(GameObject floor)
    {
        return floor.GetComponent<Renderer>().bounds.size;
    }

    public void CreateTiles(int tileAmount, Vector3 planeSize)
    {
        Vector3 edgeA = transform.position + new Vector3(planeSize.x * transform.localScale.x / 2, 0, 0);
        Vector3 edgeB = transform.position - new Vector3(planeSize.x * transform.localScale.x / 2, 0, 0);
        Vector3 edgeC = transform.position + new Vector3(0, 0, planeSize.z * transform.localScale.y / 2);
        Vector3 edgeD = transform.position - new Vector3(0, 0, planeSize.z * transform.localScale.y / 2);
        Debug.DrawLine(planeSize, planeSize, Color.blue);
    }
    
}