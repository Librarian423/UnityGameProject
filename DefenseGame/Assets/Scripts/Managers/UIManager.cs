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


    [Header("Buttons")]
    public GameObject pauseBtn;

    //Popups
    [Header("PopUps")]
    public GameObject skillTree;
    public GameObject pausePopUp;

    //Magic btns
    [Header("Magic Buttons")]
    public GameObject fbBtn;
    public GameObject exBtn;
    public GameObject lBtn;
    public GameObject elecBtn;

    [Header("UI SE")]
    public AudioClip skillTreeSE;

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

    public void OpenSkillTree()
    {
        SoundManager.instance.PlayEffect(skillTreeSE);
        GameManager.instance.Pause();
        skillTree.SetActive(true);
    }

    private void OpenPausePopUp()
    {
        GameManager.instance.Pause();
        pausePopUp.SetActive(true);
    }

    private void ClosePausePopUp()
    {
        GameManager.instance.Resume();
        pausePopUp.SetActive(false);
    }

    public void PauseGame()
    {
        if (!skillTree.activeSelf)
        {
            SetInterectableFalse(pauseBtn.GetComponent<Button>());
            GameManager.instance.Pause();
            OpenPausePopUp();
        }
    }

    public void ResumeGame()
    {
        if (!skillTree.activeSelf)
        {
            SetInterectableTrue(pauseBtn.GetComponent<Button>());
            GameManager.instance.Resume();
            ClosePausePopUp();
        }
    }

    public void SearchMagicButton(List<Magics> magics)
    {
        if (magics.Count > 0) 
        {
            foreach (var item in magics)
            {
                EnableMagicBtn(item);
            }
        }
    }
    
    public void EnableMagicBtn(Magics type)
    {
        switch (type)
        {
            case Magics.FireBlast:
                if (!fbBtn.activeSelf)
                {
                    fbBtn.SetActive(true);
                }
                break;
            case Magics.Laser:
                if (!lBtn.activeSelf)
                {
                    lBtn.SetActive(true);
                }
                break;
            case Magics.Explosion:
                if (!exBtn.activeSelf)
                {
                    exBtn.SetActive(true);
                }
                break;
            case Magics.Electric:
                if (!elecBtn.activeSelf)
                {
                    elecBtn.SetActive(true);
                }
                break;
        }
    }

    public void SetInterectableFalse(Button button)
    {
        if (button == null)
        {
            return;
        }
        button.interactable = false;
    }

    public void SetInterectableTrue(Button button)
    {
        if (button == null)
        {
            return;
        }
        button.interactable = true;
    }

    public void ClickBtnSE(AudioClip clip)
    {
        SoundManager.instance.PlayEffect(clip);
    }

    public void SetMagicButtons(bool isInterect)
    {
        if (isInterect)
        {
            SetInterectableTrue(fbBtn.GetComponent<Button>());
            SetInterectableTrue(exBtn.GetComponent<Button>());
            SetInterectableTrue(lBtn.GetComponent<Button>());
            SetInterectableTrue(elecBtn.GetComponent<Button>());
        }
        else
        {
            SetInterectableFalse(fbBtn.GetComponent<Button>());
            SetInterectableFalse(exBtn.GetComponent<Button>());
            SetInterectableFalse(lBtn.GetComponent<Button>());
            SetInterectableFalse(elecBtn.GetComponent<Button>());
        }
    }
}
