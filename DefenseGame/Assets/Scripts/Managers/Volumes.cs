using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volumes : MonoBehaviour
{
    public Slider slider;
    public SoundManager.Sound soundType;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.value = SoundManager.instance.GetVolume(soundType);
    }

    public void SetVolumes()
    {
        SoundManager.instance.SetVolumes(slider.value, soundType);
    }
}
