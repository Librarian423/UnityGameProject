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

    public virtual void OnHit(float damage) { }

    public virtual void SetStates(float health, float speed, float damage, float moveDistance, float attackDelay)
    {
        this.health = health;
        this.speed = speed;
        this.damage = damage;
        this.movePos = moveDistance;
        this.attackDelay = attackDelay;
    }

    public virtual void Die() { }
}
