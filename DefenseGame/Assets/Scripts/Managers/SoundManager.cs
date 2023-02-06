using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Bgm,
        Effect,

    }

    public AudioClip[] BGM;
    public AudioSource bgmSound;
    public AudioSource effectSound;


    [Range(0, 1)] public float totalVolume = 1f;
    [Range(0, 1)] public float bgmVolume = 1f;
    [Range(0, 1)] public float effectVolume = 1f;

    private void Awake()
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeBgmOnSceneLoad(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void ChangeBgmOnSceneLoad(int sceneNum)
    {
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

    public void StopAll()
    {
        if (bgmSound.isPlaying)
        {
            bgmSound.Stop();
        }
    }
}