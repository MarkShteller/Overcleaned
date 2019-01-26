using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public float TileSize;
    public Transform TopLeft;
    public Transform BottomRight;
    private Tile[,] _tiles;
    
    private Vector3 XBasis = new Vector3(1,0,0);
    private Vector3 YBasis = new Vector3(0,0,-1);

    private void Start()
    {
        _tiles = MapBuilder.BuildTileMap(TopLeft.position, BottomRight.position, TileSize);
    }

    private void Update()
    {
        for (int i = 0; i < _tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _tiles.GetLength(1); j++)
            {   
                
                Debug.DrawLine(_tiles[i,j].TopLeft, _tiles[i,j].TopLeft + (XBasis * TileSize), Color.red);
                Debug.DrawLine(_tiles[i,j].TopLeft, _tiles[i,j].TopLeft + (YBasis *TileSize), Color.red);
            }
        }
       
    }

    public float GetCleanlinessLevel()
    {
        float cleanLevel = 0;
        for (int i = 0; i < _tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _tiles.GetLength(1); j++)
            {
                if (_tiles[i,j].IsClean) 
                {
                    cleanLevel++;
                }
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
}