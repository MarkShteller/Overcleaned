using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tool : MonoBehaviour, IInteractable, IHoldable
{
    private Tile _currTile;

    private Player _heldPlayer;

    public ToolType type;

    public void interact(Player player)
    {
        if (!player.IsHoldingObject())
        {
            player.FocusDetector.RemoveObject(this);
            this.OnPickUp(player);
        }
    }

    public bool CanInteract()
    {
        return true;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }


    public void OnPickUp(Player player)
    {
        this._heldPlayer = player;

        if(this._currTile != null)
        {
            this._currTile.RemoveTool();
        }

        this._currTile = null;
        
        player.HoldGameObject(this);
    }

    public bool OnTryDrop()
    {
        Tile underlyingTile = RoomManager.instance.GetTileAt(transform.position);
        if (underlyingTile.IsEmpty)
        {
            underlyingTile.AddTool(this);
            this._currTile = underlyingTile;
            this._heldPlayer.DropGameObject(underlyingTile.GetCenter(RoomManager.instance.TileSize));
            this._heldPlayer = null;
            return true;
        }

        return false;
    }

    public Player GetPlayer()
    {
        return this._heldPlayer;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public bool IsDispensable()
    {
        return false;
    }

    public void OnDispense()
    {

    }
}
