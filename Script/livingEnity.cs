using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class livingEnity : MonoBehaviour
{
    public float Health;

    protected Animator animator;

    protected float nowHealth { get; private set; }
    protected bool IsDead;

    public event Action OnDeath;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health > 0 || IsDead)
            return;

        Debug.Log("TO DEATH");
        IsDead = true;
        OnDeath?.Invoke();
        Destroy(gameObject.GetComponent<Collider2D>());
    }
}
