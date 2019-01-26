using UnityEngine;


public class Tile
{
    
    public Tile(int x, int y)
    {
        XLocation = x;
        YLocation = y;
    }
    public bool IsClean { get {
            return this.MessReference == null;
        }
    }
    public bool HasPlayer { get
        {
            return this.PlayerReference != null;
        }
    }

    public Mess MessReference { get; set; }
    public Player PlayerReference { get; set; }
    public int XLocation { get; set; }
    public int YLocation { get; set; }
    public Vector3 TopLeft { get; set; }

    public Vector3 GetCenter(float tileSize)
    {
        Vector3 offset = (new Vector3(1, 0, -1)) * tileSize * 0.5f;
        return TopLeft + offset;
    }
}
