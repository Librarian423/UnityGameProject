using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MultipleAttack : MonoBehaviour, IMagics
{
    private ParticleSystem particle;
    //private bool isAttak = false;
    public Magics type;
    public float Damage { get; set; }
    public float Speed { get; set; }
    public float AttackTimer { get; set; }

    private float timer = 0f;
    private float durationTimer = 0f;
    public float duration = 10f;

    private List<Enemy> enemies = new List<Enemy>();

    public void Attack(Vector2 pos)
    {
        //Debug.Log("attack");
        //Debug.Log(pos.x);
        switch (type)
        {
            case Magics.FireBlast:
                gameObject.transform.position = pos;
                break;
            case Magics.Electric:
                durationTimer = duration;
                gameObject.transform.position = pos;
                break;
        }
        //gameObject.transform.position = pos;
        gameObject.SetActive(true);
        particle.Play();
        //isAttak = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        durationTimer -= Time.deltaTime;

        if (type == Magics.FireBlast) 
        {
            //timer -= Time.deltaTime;
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
        
        if (type == Magics.Electric && durationTimer <= 0) 
        {

            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) 
        {
            timer = AttackTimer;
            //isAttak = !isAttak;
            //foreach (var enemy in enemies)
            //{
            //    if (enemy == null)
            //    {
            //        enemies.Remove(enemy);
            //        //continue;
            //    }
            //    else
            //    {
            //        enemy.OnHit(Damage, enemy.transform.position);
            //    }
                
            //}
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    enemies[i].OnHit(Damage, enemies[i].transform.position);
                }
                
            }
        }
    }

    private void OnBecameInvisible()
    {
        //isAttak = false;
        gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var target = collision.gameObject.GetComponent<Enemy>();
            if (target != null)
            {
                enemies.Add(target);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("Exit");
            var target = collision.gameObject.GetComponent<Enemy>();
            if (target != null && enemies.Contains(target)) 
            {
                enemies.Remove(target);
            }

        }
    }


    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //if (collision.gameObject.layer != LayerMask.NameToLayer("BossDragon"))
    //    //    return;

    //    //Debug.Log("Stay");


    //    if (isAttak && collision.gameObject.tag == "Enemy") 
    //    {
    //        //Debug.Log("hit");
    //        //timer = AttackTimer;
    //        var target = collision.gameObject.GetComponent<Enemy>();
    //        if (target != null)
    //        {
    //            target.OnHit(Damage, target.transform.position);
    //        }

    //    }
    //}
}
