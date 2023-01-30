using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class AreaAttack : MonoBehaviour, IMagics
{
    //private Animator animator;
    private ParticleSystem particle;
    private BoxCollider2D hitBox;
    private bool isAttak = false;
    //private float damage;
    public float Damage { get; set; }

    public void Attack()
    {
       //Debug.Log("attack");
        particle.transform.position = Vector3.zero;
        gameObject.SetActive(true);
        particle.Play();
        isAttak = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        hitBox = GetComponent<BoxCollider2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (particle.isStopped && isAttak)
        {
            gameObject.SetActive(false);
            isAttak = false;
            //Debug.Log("stop");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("enter");
        if (isAttak && collision.gameObject.tag == "Enemy")
        {
            Debug.Log("hit");
            var target = collision.gameObject.GetComponent<Dragon>();
            if (target != null)
            {
                target.OnHit(Damage);
            }
            
        }
    }
}
