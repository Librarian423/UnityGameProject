using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bow : MonoBehaviour
{
    private Animator animator;
    private Vector2 mousePoint;
    private Vector2 direction;

    public GameObject dotPrefab;
    public GameObject[] dots;
    public int dotCount = 20;

    public float force = 14f;

    //private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dots = new GameObject[dotCount];

        for (int i = 0; i < dotCount; i++)
        {
            dots[i] = Instantiate(dotPrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsFiring", true);
            Debug.Log("down");
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsFiring", false);
            Debug.Log("up");
        }
        mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 bowpos = transform.position;

        direction = mousePoint - bowpos;

        faceMouse();

        for (int i = 0; i < dotCount; i++)
        {
            dots[i].transform.position = DrawArc(i * 0.1f);
        }
    }

    private void faceMouse()
    {
        transform.right = direction;
        float angle = Mathf.Atan2(mousePoint.y - transform.position.y, mousePoint.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private Vector2 DrawArc(float point)
    {
        Vector2 arc = (Vector2)transform.position + (direction.normalized * force * point) +
            0.5f * Physics2D.gravity * (point * point);
        return arc;
    }
}
