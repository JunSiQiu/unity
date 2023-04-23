using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public Animator animator;
    private float rotZ;
    public GameObject Slash;

    protected override void Start()
    {
        //Slash = GetComponentInChildren<GameObject>();
        filpY = transform.localScale.y;
    }

    protected override void Update()
    {
        Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;  //弧度转角度
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        base.Update();
    }

    protected override void Shoot()
    {
        if (timer != 0)
        {
            timer -= Time.deltaTime;
            animator.SetBool("IsAttacking", false);
            if (timer <= 0)
                timer = 0;
        }

        if (Input.GetButton("Fire1"))
        {
            if (timer == 0)
            {
                timer = inteval;
                animator.SetBool("IsAttacking", true);
            }
        }
    }

    protected override void Fire()
    {
        /*GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        //bullet.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        bullet.transform.right = direction;
        bullet.transform.position = muzzlePos.position;
        Destroy(bullet, 0.5f);*/
        Slash.SetActive(true);
        Debug.Log("开始");
    }

    public void endFire()
    {
        Slash.SetActive(false);
        Debug.Log("结束");
    }
}
