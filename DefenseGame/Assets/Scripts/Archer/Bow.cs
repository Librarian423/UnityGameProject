using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.XR;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Bow : MonoBehaviour
{
    private Animator animator;

    //Projectile Track
    private Vector2 mousePoint;
    private Vector2 direction;

    [Header("Projectile line")]
    public GameObject dotPrefab;
    public GameObject[] dots;
    public int dotCount = 20;

    [Header("Arrow details")]
    public float force = 14f;
    //Fire timer
    public float fireDelay = 0.2f;
    private float timer = 0.2f;
    public float damage = 10f;

    //Arrow Pool
    [Header("Arrow Pool")]
    public int maxPoolSize = 500;
    public int stackDefaultCapacity = 100;
    public Arrow arrowPrefab;
    public int arrowCount = 2;
    [Range(0, 1)] public float gap;

    private enum Level
    {
        One,
        Two,
        Three,
    }
    private Level level;

    private IObjectPool<Arrow> arrowPool;
    public IObjectPool<Arrow> ArrowPool
    {
        get
        {
            if (arrowPool == null)
                arrowPool =
                    new ObjectPool<Arrow>(
                        CreateArrow,
                        OnTakeFromPool,
                        OnReturnedToPool,
                        OnDestroyPoolObject,
                        true,
                        stackDefaultCapacity,
                        maxPoolSize);
            return arrowPool;
        }
    }

    private Arrow CreateArrow()
    {
        Arrow arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        arrow.arrowPool = ArrowPool;
        return arrow;
    }

    private void OnTakeFromPool(Arrow arrow)
    {
        arrow.gameObject.SetActive(true);
        
    }

    private void OnReturnedToPool(Arrow arrow)
    {
        arrow.gameObject.SetActive(false);
        
    }

    private void OnDestroyPoolObject(Arrow arrow)
    {
        Destroy(arrow.gameObject);
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        level = Level.One;
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
        if (GameManager.instance.IsPause || !GameManager.instance.IsArrowAble) 
        {
            return;
        }

        timer += Time.deltaTime;

        if (//GameManager.instance.IsArrowAble &&
            Input.GetMouseButtonDown(0) &&
            timer >= fireDelay &&
            !EventSystem.current.IsPointerOverGameObject()) 
        {
            timer = 0f;
            animator.SetBool("IsFiring", true);
            switch (level)
            {
                case Level.One:
                    Fire();
                    break;
                case Level.Two:
                    Fire2();
                    break;
            }
            //Fire();
            
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
        var arrow = ArrowPool.Get();
        arrow.transform.position = transform.position;
        arrow.Damage = damage;
        arrow.GetComponent<Rigidbody2D>().velocity = transform.up * force;

        //var arrow2 = ArrowPool.Get();
        //arrow2.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y);
        //arrow2.Damage = damage;
        //arrow2.GetComponent<Rigidbody2D>().velocity = transform.up * force;
    }

    private void Fire2()
    {
        
        for (int i = 0; i < arrowCount; i++)
        {
            var arrow = ArrowPool.Get();
            arrow.Damage = damage;
            arrow.transform.position = new Vector3(transform.position.x + (i * gap), transform.position.y);
            arrow.GetComponent<Rigidbody2D>().velocity = transform.up * force;
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

    public void SetLevel2()
    {
        if (level == Level.Two)
        {
            return;
        }
        level = Level.Two;
    }
}
