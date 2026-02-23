using UnityEngine;
using System.Collections;

public class AmbienceManager : MonoBehaviour
{
    [Header("Visual Configurations")]
    public GameObject[] lightingBackgroundPrefabs;
    public float minX, maxX;
    public float heightY;
    public float fadeDuration;

    [Header("Time Configurations")] 
    public float minTime;
    public float maxTime;
    
    [Header("Background Flash Controller")]
    public float maxAlpha;
    public float duration;

    void Start()
    {
        StartCoroutine(LightingCicle());
    }

    IEnumerator LightingCicle()
    {
        while (true)
        {
            float waitingTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitingTime);
            
            Vector3 position = new Vector3(Random.Range(minX, maxX), heightY, 0);

            if (lightingBackgroundPrefabs.Length > 0)
            {
                int index = Random.Range(0, lightingBackgroundPrefabs.Length);
                Instantiate(lightingBackgroundPrefabs[index], position, Quaternion.identity);
            }

            if (ScreenFlash.instance != null)
            {
                ScreenFlash.instance.TriggerFlash(maxAlpha, duration);
            }
        }
    }
    
}
