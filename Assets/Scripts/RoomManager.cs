using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Tile[] tiles;

    private MessType _state;

    public enum MessType
    {
        Wet,
        Clean,
        Dirt, 
        Dishes,
        Clothes,
        Trash,
        DogShit,
        Mud
    }

    public enum ToolType
    {
        Broom, 
        Mop,
        Rag,
        Hand
    }

    private void Start()
    {
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

    public void changeTileState(MessType state, PlayerBehavior playerBehavior)
    {
        switch (state)
        {    case MessType.Clean:
                // base case
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
        }
    }

    public void getTileState()
    {
        
    }

    private void HandleWetState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().GetType() == ToolType.Rag)
        {
            this.changeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleDirtState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().GetType() == ToolType.Broom)
        {
            this.changeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleDishState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().GetType() == ToolType.Hand)
        {
            this.changeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleClothesState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().GetType() == ToolType.Hand)
        {
            this.changeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleTrashState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().GetType() == ToolType.Hand)
        {
            this.changeTileState(MessType.Clean, playerBehavior);
        }
    }

    private void HandleShitState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().GetType() == ToolType.Hand)
        {
            this.changeTileState(MessType.Mud, playerBehavior);
        }
    }

    private void HandleMudState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.GetTool().GetType() == ToolType.Mop)
        {
            this.changeTileState(MessType.Mud, playerBehavior);
        }
    }
    
}