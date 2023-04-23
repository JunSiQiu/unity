using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public GameObject explosionPrefab;
    public LayerMask whatToHit;

    new private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void setSpeed(Vector2 direction)
    {
        rigidbody.velocity = direction * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject exp = ObjectPool.Instance.GetObject(explosionPrefab);
        exp.transform.position = transform.position;

        //Destroy(gameObject);
        ObjectPool.Instance.PushObject(gameObject);

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
