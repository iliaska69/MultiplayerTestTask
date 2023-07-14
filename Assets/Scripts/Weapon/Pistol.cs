using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot()
    {
        var spawnedBulletGameObject = Instantiate(bullet, transform.position + NormalizedWeaponDirection, Quaternion.identity);
        var spawnedBullet = spawnedBulletGameObject.GetComponent<Bullet>();
        spawnedBullet.StartBullet(NormalizedWeaponDirection);
    }
}
