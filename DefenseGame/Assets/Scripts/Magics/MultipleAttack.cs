using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleAttack : MonoBehaviour, IMagics
{
    private ParticleSystem particle;
    private bool isAttak = false;
    public float Damage { get; set; }
    public float Speed { get; set; }
    public float AttackTimer { get; set; }

    private float timer = 0f;

    public void Attack(Vector2 pos)
    {
        //Debug.Log("attack");
        //Debug.Log(pos.x);
        gameObject.transform.position = pos;
        gameObject.SetActive(true);
        particle.Play();
        isAttak = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttak)
        {
            timer -= Time.deltaTime;
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
    }

    private void OnBecameInvisible()
    {
        isAttak = false;
        gameObject.SetActive(false);
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isAttak && timer <= 0 && collision.gameObject.tag == "Enemy") 
        {
            //Debug.Log("hit");
            timer = AttackTimer;
            var target = collision.gameObject.GetComponent<Enemy>();
            if (target != null)
            {
                target.OnHit(Damage, target.transform.position);
            }

        }
    }
}
