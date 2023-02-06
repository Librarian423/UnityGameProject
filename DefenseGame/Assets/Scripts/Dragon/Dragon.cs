using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dragon : Enemy
{
    //Fireball
    public FireBall fireBallPrefab;
    public DragonData dragonData;
    public AudioClip deathClip;
    public AudioClip attackClip;
    private float fireTimer;
    
    public event Action onDeath;

    // Start is called before the first frame update
    private void Start()
    {
        SetStates();
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
        SoundManager.instance.PlayEffect(attackClip);
        FireBall fireBall = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);
        fireBall.Damage = damage;
    }

    public override void OnHit(float damage, Vector2 position)
    {
        dragonData.PlayEffect(position);
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
            SoundManager.instance.PlayEffect(deathClip);
            PropertyManager.instance.SetMoney(dragonData.dropGold);
            onDeath();      
        }
        Destroy(gameObject);   
    }
}
