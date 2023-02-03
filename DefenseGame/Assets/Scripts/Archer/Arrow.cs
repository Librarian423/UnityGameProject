using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Arrow : MonoBehaviour
{

    private Rigidbody2D rb;
    public IObjectPool<Arrow> arrowPool { get; set; }
    public float Damage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnBecameInvisible()
    {
        arrowPool.Release(this);
    }

    private void Move()
    {
        Vector2 dir = rb.velocity;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            
            var target = collision.collider.GetComponent<Enemy>();
            if (target != null) 
            {
                //Debug.Log("hit");
                target.OnHit(Damage, transform.position);
            }
            gameObject.SetActive(false);
        }
        
    }
}
