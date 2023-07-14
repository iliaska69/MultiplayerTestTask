using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    private Transform _weaponTransform;
    protected Vector3 NormalizedWeaponDirection;
    [SerializeField] protected GameObject bullet;
    
    public abstract void Shoot();

    private void Awake()
    {
        _weaponTransform = transform;
    }

    private void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        var weaponDirection = InputManager.instance.GetMovementDirection();
        if(weaponDirection == Vector2.zero) return;
        NormalizedWeaponDirection = Vector3.Normalize(weaponDirection);
        var angle = Mathf.Atan2(NormalizedWeaponDirection.y, NormalizedWeaponDirection.x) * Mathf.Rad2Deg;
        _weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
