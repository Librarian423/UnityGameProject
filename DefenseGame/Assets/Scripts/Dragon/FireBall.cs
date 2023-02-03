using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 2f;
    public float Damage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Castle"))
        {
            //Debug.Log("hit");
            var target = collision.gameObject.GetComponent<Castle>();
            if (target != null)
            {
                target.OnHit(Damage);
            }
            Destroy(gameObject);
        }

    }
}
