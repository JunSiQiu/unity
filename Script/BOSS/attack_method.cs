using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_method : MonoBehaviour
{
    public float inteval;             // 攻击间隔间隔时间
    public float bulletAngle;
    public int bulletNum;
    public GameObject bulletPrefab1;   // 子弹预制体
    public GameObject bulletPrefab2;   // 子弹预制体
    public Transform target;
    public Animator anim;

    private Transform muzzlePos;      // 枪口位置
    private Vector2 direction;        // 开火位置
    public float timer;
    private bool canShoot = false;

    public GameObject mattack1;
    public GameObject mattack2;

    // Start is called before the first frame update
    void Start()
    {
        muzzlePos = transform.Find("Muzzle");
        timer = inteval;
    }

    // Update is called once per frame
    void Update()
    {
        direction = (new Vector2(target.position.x, target.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
        attackMethod();
    }

    private void attackMethod()
    {
        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                timer = 0;
        }
        else if (canShoot)
        {
            if (timer == 0)
            {
                timer = inteval;
                int attackNum = Random.Range(1, 5);
                Debug.Log(attackNum);
                if (attackNum != 2)
                {
                    attack1();
                    //attack2();
                }
                else if (attackNum == 2)
                    attack2();
            }
        }
    }

    private void attack1()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab1);
        bullet.transform.right = direction;
        bullet.transform.position = muzzlePos.position;

        float angle = Random.Range(-2f, 2f);
        bullet.GetComponent<EnemyBullet>().setSpeed(Quaternion.AngleAxis(angle, Vector3.forward) * direction);
        bullet.GetComponent<Animator>().SetTrigger("isFlying");
        //anim.SetTrigger("isFlying");
    }

    private void attack2()
    {
        int median = bulletNum / 2;
        for (int i = 0; i < bulletNum; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab2);
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
            Debug.Log("进入");
            canShoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("离开");
            canShoot = false;
        }
    }
}
