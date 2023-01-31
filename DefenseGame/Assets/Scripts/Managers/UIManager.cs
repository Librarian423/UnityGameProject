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

    private void Start()
    {
        if (hpBar != null)
        {
            hpBar.value = 100f;
        }
       
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

    public void UdateSurvivedWave(int wave)
    {
        waveText.text = "Survived " + wave + " waves";
    }

}
