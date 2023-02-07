using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Collecter : MonoBehaviour
{
    public GameObject character;
    public CollecterData data;

    private SpriteRenderer ship;
    private SpriteRenderer collecter;
    private Animator animator;

    private float timer = 0f;
    public float distance = 1f;
  
    private Resource target;
    private List<Resource> targets;

    private State state;

    public enum State
    {
        Searching,
        Moving,
        Collecting,
    }

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<SpriteRenderer>();
        if (character != null)
        {
            collecter = character.GetComponent<SpriteRenderer>();
            animator = character.GetComponent<Animator>();
        }
        targets = PropertySpawner.instance.GetResources();
        state = State.Searching;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (target == null)
        {
            state = State.Searching;
        }

        switch (state)
        {
            case State.Searching:
                if (PropertySpawner.instance.GetResourcesCount() > 0)
                {
                    FindNextTarget();
                }
                else
                {
                    BackToBase();
                }
                break;

            case State.Moving:
                if (transform.position.x >= target.transform.position.x)
                {
                    MoveLeft(target.transform.position);
                }
                else
                {
                    MoveRight(target.transform.position);
                }
                break;

            case State.Collecting: 
                if (timer <= 0)
                {    
                    timer = data.collectSpeed;
                    Collecting();
                    PropertyManager.instance.SetMeat(data.maxMeat);
                }
                break;
        }
        
    }

    private void FindNextTarget()
    {
        
        float temp = Vector2.Distance(transform.position, targets[0].transform.position);
        foreach (var target in targets)
        {  
            var dis = Vector2.Distance(transform.position, target.transform.position);
            this.target = target;
            if (dis <= temp)
            {
                this.target = target;
                temp = dis;
            }
        }
        state = State.Moving;
    }

    private void BackToBase()
    {
        animator.SetBool("Attack", false);
        if (transform.position.x > -4) 
        {
            ship.flipX = true;
            collecter.flipX = true;
            transform.Translate(Vector2.left * data.speed * Time.deltaTime);

        }
        else if (transform.position.x <= -4)
        {
            ship.flipX = false;
            collecter.flipX = false;

        } 
    }

    private void MoveLeft(Vector2 pos)
    {
        ship.flipX = true;
        collecter.flipX = true;
        animator.SetBool("Attack", false);
        if (Vector2.Distance(transform.position, pos) >= distance)
        {
            //isMoving = true;
            transform.Translate(Vector2.left * data.speed * Time.deltaTime);

        }
        else
        {
            state = State.Collecting;
        }
    }

    private void MoveRight(Vector2 pos)
    {
        ship.flipX = false;
        collecter.flipX = false;
        animator.SetBool("Attack", false);
        if (Vector2.Distance(transform.position, pos) >= distance)
        {
            transform.Translate(Vector2.right * data.speed * Time.deltaTime);

        }
        else
        {
            state = State.Collecting;
        }
    }

    private void Collecting()
    {
        target.CollectResource(data.maxGold, data.maxMeat);
        animator.SetBool("Attack", true);
    }
}
