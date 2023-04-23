using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float inteval;             // ������ʱ��
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;   // �ӵ�Ԥ����

    protected Transform muzzlePos;      // ǹ��λ��
    protected Vector2 mousePos;         // ���λ��
    protected Vector2 direction;        // ����λ��

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
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);      // ��������ת��Ϊ���������ȡ���λ��
        //Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;  //����ת�Ƕ�
        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector3(-filpY, -filpY, 1);
        else
            transform.localScale = new Vector3(filpY, filpY, 1);

        Shoot();
    }

    protected virtual void Shoot()
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;         // ����ָ�����λ��

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
