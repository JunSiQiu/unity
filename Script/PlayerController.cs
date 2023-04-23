using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public GameObject[] guns;

    private Vector2 moveDirection;
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    private Transform transform;
    private int gunNum=0;               // 枪械下标
    private float filpY;
    protected Vector2 mousePos;         // 鼠标位置
    private bool canPick = false;
    private GameObject go;
    private GameObject tempGO;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        guns[0].SetActive(true);
        filpY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // 移动
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection = moveDirection * moveSpeed;
        animator.SetFloat("moveSpeed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.y));

        // 转向
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);      // 像素坐标转换为世界坐标获取鼠标位置
        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector3(-filpY, filpY, 1);
        else
            transform.localScale = new Vector3(filpY, filpY, 1);

        /*// 武器指向鼠标指针
        Vector3 direction = Input.mousePosition - mainCamera.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       // 计算向量与x轴的夹角
        weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);   // 让武器围绕z轴旋转*/

        // 选择武器
        SwitchGun();

        // 拾取武器
        PickUpWeapon();
    }

    private void FixedUpdate()
    {
        rigidbody2D.AddForce(moveDirection, ForceMode2D.Impulse);
    }

    void SwitchGun()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            guns[gunNum].SetActive(false);
            gunNum--;
            if (gunNum < 0)
            {
                gunNum = guns.Length - 1;
            }
            guns[gunNum].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            guns[gunNum].SetActive(false);
            gunNum++;
            if (gunNum > guns.Length - 1)
            {
                gunNum = 0;
            }
            guns[gunNum].SetActive(true);
        }
    }

    void PickUpWeapon()
    {
        if (canPick && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("canPick");
            guns[2].GetComponent<gun3>().weaponPrefab = go;
            //Destroy(tempGO.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            //Debug.Log(collision.name);

            /// TODO 拾取武器
            canPick = true;
            //collision.GetComponent<pistol>().enabled = true;
            string path = "Weapon/"+collision.name;
            go = Resources.Load<GameObject>(path);
            //Debug.Log(go);
            tempGO = collision.gameObject;
            //Destroy(collision.gameObject);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            canPick = false;
        }
    }
}
