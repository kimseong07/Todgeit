using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Slider mainVolumeBar;
    public AudioSource mainAudio;

    public AudioSource dieAudio;

    private static float volumeBar = 1f;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("다수의 칼라매니저가 실행중입니다");
        }
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        mainAudio.volume = mainVolumeBar.value;
        dieAudio.volume = mainVolumeBar.value;
        volumeBar = mainVolumeBar.value;
        volumeBar = dieAudio.volume;
        PlayerPrefs.SetFloat("volumebar", volumeBar);
    }

    private void Init()
    {
        volumeBar = PlayerPrefs.GetFloat("volumebar", 1f);
        mainVolumeBar.value = volumeBar;
        mainAudio.volume = mainVolumeBar.value;
        dieAudio.volume = mainVolumeBar.value;
    }

    public void PlayerDie()
    {
        dieAudio.Play();
        mainAudio.Stop();
    }
}
