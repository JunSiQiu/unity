using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : livingEnity
{
    public GameObject Weapon;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if(Health <= 0)
        {
            animator.SetTrigger("Death");
            //gameObject.GetComponent<MovementController2D>().enabled = false;
            Destroy(Weapon);
        }
    }
}
