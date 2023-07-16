using Unity.Netcode;
using UnityEngine;

public class Coin : NetworkBehaviour, ICollectable
{
    public void Collect(Player sender)
    {
        Game.instance.AddCoin(sender);
        DisableCoinServerRPC();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DisableCoinServerRPC()
    {
        DisableCoinClientRPC();
    }
    
    [ClientRpc]
    private void DisableCoinClientRPC()
    {
        gameObject.SetActive(false);
    }
}
