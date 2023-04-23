using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : Weapon
{
    public int bulletNum;
    public float bulletAngle;

    protected override void Fire()
    {
        int median = bulletNum / 2;
        for(int i = 0; i < bulletNum; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
            bullet.transform.position = muzzlePos.position;
            bullet.transform.right = direction;

            if (bulletNum % 2 == 1)
            {
                bullet.GetComponent<Bullet>().setSpeed(Quaternion.AngleAxis(bulletAngle * (i - median), Vector3.forward) * direction);
            }
            else
            {
                bullet.GetComponent<Bullet>().setSpeed(Quaternion.AngleAxis(bulletAngle * (i - median) + bulletAngle / 2, Vector3.forward) * direction);
                bullet.transform.right = direction;
            }
        }
    }
}
