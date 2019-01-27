using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DispenseReceiverType
{
    TrashCan,
    Window,
    Dishwasher,
    Washer,
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
                    if(mess.Messtype == MessType.Trash || mess.Messtype == MessType.Poop)
                    {
                        mess.OnDispense();
                    }
                }

                if (this.type == DispenseReceiverType.Dishwasher)
                {
                    if (mess.Messtype == MessType.Dishes)
                    {
                        mess.OnDispense();
                        GameManager.Instance.AddDishwasherItem();
                    }
                }

                if (this.type == DispenseReceiverType.Washer)
                {
                    if (mess.Messtype == MessType.Clothes)
                    {
                        mess.OnDispense();
                        GameManager.Instance.AddWasherItem();
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
        if (type == DispenseReceiverType.Dishwasher)
        {
            if (GameManager.Instance.isDishwasherFull)
                return false;
        }
        if (type == DispenseReceiverType.Washer)
        {
            if (GameManager.Instance.isWasherFull)
                return false;
        }
        return true;
    }
}
