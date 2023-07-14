using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private Transform _bulletTransform;
    private Vector3 _movementDirection;

    private void Awake()
    {
        _bulletTransform = transform;
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
        var damageableComponent = col.GetComponent<IDamageable>();
        if(damageableComponent == null) return;
        damageableComponent.Damage(damage);
        Destroy(gameObject);
    }

}
