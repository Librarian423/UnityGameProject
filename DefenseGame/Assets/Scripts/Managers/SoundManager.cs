using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SoundManager;

public class SoundManager : MonoBehaviour
{
    private static SoundManager m_instance;
    public static SoundManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<SoundManager>();
            }

            return m_instance;
        }
    }

    public enum Sound
    {
        Total,
        Bgm,
        Effect,

    }
    [Header("OptionSliders")]
    public GameObject soundPopup;
    //public Slider totalVol;
    //public Slider bgmVol;
    //public Slider seVol;

    [Header("Clips")]
    public AudioClip[] BGM;
    public AudioSource bgmSound;
    public AudioSource effectSound;


    [Range(0, 1)] public static float totalVolume = 1f;
    [Range(0, 1)] public static float bgmVolume = 0.5f;
    [Range(0, 1)] public static float effectVolume = 0.5f;

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (soundPopup != null)
        {
            bgmSound.volume = bgmVolume * totalVolume;
            effectSound.volume = effectVolume * totalVolume;
        }
       
        ChangeBgmOnSceneLoad(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void ChangeBgmOnSceneLoad(int sceneNum)
    {
        Debug.Log("play");
        StopAll();
        PlayBgm(BGM[sceneNum]);
    }

    public void PlayBgm(AudioClip clip)
    {
        bgmSound.clip = clip;
        bgmSound.loop = true;
        bgmSound.volume = bgmVolume * totalVolume;
        bgmSound.Play();
    }

    public void PlayEffect(AudioClip clip)
    {
        //effectSound.clip = clip;
        effectSound.loop = false;
        effectSound.volume = effectVolume * totalVolume;
        effectSound.PlayOneShot(clip);
    }

    public void SetVolumes(float volume, Sound sound)
    {
        switch (sound)
        {
            case Sound.Total:
                totalVolume = volume;
                break;
            case Sound.Bgm:
                bgmVolume = volume;
                break;
            case Sound.Effect:
                effectVolume = volume;
                break;
        }   

        bgmSound.volume = bgmVolume * totalVolume;
        effectSound.volume = effectVolume * totalVolume;
    }

    public void StopAll()
    {
        if (bgmSound.isPlaying)
        {
            bgmSound.Stop();
        }
    }

    public float GetVolume(Sound sound)
    {
        switch (sound)
        {
            case Sound.Total:
                return totalVolume;
            case Sound.Bgm:
                return bgmVolume;
            case Sound.Effect:
                return effectVolume;
        }
        return 0;
    }
}
