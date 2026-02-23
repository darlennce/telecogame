using UnityEngine;

public class SoundButtonHelper : MonoBehaviour
{
    public void PlayClickSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayButtonSound();
        }
    }
}