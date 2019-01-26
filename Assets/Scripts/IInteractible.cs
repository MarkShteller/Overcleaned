public interface IInteractible
{
    void pickup(PlayerBehavior playerBehavior);
    void dropOn(Tile tile);
    void giveToReceiver(IReceiver receiver);
}
