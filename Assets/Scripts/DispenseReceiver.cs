using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DispenseReceiverType
{
    TrashCan,
    Window,
    Dishwasher,
    Closet
}


public class DispenseReceiver : MonoBehaviour, IInteractable
{
    public DispenseReceiverType type;

    public void interact(Player player)
    {
        //Check if the player is holding a Mess
        if(player._currentHeld != null) {
            Mess mess = player._currentHeld.GetGameObject().GetComponent<Mess>();
            if(mess != null)
            {

                //Check if the receiver is compatible with object held by the plaer
                if (this.type == DispenseReceiverType.TrashCan)
                {
                    if(mess.Messtype == MessType.Trash)
                    {
                        mess.OnDispense();
                    }
                }

                if (this.type == DispenseReceiverType.Dishwasher)
                {
                    if (mess.Messtype == MessType.Dishes)
                    {
                        mess.OnDispense();
                    }
                }
            }
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool CanInteract()
    {
        return true;
    }
}
