using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float health = 100f;
    protected float speed = 2f;
    protected float damage = 10f;
    protected float movePos = 0f;
    protected float attackDelay = 2f;

    public virtual void OnHit(float damage, Vector2 position) { }

    public virtual void SetStates() { }

    public virtual void Die() { }
}
