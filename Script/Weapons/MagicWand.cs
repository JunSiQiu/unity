using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWand : Weapon
{
    public int bulletNum;
    public float bulletAngle;
    public Transform target;

    private bool canShoot = false;

    protected override void Update()
    {
        direction = (new Vector2(target.position.x,target.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;        
        //direction = new Vector2(target.position.x, target.position.y);
        Shoot();
    }

    protected override void Shoot()
    {
        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                timer = 0;
        }

        if (canShoot)
        {
            if (timer == 0)
            {
                timer = inteval;
                Fire();
            }
        }
    }

    protected override void Fire()
    {
        int median = bulletNum / 2;
        for (int i = 0; i < bulletNum; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
            bullet.transform.right = direction;
            bullet.transform.position = muzzlePos.position;

            if (bulletNum % 2 == 1)
            {
                bullet.GetComponent<EnemyBullet>().setSpeed(Quaternion.AngleAxis(bulletAngle * (i - median), Vector3.forward) * direction);
            }
            else
            {
                bullet.GetComponent<EnemyBullet>().setSpeed(Quaternion.AngleAxis(bulletAngle * (i - median) + bulletAngle / 2, Vector3.forward) * direction);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("½øÈë");
            //gameObject.GetComponentInParent<MovementController2D>().enabled = true;
            canShoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Àë¿ª");
            //gameObject.GetComponentInParent<MovementController2D>().enabled = false;
            canShoot = false;
        }
    }
}
