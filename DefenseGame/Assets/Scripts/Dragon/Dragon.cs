using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dragon : MonoBehaviour
{
    public float health = 100f;
    private float speed = 2f;
    private float damage = 10f;
    private float movePos = 0f;
    private float fireDelay = 2f;

    //private float currentDistance;
    private bool isDead = false;
    private float fireTimer;

    public event Action onDeath;

    //Fireball
    public FireBall fireBallPrefab;

    //Ragdoll
    //public GameObject resourcePrefab;

    // Start is called before the first frame update
    void Start()
    {
        //currentDistance = transform.position.x;
        fireTimer = fireDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        //currentDistance = transform.position.x;
        fireTimer += Time.deltaTime;
        if (transform.position.x >= movePos)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (transform.position.x < movePos && fireTimer >= fireDelay) 
        {
            fireTimer = 0;
            Fire();
        }
        
        
    }

    private void Fire()
    {
        FireBall fireBall = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);
        fireBall.Damage = damage;
        
        //arrow.GetComponent<Rigidbody2D>().velocity = transform.up * fireForce;
    }

    public void OnHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void SetStates(float health, float speed, float damage, float moveDistance, float fireDelay)
    {
        this.health = health;
        this.speed = speed;
        this.damage = damage;
        this.movePos = moveDistance;
        this.fireDelay = fireDelay;
    }

    public void Die()
    {
        if (onDeath != null)
        {
            onDeath();
            //PropertySpawner.instance.CreateCorp(PropertySpawner.DragonType.Gold, transform.position);
        }
        //Instantiate<GameObject>(resourcePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}