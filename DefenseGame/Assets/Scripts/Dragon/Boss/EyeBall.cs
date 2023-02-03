using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EyeBall : Enemy
{
    //Fireball
    public FireBall fireBallPrefab;
    public DragonData dragonData;
    //private float summonDelay;

    public event Action onDeath;

    // Start is called before the first frame update
    private void Start()
    {
        SetStates();
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

    public override void OnHit(float damage, Vector2 position)
    {
        dragonData.PlayEffect(transform.position);
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public override void SetStates()
    {
        this.health = dragonData.health;
        this.speed = dragonData.speed;
        this.damage = dragonData.damage;
        this.movePos = dragonData.movePos;
        this.attackDelay = dragonData.fireDelay;
    }

    public override void Die()
    {
        if (onDeath != null)
        {
            dragonData.PlayEffect(transform.position);
            onDeath();
        }
        Destroy(gameObject);
    }
}
