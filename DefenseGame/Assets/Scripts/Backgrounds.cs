using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgrounds : MonoBehaviour
{
    public GameObject[] cloud1;
    public GameObject[] cloud2;

    public float speed1 = 1f;
    public float speed2 = 2f;

    public float width = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPause)
        {
            return;
        }
        if (cloud1 == null || cloud2 == null) 
        {
            return;
        }
        MoveClouds();
        RollBack();
    }

    private void MoveClouds()
    {
        foreach (var c1 in cloud1)
        {
            c1.transform.Translate(Vector2.right * speed1 * Time.deltaTime);
        }

        foreach (var c1 in cloud2)
        {
            c1.transform.Translate(Vector2.right * speed2 * Time.deltaTime);
        }
    }

    private void RollBack()
    {
        foreach (var c1 in cloud1)
        {
            if (c1.transform.position.x >= width) 
            {
                c1.transform.position -= new Vector3(width * cloud1.Length, 0f, 0f);
            }
           
        }

        foreach (var c2 in cloud2)
        {
            if (c2.transform.position.x >= width)
            {
                c2.transform.position -= new Vector3(width * cloud2.Length, 0f, 0f);
            }

        }
    }
}
