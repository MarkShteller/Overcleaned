
using UnityEngine;

public class Mess: MonoBehaviour, IInteractable, IHoldable
{
    public MessType Messtype;

    private Tile _currTile;

    private Player _heldPlayer;

    private Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
    }


    public void dropOn(Tile tile)
    {
        tile.AddMess(this);
        this._currTile = tile;
    }

    public void OnPickUp(Player player)
    {
        this._heldPlayer = player;
        collider.enabled = false;
        this.cleanUp();
        //Attach to players handpoint

        player.HoldGameObject(this);

    }

    public bool OnTryDrop()
    {
        if (Messtype == MessType.Trash || Messtype == MessType.Dishes)
        {
            Tile underlyingTile = RoomManager.instance.GetTileAt(transform.position);

            if (underlyingTile.IsEmpty)
            {
                this.dropOn(underlyingTile);
                this._heldPlayer.DropGameObject(underlyingTile.GetCenter(RoomManager.instance.TileSize));
                this._heldPlayer = null;
                return true;
            }

            //Check if near trashcan

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
        return gameObject;
    }

    public bool IsDispensable()
    {
        return true;
    }


    public void OnDispense()
    {
        this._heldPlayer._currentHeld = null;
        this._heldPlayer = null;
        this.enabled = false;
        Destroy(this.gameObject);
    }


    public bool CanInteract()
    {
        return this._heldPlayer == null;
    }

    public void cleanUp()
    {
        this._currTile.CleanMess();
        this._currTile = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void interact(Player player)
    {
        if(Messtype == MessType.Trash || Messtype == MessType.Dishes)
        {
            if (!player.IsHoldingObject())
            {
                player.FocusDetector.RemoveObject(this);
                this.OnPickUp(player);
            }
            
        }
    }
}
