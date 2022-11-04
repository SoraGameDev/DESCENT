using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using UnityEngine.UI;
public class AudioSettings : MonoBehaviour
{
    [Header("FMOD Settings")]

    [SerializeField] FMOD.Studio.Bus Music;
    [SerializeField] FMOD.Studio.Bus Master;
    [SerializeField] FMOD.Studio.Bus SFX;
    //Local variables, not useful between scenes
     //float gamemusicVolume = 0.5f; 
     //float gamemasterVolume = 1f;
    // float gamesfxVolume = 0.5f;

    public Slider masterVolume;
    public Slider musicVolume;
    public Slider sfxVolume;



    // Start is called before the first frame update
    private void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        Master = FMODUnity.RuntimeManager.GetBus("bus:/");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");


        if (!PlayerPrefs.HasKey("masterVolume"))
        {
            PlayerPrefs.SetFloat("masterVolume", 1);
        }
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
        }
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Music.setVolume(PlayerPrefs.GetFloat("musicVolume"));
        Master.setVolume(PlayerPrefs.GetFloat("masterVolume"));
        SFX.setVolume(PlayerPrefs.GetFloat("sfxVolume"));
        musicVolume.value = PlayerPrefs.GetFloat("musicVolume");
        masterVolume.value = PlayerPrefs.GetFloat("masterVolume");
        sfxVolume.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void MasterVolumeLevel (float newMasterVolume)
    {
       // masterVolume = newMasterVolume;
        PlayerPrefs.SetFloat("masterVolume", newMasterVolume);
    }
    public void MusicVolumeLevel(float newMusicVolume)
    {
       // musicVolume = newMusicVolume;
        PlayerPrefs.SetFloat("musicVolume", newMusicVolume);
    }
    public void SFXVolumeLevel (float newSFXVolume)
    {
       // sfxVolume = newSFXVolume;
        PlayerPrefs.SetFloat("sfxVolume", newSFXVolume);
    }
}
