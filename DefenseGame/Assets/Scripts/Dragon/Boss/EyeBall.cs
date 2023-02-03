using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EyeBall : Enemy
{
    //Fireball
    public FireBall fireBallPrefab;

    //private float summonDelay;

    public event Action onDeath;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    { 
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("enter");
        if (collision.gameObject.CompareTag("Castle"))
        {
            //Debug.Log("hit");
            var target = collision.gameObject.GetComponent<Castle>();
            if (target != null)
            {
                target.OnHit(damage);
            }
            Die();
        }
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
