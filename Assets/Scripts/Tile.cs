using UnityEngine;

interface IPhysicalLocation
{
    float LeftCorner();
    float RightCorner();

}
public class Tile
{
    
    public Tile(int x, int y)
    {
        XLocation = x;
        YLocation = y;
    }
    public bool IsClean { get; set; }
    public Mess TileObjectReference { get; set; }
    public int XLocation { get; set; }
    public int YLocation { get; set; }
    
    public float PhysicalX { get; set; }
    public float PhysicalY { get; set; }
}
