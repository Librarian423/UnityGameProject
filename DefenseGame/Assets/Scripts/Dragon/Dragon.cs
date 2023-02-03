using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dragon : Enemy
{
    //Fireball
    public FireBall fireBallPrefab;

    private float fireTimer;

    public event Action onDeath;

    // Start is called before the first frame update
    private void Start()
    {
        fireTimer = attackDelay;
    }

    // Update is called once per frame
    private void Update()
    {
        fireTimer += Time.deltaTime;
        if (transform.position.x >= movePos)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (transform.position.x < movePos && fireTimer >= attackDelay) 
        {
            fireTimer = 0;
            Fire();
        }
    }

    private void Fire()
    {
        FireBall fireBall = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);
        fireBall.Damage = damage;
    }

    public override void OnHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public override void SetStates(float health, float speed, float damage, float moveDistance, float fireDelay)
    {
        this.health = health;
        this.speed = speed;
        this.damage = damage;
        this.movePos = moveDistance;
        this.attackDelay = fireDelay;
    }

    public override void Die()
    {
        if (onDeath != null)
        {
            onDeath();      
        }
        Destroy(gameObject);   
    }
}
