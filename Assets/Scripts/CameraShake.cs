using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private Vector3 originalPos;
    private bool isShaking = false;

    void Awake()
    {
        if(instance == null) instance = this;
        originalPos = transform.position;
    }

    public void Shake(float duration, float magnitude)
    {
        if(isShaking) return;
        StartCoroutine(DoShake(duration, magnitude));
    }

    IEnumerator DoShake(float duration, float magnitude)
    {
        isShaking = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;    
            float y = Random.Range(-1f, 1f) * magnitude; 
            
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.localPosition = originalPos;
        isShaking = false;
        
    }
}
