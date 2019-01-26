using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsClean { get; set; }
    private IInteractible tileObjectReference = null;

    public IInteractible TileObjectReference
    {
        get { return tileObjectReference; }
        set { tileObjectReference = value; }
    }
}
