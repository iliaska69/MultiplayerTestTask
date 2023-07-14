using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private List<Player> _players = new List<Player>();
    private Player _ownerPlayer;
    
    public static Game instance = null;
    
    public delegate void OwnerCoinsAmountChanged(int coinAmount);
    public event OwnerCoinsAmountChanged ownerCoinsAmountChangedEvent;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    public void RegisterPlayer(Player player)
    {
        _players.Add(player);
        _ownerPlayer = player;
    }

    public void DestroyPlayer(Player player)
    {
        _players.Remove(player);
        if(_players.Count == 1) Debug.Log("!");
    }

    public void AddCoin(Player player)
    {
        player.AddCoin();
        
        if(player == _ownerPlayer) ownerCoinsAmountChangedEvent.Invoke(_ownerPlayer.CoinAmount);
    }
}
