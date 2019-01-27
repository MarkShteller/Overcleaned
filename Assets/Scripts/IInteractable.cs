using UnityEngine;
public interface IInteractable
{
    void interact(Player player);

    Vector3 GetPosition();
}
