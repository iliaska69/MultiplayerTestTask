using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void Collect(Player sender)
    {
        Game.instance.AddCoin(sender);
        Destroy(gameObject);
    }
}
