using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun3 : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Transform position;

    private GameObject weapon;

    private void Start()
    {
    }

    private void Update()
    {
        //Debug.Log(weaponPrefab.name);
        if (weapon == null)
            ChangeWeapon();

        if (weapon != null && weapon.name != weaponPrefab.name + "(Clone)")
        {
            Destroy(weapon.gameObject);
            ChangeWeapon();
        }
    }

    // 切换当前武器预制体
    private void ChangeWeapon()
    {
        weapon = Instantiate(weaponPrefab);
        weapon.transform.parent = position.transform;
        weapon.transform.localPosition = new Vector3(0, 0, 0);
    }
}
