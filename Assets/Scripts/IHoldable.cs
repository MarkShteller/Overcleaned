using UnityEngine;
public interface IHoldable
{
    void OnPickUp(Player player);

    bool OnTryDrop();

    Player GetPlayer();

    GameObject GetGameObject();
}

