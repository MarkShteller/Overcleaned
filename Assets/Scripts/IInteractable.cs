using UnityEngine;
public interface IInteractable
{
    void interact(Player player);

    bool CanInteract();

    Vector3 GetPosition();
}
