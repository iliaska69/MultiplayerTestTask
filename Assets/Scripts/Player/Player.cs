using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Weapon weapon;

    private Transform _playerTransform;
    private int _coinAmount;

    public int CoinAmount => _coinAmount;

    void Awake()
    {
        _playerTransform = transform;
    }
    private void Start()
    {
        InputManager.instance.playerTryShootEvent += OnPlayerTryShoot;
        Game.instance.RegisterPlayer(this);
    }

    private void OnDestroy()
    {
        InputManager.instance.playerTryShootEvent -= OnPlayerTryShoot;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        var movementDirection = InputManager.instance.GetMovementDirection();
        Vector3 newVector3Direction = movementDirection;
        _playerTransform.position += newVector3Direction * movementSpeed * Time.deltaTime;
    }

    private void OnPlayerTryShoot()
    {
        if(weapon != null) weapon.Shoot();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
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
        Destroy(gameObject);
    }
}
