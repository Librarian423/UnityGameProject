using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //private Vector2 mousePoint;
    //private LineRenderer line;
    //private Vector2 direction;

    //public GameObject dotPrefab;
    //public GameObject[] dots;
    //public int dotCount = 10;

    //public float force = 20f;

    private Rigidbody2D playerRigidbody; 
    private Animator animator; 
    private AudioSource playerAudio; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   
}
