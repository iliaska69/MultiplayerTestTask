using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Game : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    
    private List<Player> _players = new List<Player>();
    private Player _ownerPlayer;

    public static Game instance = null;
    
    public delegate void OwnerCoinsAmountChanged(int coinAmount);
    public event OwnerCoinsAmountChanged ownerCoinsAmountChangedEvent;
    
    public delegate void LevelFinished(Player winnerPlayer, int coinAmount);
    public event LevelFinished levelFinishedEvent;
    
    public delegate void PlayerRegistered();
    public event PlayerRegistered playerRegisteredEvent;

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
        
        NetworkManager.Singleton.OnClientConnectedCallback += OnPlayerConnected;
    }

    private void OnPlayerConnected(ulong clientId)
    {
        if (!IsServer) return;
        var player = Instantiate(playerPrefab);
        player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
    }

    private void ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        response.Approved = true;
    }
    
    

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += OnLoadEvent;
        }
    }

    private void OnLoadEvent(string sceneName, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            var player = Instantiate(playerPrefab);
            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
        }
    }

    public void RegisterPlayer(Player player)
    {
        playerRegisteredEvent.Invoke();
    }
    public void RegisterPlayerAsOwner(Player player)
    {
        _ownerPlayer = player;
        var playerNetworkObjectReference = player.GetNetworkObject();
        RegisterPlayerServerRPC(playerNetworkObjectReference);
        playerRegisteredEvent.Invoke();
    }
    public void DestroyPlayer(Player player)
    {
        var playerNetworkObjectReference = player.GetNetworkObject();
        DestroyPlayerServerRPC(playerNetworkObjectReference);
    }

    [ServerRpc(RequireOwnership = false)]
    private void DestroyPlayerServerRPC(NetworkObjectReference playerNetworkObjectReference)
    {
        playerNetworkObjectReference.TryGet(out NetworkObject playerNetworkObject);
        var player = playerNetworkObject.GetComponent<Player>();

        _players.Remove(player);
        if (_players.Count == 1)
        {
            var winnerPlayer = _players[0];
            _players.Clear();
            GameFinishedClientRpc(winnerPlayer.GetNetworkObject(), winnerPlayer.CoinAmount);
        }
    }

    public void AddCoin(Player player)
    {
        var playerNetworkObjectReference = player.GetNetworkObject();
        AddCoinServerRPC(playerNetworkObjectReference);
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void RegisterPlayerServerRPC(NetworkObjectReference playerNetworkObjectReference)
    {
        playerNetworkObjectReference.TryGet(out NetworkObject playerNetworkObject);
        var player = playerNetworkObject.GetComponent<Player>();
        _players.Add(player);
    }

    [ServerRpc(RequireOwnership = false)]
    private void AddCoinServerRPC(NetworkObjectReference playerNetworkObjectReference)
    {
        playerNetworkObjectReference.TryGet(out NetworkObject playerNetworkObject);
        var player = playerNetworkObject.GetComponent<Player>();
        
        player.AddCoin();
        
        CoinAmountChangedClientRPC(playerNetworkObjectReference, player.CoinAmount);
    }

    [ClientRpc]
    private void CoinAmountChangedClientRPC(NetworkObjectReference playerNetworkObjectReference, int coinAmount)
    {
        playerNetworkObjectReference.TryGet(out NetworkObject playerNetworkObject);
        var player = playerNetworkObject.GetComponent<Player>();
        if(player == _ownerPlayer) ownerCoinsAmountChangedEvent.Invoke(coinAmount);
    }
    
    [ClientRpc]
    private void GameFinishedClientRpc(NetworkObjectReference playerNetworkObjectReference, int coinAmount)
    {
        playerNetworkObjectReference.TryGet(out NetworkObject playerNetworkObject);
        var winnerPlayer = playerNetworkObject.GetComponent<Player>();
        levelFinishedEvent?.Invoke(winnerPlayer, coinAmount);
    }
}
