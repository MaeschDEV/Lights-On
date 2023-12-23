using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //Public
    [Header("References")]
    public Sound[] musicSounds;
    public Sound[] sfxSounds;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private buttonManager buttonManager;

    //Public & Static
    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.volume = PlayerPrefs.GetFloat("VolumeMusic", 1);
        sfxSource.volume = PlayerPrefs.GetFloat("VolumeSFX", 1);
        if(PlayerPrefs.GetInt("MuteMusic", 1) == 0)
        {
            //Mute
            musicSource.mute = true;
            buttonManager.setMusicButton(true);
        }
        else
        {
            //Not Mute
            musicSource.mute = false;
            buttonManager.setMusicButton(false);
        }
        if (PlayerPrefs.GetInt("MuteSFX", 1) == 0)
        {
            //Mute
            sfxSource.mute = true;
            buttonManager.setSFXButton(true);
        }
        else
        {
            //Not Mute
            sfxSource.mute = false;
            buttonManager.setSFXButton(false);
        }
        musicSlider.value = musicSource.volume;
        sfxSlider.value = sfxSource.volume;
        PlayMusic("Music");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.ClipName == name);

        if(s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.ClipName == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        if(musicSource.mute)
        {
            PlayerPrefs.SetInt("MuteMusic", 0);
        }
        else
        {
            PlayerPrefs.SetInt("MuteMusic", 1);
        }
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        if (sfxSource.mute)
        {
            PlayerPrefs.SetInt("MuteSFX", 0);
        }
        else
        {
            PlayerPrefs.SetInt("MuteSFX", 1);
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("VolumeMusic", volume);
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("VolumeSFX", volume);
    }
}
