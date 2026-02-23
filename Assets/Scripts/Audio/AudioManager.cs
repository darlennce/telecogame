using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //... Resto do codigo 

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip gameOverSound;
    public AudioClip lightingHitGround;
    public AudioClip lightingSuck;     
    public AudioClip menuSelection;    
    public AudioClip throwSound;
    public AudioClip countingSFX;
    public AudioClip goSFX;
    
    public bool isMusicMuted = false;
    public bool isSFXMuted = false;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (backgroundMusic != null && !musicSource.isPlaying)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        isMusicMuted = !isMusicMuted;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        isSFXMuted = !isSFXMuted;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    
    public void PlayLightingHit() => PlaySFX(lightingHitGround);
    public void PlayLightingSuck() => PlaySFX(lightingSuck);
    public void PlayGameOver() => PlaySFX(gameOverSound);
    public void PlayThrowSound() => PlaySFX(throwSound);
    public void PlayButtonSound() => PlaySFX(menuSelection);
    public void PlayCountingSFX() => PlaySFX(countingSFX);
    public void PlayGoSFX() => PlaySFX(goSFX);
}