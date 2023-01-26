using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Arrow : MonoBehaviour
{

    private Rigidbody2D rb;
    private IObjectPool<Arrow> arrowPool;

    public float fireForce = 14f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //rb.velocity = transform.right * fireForce;
    }

    public void SetPool(IObjectPool<Arrow> pool)
    {
        arrowPool = pool;
    }

    //private void OnBecameInvisible()
    //{
    //    arrowPool.Release(this);
    //}

    private void Move()
    {
        Vector2 dir = rb.velocity;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
