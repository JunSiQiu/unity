using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossbow : Weapon
{
    protected override void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.right = direction;
        bullet.transform.position = muzzlePos.position;
        bullet.GetComponent<Bullet>().setSpeed(direction);
    }
}
