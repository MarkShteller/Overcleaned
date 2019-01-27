
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
        tile.MessReference = this;
        this._currTile = tile;
    }

    public void OnPickUp(Player player)
    {
        this._heldPlayer = player;
        collider.enabled = false;
        this.cleanUp();
        //Attach to players handpoint

    }

    public bool OnTryDrop()
    {
        if (Messtype == MessType.Trash)
        {
            Tile underlyingTile = RoomManager.instance.GetTileAt(transform.position);

            if (underlyingTile.IsEmpty)
            {
                this.dropOn(underlyingTile);
                //Re-enable collider
                collider.enabled = true;
                transform.position = underlyingTile.GetCenter(RoomManager.instance.TileSize);
                transform.rotation = Quaternion.identity;

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
        if(Messtype == MessType.Trash)
        {
            this.OnPickUp(player);
        }
    }
}
