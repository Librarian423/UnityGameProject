using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Resource : MonoBehaviour
{
    public ResourceData resource;

    private int meat = 0;
    private int gold = 0;
    private float duration = 5f;

    private float timer = 0f;

    public event Action onComplete;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        timer = duration;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 || (meat <= 0 && gold <= 0)) 
        {
            //Destroy(gameObject);
            onComplete();
        }
    }

    public void GetResources(int meat, int gold)
    {
        this.meat -= meat;
        this.gold -= gold;
    }

    private void Init()
    {
        meat = resource.meat;
        gold = resource.gold;
        duration = resource.duration;
    }
}
