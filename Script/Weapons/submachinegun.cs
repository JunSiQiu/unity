using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submachinegun : Weapon
{
    protected override void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.right = direction;
        bullet.transform.position = muzzlePos.position;

        float angle = Random.Range(-5f, 5f);
        bullet.GetComponent<Bullet>().setSpeed(Quaternion.AngleAxis(angle, Vector3.forward) * direction);
    }
}
