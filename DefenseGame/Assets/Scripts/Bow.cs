using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class Bow : MonoBehaviour
{
    private Animator animator;

    //Projectile Track
    private Vector2 mousePoint;
    private Vector2 direction;

    public GameObject dotPrefab;
    public GameObject[] dots;
    public int dotCount = 20;
    public float force = 14f;

    //Arrow Pool
    //private IObjectPool<Arrow> arrowPool;

    public Arrow arrowPrefab;
    public float fireForce = 14f;

    //private void Awake()
    //{
    //    arrowPool = new ObjectPool<Arrow>(
    //        CreateArrow,
    //        OnGet,
    //        OnRelease,
    //        OnDestroyArrow,
    //        maxSize: 10);
    //}

    //private Arrow CreateArrow()
    //{
    //    Arrow arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
    //    arrow.SetPool(arrowPool);
    //    return arrow;
    //}

    //private void OnGet(Arrow arrow)
    //{
    //    arrow.gameObject.SetActive(true);
    //}

    //private void OnRelease(Arrow arrow)
    //{
    //    arrow.gameObject.SetActive(false);
    //}

    //private void OnDestroyArrow(Arrow arrow)
    //{
    //    Destroy(arrow.gameObject);
    //}

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        dots = new GameObject[dotCount];

        for (int i = 0; i < dotCount; i++)
        {
            dots[i] = Instantiate(dotPrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsFiring", true);
            //arrowPool.Get();
            Fire();
            //Debug.Log("down");
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsFiring", false);
            //Debug.Log("up");
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

    private void Fire()
    {
        Arrow arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        arrow.GetComponent<Rigidbody2D>().velocity = transform.up * fireForce;
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
