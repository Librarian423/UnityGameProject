using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public float health = 100f;
    public float speed = 2f;
    public float damage = 10f;
    public float moveDistance = 10f;

    private float currentDistance;

    // Start is called before the first frame update
    void Start()
    {
        currentDistance = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        //currentDistance = transform.position.x;

        if (transform.position.x > 0f) 
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        
    }
}
