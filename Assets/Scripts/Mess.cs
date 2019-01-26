
using UnityEngine;

public class Mess: MonoBehaviour, IInteractible
{
    public MessType Messtype { get; set; }
    
    public void pickup(PlayerBehavior playerBehavior)
    {
        
    }

    public void dropOn(Tile tile)
    {
        
    }

    public void giveToReceiver(IReceiver receiver)
    {
        
    }
}
