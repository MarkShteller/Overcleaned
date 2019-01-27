using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocusDetector : MonoBehaviour
{

    public Player player;

    private HashSet<IInteractable> _set;

    void Awake()
    {
        _set = new HashSet<IInteractable>();
    }

    void OnTriggerEnter(Collider col) {
        if(col.tag == "interactable")
        {
            IInteractable iObj= col.gameObject.GetComponent<IInteractable>();
            if (!_set.Contains(iObj))
            {
                _set.Add(iObj);
            }
        }

    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "interactable")
        {
            IInteractable iObj = col.gameObject.GetComponent<IInteractable>();
            RemoveObject(iObj);
        }
    }

    public void RemoveObject(IInteractable iObj)
    {
        if (_set.Contains(iObj))
        {
            _set.Remove(iObj);
        }
    }

    public IInteractable GetClosestInteractable()
    {
        Vector3 playerPosition = player.transform.position;
        if(_set.Count == 0)
        {
            return null;
        }

        float distance = 999999.0f;
        IInteractable closest = null;
        foreach (IInteractable interactable in _set)
        {
            try
            {
                if(interactable != null)
                {
                    if (interactable.CanInteract())
                    {
                        float currDistance = (interactable.GetPosition() - playerPosition).magnitude;
                        if(currDistance < distance)
                        {
                            closest = interactable;
                            distance = currDistance;
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        return closest;
    }
}
