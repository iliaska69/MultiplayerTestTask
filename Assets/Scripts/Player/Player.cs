using System;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Weapon weapon;

    private Transform _playerTransform;
    private int _coinAmount;
    private string _playerName = "ILIASKA";


    public string PlayerName => _playerName;
    public int CoinAmount => _coinAmount;
    
    public delegate void PlayerNameChanged(string newPlayerName);
    public event PlayerNameChanged playerNameChangedEvent;

    void Awake()
    {
        _playerTransform = transform;
    }
    private void Start()
    {
        InputManager.instance.playerTryShootEvent += OnPlayerTryShoot;
        Game.instance.playerRegisteredEvent += OnNewPlayerRegistered;
        
        LoadPlayerName();
        if(!IsLocalPlayer) Game.instance.RegisterPlayer(this);
        else Game.instance.RegisterPlayerAsOwner(this);
    }

    private void OnDestroy()
    {
        InputManager.instance.playerTryShootEvent -= OnPlayerTryShoot;
        Game.instance.playerRegisteredEvent -= OnNewPlayerRegistered;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(!IsOwner) return;
        var movementDirection = InputManager.instance.GetMovementDirection();
        Vector3 newVector3Direction = movementDirection;
        _playerTransform.position += newVector3Direction * movementSpeed * Time.deltaTime;
    }

    private void OnPlayerTryShoot()
    {
        if(!IsOwner) return;
        if(weapon != null) weapon.Shoot();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!IsLocalPlayer) return;
        var collectableItem = col.GetComponent<ICollectable>();
        if(collectableItem == null) return;
        collectableItem.Collect(this);
    }

    public void AddCoin()
    {
        _coinAmount++;
    }

    public void DestroyPlayer()
    {
        Game.instance.DestroyPlayer(this);
        gameObject.SetActive(false);
    }

    private void LoadPlayerName()
    {
        if(!IsLocalPlayer) return;
        _playerName = PlayerPrefs.GetString("PlayerName", "NONE");
        SetPlayerNameServerRPC(_playerName);
    }

    private void OnNewPlayerRegistered()
    {
        LoadPlayerName();
    }
    
    [ServerRpc] private void SetPlayerNameServerRPC(string playerName)
    {
        SetPlayerNameClientRPC(playerName);
    }

    [ClientRpc]
    private void SetPlayerNameClientRPC(string playerName)
    {
        _playerName = playerName;
        playerNameChangedEvent.Invoke(playerName);
    }

    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }
}
