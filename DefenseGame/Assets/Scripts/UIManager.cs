using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 싱글톤 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; 

    public TextMeshProUGUI waveText;
    public Slider hpBar;

    //public GameObject gameoverUI; // 게임 오버시 활성화할 UI 

    private void Start()
    {
        hpBar.value = 100f;
    }
    // 적 웨이브 텍스트 갱신
    public void UpdateWaveText(int wave)
    {
        waveText.text = "Wave " + wave;
        
    }

    public void UpdateHealthBar(float health)
    {
        hpBar.value = health;
    }
    //// 게임 오버 UI 활성화
    //public void SetActiveGameoverUI(bool active)
    //{
    //    gameoverUI.SetActive(active);
    //}

}
