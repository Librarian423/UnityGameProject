using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class AreaAttack : MonoBehaviour, IMagics
{
    private ParticleSystem particle;
    private bool isAttak = false;
    public float Damage { get; set; }

    public void Attack(Vector2 pos)
    {
        gameObject.transform.position = new Vector3(pos.x, 0, 0);
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
        
        if (particle.isStopped && isAttak)
        {
            gameObject.SetActive(false);
            isAttak = false;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("enter");
        if (isAttak && collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("hit");
            var target = collision.gameObject.GetComponent<Enemy>();
            if (target != null)
            {
                target.OnHit(Damage);
            }
            
        }
    }
}
