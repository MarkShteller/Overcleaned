
using UnityEngine;

public class Mess: MonoBehaviour, IInteractible
{
    public MessType Messtype { get; set; }

    private Tile _currTile;
    
    public void pickup(PlayerBehavior playerBehavior)
    {
        
    }

    public void dropOn(Tile tile)
    {
        tile.AddMess(this);
        this._currTile = tile;
    }

    public void giveToReceiver(IReceiver receiver)
    {
        
    }


    public void cleanUp()
    {
        this._currTile.CleanMess();
        this._currTile = null;
    }
}
