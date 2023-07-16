using UnityEngine;
using Unity.Netcode;

public class Pistol : Weapon
{
    public override void Shoot()
    {
        ShootServerRPC(transform.position, NormalizedWeaponDirection);
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void ShootServerRPC(Vector3 spawnPoint, Vector3 normalizedWeaponDirection)
    {
        // var spawnedBulletGameObject = Instantiate(bullet, spawnPoint + normalizedWeaponDirection, Quaternion.identity);
        // var spawnedBullet = spawnedBulletGameObject.GetComponent<Bullet>();
        // spawnedBullet.StartBullet(normalizedWeaponDirection);
        ShootClientRPC(spawnPoint, normalizedWeaponDirection);
    }

    [ClientRpc]
    private void ShootClientRPC(Vector3 spawnPoint, Vector3 normalizedWeaponDirection)
    {
        Debug.Log("REZINA");
        var spawnedBulletGameObject = Instantiate(bullet, spawnPoint + normalizedWeaponDirection, Quaternion.identity);
        var spawnedBullet = spawnedBulletGameObject.GetComponent<Bullet>();
        spawnedBullet.StartBullet(normalizedWeaponDirection);
    }
}
