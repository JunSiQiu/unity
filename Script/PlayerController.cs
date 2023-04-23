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
    private int gunNum=0;               // ǹе�±�
    private float filpY;
    protected Vector2 mousePos;         // ���λ��
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
        // �ƶ�
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveDirection = moveDirection * moveSpeed;
        animator.SetFloat("moveSpeed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.y));

        // ת��
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);      // ��������ת��Ϊ���������ȡ���λ��
        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector3(-filpY, filpY, 1);
        else
            transform.localScale = new Vector3(filpY, filpY, 1);

        /*// ����ָ�����ָ��
        Vector3 direction = Input.mousePosition - mainCamera.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;       // ����������x��ļн�
        weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);   // ������Χ��z����ת*/

        // ѡ������
        SwitchGun();

        // ʰȡ����
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

            /// TODO ʰȡ����
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
