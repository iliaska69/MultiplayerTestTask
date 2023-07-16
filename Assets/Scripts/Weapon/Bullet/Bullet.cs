using System;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private Transform _bulletTransform;
    private Vector3 _movementDirection;

    private void Awake()
    {
        _bulletTransform = transform;
        Debug.Log("SPAWNED!!" + gameObject.GetInstanceID() + "  " + gameObject.name);
    }

    private void Update()
    {
        if(_movementDirection == Vector3.zero) return;
        _bulletTransform.position += _movementDirection * speed * Time.deltaTime;
    }

    public void StartBullet(Vector3 direction)
    {
        _movementDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("DAMAGED");
        var damageableComponent = col.GetComponent<IDamageable>();
        if(damageableComponent == null) return;
        damageableComponent.Damage(damage);
        Destroy(gameObject);
    }
    

}
