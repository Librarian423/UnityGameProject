using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
