using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private float maxHealth;
    public float health = 100;
    public const float recoverAmount = 50f;
    public int cost = 1000;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit(float damage)
    {
        health -= damage;
        UIManager.instance.UpdateHealthBar(health);
        if (health <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    public void Recover()
    {
        if (PropertyManager.instance.Money >= cost) 
        {
            PropertyManager.instance.SetMoney(-cost);
            health += recoverAmount;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            UIManager.instance.UpdateHealthBar(health);
        }
       
    }
}
