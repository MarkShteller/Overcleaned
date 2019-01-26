using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public Tile[] tiles;

    private MessType _state;

    public enum MessType
    {
        WET,
        CLEAN,
        DIRT, 
        DISHES,
        CLOTHES,
        TRASH,
        DOG_SHIT,
        MUD
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

    public float getCleanlinessLevel() 
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
        {    case MessType.CLEAN:
                // base case
                break;
            case MessType.WET:
                this.HandleWetState(playerBehavior);
                break;
            case MessType.DIRT:
                this.HandleDirtState(playerBehavior);
                break;
            case MessType.DISHES:
                this.HandleDishState(playerBehavior);
                break;
            case MessType.CLOTHES:
                this.HandleClothesState(playerBehavior);
                break;
            case MessType.TRASH:
                this.HandleTrashState(playerBehavior);
                break;
            case MessType.DOG_SHIT:
                this.HandleShitState(playerBehavior);
                break;
            case MessType.MUD:
                this.HandleMudState(playerBehavior);
                break;
        }
    }

    public void getTileState()
    {
        
    }

    private void HandleWetState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.getTool().GetType() == ToolType.Rag)
        {
            this.changeTileState(MessType.CLEAN, playerBehavior);
        }
    }

    private void HandleDirtState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.getTool().GetType() == ToolType.Broom)
        {
            this.changeTileState(MessType.CLEAN, playerBehavior);
        }
    }

    private void HandleDishState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.getTool().GetType() == ToolType.Hand)
        {
            this.changeTileState(MessType.CLEAN, playerBehavior);
        }
    }

    private void HandleClothesState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.getTool().GetType() == ToolType.Hand)
        {
            this.changeTileState(MessType.CLEAN, playerBehavior);
        }
    }

    private void HandleTrashState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.getTool().GetType() == ToolType.Hand)
        {
            this.changeTileState(MessType.CLEAN, playerBehavior);
        }
    }

    private void HandleShitState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.getTool().GetType() == ToolType.Hand)
        {
            this.changeTileState(MessType.MUD, playerBehavior);
        }
    }

    private void HandleMudState(PlayerBehavior playerBehavior)
    {
        if (playerBehavior.getTool().GetType() == ToolType.Mop)
        {
            this.changeTileState(MessType.MUD, playerBehavior);
        }
    }
    
}