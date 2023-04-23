using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemies")
        {
            // TODO (¹¥»÷ÃüÖÐµÐÈË)
            Enemies enemy = collision.transform.gameObject.GetComponent<Enemies>();
            //Debug.Log(enemy);
            enemy.TakeDamage(damage);
            //IDamageable damageable = enemy.transform.GetComponent<IDamageable>();
            //damageable?.TakeDamage(damage);
        }
    }
}
