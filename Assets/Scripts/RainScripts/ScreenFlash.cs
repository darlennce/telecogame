using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash instance;
    public Image flashImage;
  
    void Awake()
    {
        instance = this;
    }

    public void TriggerFlash(float maxAlpha, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(DoFlash(maxAlpha, duration));
    } 

    IEnumerator DoFlash(float maxAlpha, float duration)
    {
        float elapsed = 0f;
        Color col = flashImage.color;
        
        while (elapsed < duration * 0.2f)
        {
            elapsed += Time.deltaTime;
            col.a = Mathf.Lerp(0f, maxAlpha, elapsed / (duration * 0.2f));
            flashImage.color = col;
            yield return null;
        }
        
        elapsed = 0f;
        
        while (elapsed < duration * 0.8f)
        {
            elapsed += Time.deltaTime;
            col.a = Mathf.Lerp(maxAlpha, 0f, elapsed / (duration * 0.8f));
            flashImage.color = col;
            yield return null;
        }

        col.a = 0f;
        flashImage.color = col;
    }

    public void FlashCancel()
    {
        StopAllCoroutines(); 
        
        Image flashImage = GetComponent<Image>();

        if (flashImage != null)
        {
            Color corTemporaria = flashImage.color;
            
            corTemporaria.a = 0f;
            
            flashImage.color = corTemporaria;
        }
    }
}










