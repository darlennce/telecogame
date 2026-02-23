using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuAtmosphere : MonoBehaviour
{
    public Image flashImage;
    public float minDelay = 2f;
    public float maxDelay = 6f;

    void OnEnable()
    {
        StartCoroutine(RandomFlashLoop());
    }

    IEnumerator RandomFlashLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            
            float duration = 0.2f;
            float time = 0;
            
            while(time < duration)
            {
                float alpha = Mathf.Lerp(0, 0.3f, time / duration);
                flashImage.color = new Color(1, 1, 1, alpha);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            
            // Escurece
            flashImage.color = new Color(1, 1, 1, 0);
        }
    }
}