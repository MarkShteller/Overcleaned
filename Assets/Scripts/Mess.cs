
using UnityEngine;

public class Mess: MonoBehaviour, IInteractable
{
    public MessType Messtype { get; set; }

    private Tile _currTile;
    
    public void pickup(PlayerBehavior playerBehavior)
    {
        
    }

    public void dropOn(Tile tile)
    {
        tile.MessReference = this;
        this._currTile = tile;
    }

    public void giveToReceiver(IReceiver receiver)
    {
        
    }


    public void cleanUp()
    {
        this._currTile.MessReference = null;
        this._currTile = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void interact(Player player)
    {

    }
}
