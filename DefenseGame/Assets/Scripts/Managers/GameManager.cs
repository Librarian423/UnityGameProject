using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool isArrowAble = true;
    public bool IsArrowAble { get { return isArrowAble; } set { isArrowAble = value; } }
    protected bool isBossAlive = true;
    public bool IsBossAlive { get { return isBossAlive; } set { isBossAlive = value; } }

    public static int wave;

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Resume();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            UIManager.instance.UdateSurvivedWave(wave);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPause = true;
        UIManager.instance.SetMagicButtons(false);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPause = false;
        UIManager.instance.SetMagicButtons(true);
    }

    public void GameOver()
    {
        SoundManager.instance.ChangeBgmOnSceneLoad(2);
        SceneManager.LoadScene(2);
    }
}
