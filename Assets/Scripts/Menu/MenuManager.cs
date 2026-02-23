using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Sub-Menus")] 
    public GameObject panelSettings;
    public GameObject panelHowToPlay;
    
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;
    
    [Header("Buttons")]
    public Button muteMusicButton;
    public Button muteSFXButton;

    public Sprite[] muteMusicSprites;
    public Sprite[] muteSFXSprites;
    
    public void OpenHowToPlay() => panelHowToPlay.SetActive(true); 
    public void CloseHowToPlay() => panelHowToPlay.SetActive(false);
    
    public void OpenSettings() => panelSettings.SetActive(true);
    public void CloseSettings() => panelSettings.SetActive(false);


    void Start()
    {
        MuteIconsSyncronizer();
        
        if (musicSlider != null && AudioManager.instance != null)
            musicSlider.value = AudioManager.instance.musicSource.volume;
        
        if (sfxSlider != null && AudioManager.instance != null)
            sfxSlider.value = AudioManager.instance.sfxSource.volume;
        
        musicSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
    }
    
    public void MuteIconsSyncronizer()
    {
        if (AudioManager.instance == null) return;
        
        if (AudioManager.instance.isMusicMuted)
            muteMusicButton.GetComponent<Image>().sprite = muteMusicSprites[1];
        else
            muteMusicButton.GetComponent<Image>().sprite = muteMusicSprites[0];
        
        if (AudioManager.instance.isSFXMuted)
            muteSFXButton.GetComponent<Image>().sprite = muteSFXSprites[1];
        else
            muteSFXButton.GetComponent<Image>().sprite = muteSFXSprites[0];
    }

    public void ToggleMusicMute()
    {
        AudioManager.instance.ToggleMusic();
        if (AudioManager.instance.isMusicMuted)
        {
            muteMusicButton.GetComponent<Image>().sprite = muteMusicSprites[1];
        }
        else
        {
            muteMusicButton.GetComponent<Image>().sprite = muteMusicSprites[0];
        }
    }

    public void ToggleSFXMute()
    {
        AudioManager.instance.ToggleSFX();

        if (AudioManager.instance.isSFXMuted)
        {
            muteSFXButton.GetComponent<Image>().sprite = muteSFXSprites[1];
        }
        else
        {
            muteSFXButton.GetComponent<Image>().sprite = muteSFXSprites[0];
        }
    }
    
    //Sliders controls
    public void ChangeMusicVolume()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.SetMusicVolume(musicSlider.value);
    }

    public void ChangeSFXVolume()
    {
        if(AudioManager.instance != null)
            AudioManager.instance.SetSFXVolume(sfxSlider.value);
    }

    public void ExitGame()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}




