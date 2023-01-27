using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 접근용 프로퍼티
    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;
        }
    }

    private bool isPause = false;
    public bool IsPause { get { return isPause; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPause = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPause = false;
    }

}
