using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float inteval;             // 射击间隔时间
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;   // 子弹预制体

    protected Transform muzzlePos;      // 枪口位置
    protected Vector2 mousePos;         // 鼠标位置
    protected Vector2 direction;        // 开火位置

    protected float timer;
    protected float filpY;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        muzzlePos = transform.Find("Muzzle");
        filpY = transform.localScale.y;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);      // 像素坐标转换为世界坐标获取鼠标位置
        //Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;  //弧度转角度
        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector3(-filpY, -filpY, 1);
        else
            transform.localScale = new Vector3(filpY, filpY, 1);

        Shoot();
    }

    protected virtual void Shoot()
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;         // 武器指向鼠标位置

        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                timer = 0;
        }

        if (Input.GetButton("Fire1"))
        {
            if (timer == 0)
            {
                timer = inteval;
                Fire();
            }
        }
    }

    protected virtual void Fire()
    {
        //GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.right = direction;
        bullet.transform.position = muzzlePos.position;

        float angle = Random.Range(-2f, 2f);
        bullet.GetComponent<Bullet>().setSpeed(Quaternion.AngleAxis(angle, Vector3.forward) * direction);
    }
}
