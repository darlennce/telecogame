using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightingSpawner : MonoBehaviour
{
    [Header("Prefabs")] 
    public GameObject warningPrefab;
    public GameObject lightingPrefab;
    
    [Header("Pop Up Configurations")]
    public GameObject popUpPrefab;
    public float spearDistance = 1f;

    [Header("Configuração da Arena")] 
    public GameObject minusX; // GameObjects for locations; 
    public GameObject maximusX;
    private float minX;
    private float maxX;
    public float groundY;
    
    [Header("Difficulty")]
    public float timeBetweenSpawns;
    public float warningDuration;

    public float[] phases;

    private float gameTimer = 0f;

    [Header("Configs")] 
    public float lightingDetectRadius;
    private Vector3 lastDebugPosition;
    
    [Header("Flashing Controller")] 
    public float maxAlpha;
    public float duration;

    [Header("Success Camera Shake")] 
    public float durationShake = 0.3f;
    public float magnitudeShake = 0.2f;

    [Header("Layer Setup")] 
    public LayerMask spearLayer;
    
    void Awake()
    {
        minX = minusX.transform.position.x;
        maxX = maximusX.transform.position.x;
    }

    void Start()
    {
        gameTimer = 0f;
        StartCoroutine(SpawnRoutine());
    }
    
    void Update()
    {
        if (GameManager.instance.gameStarted)
        {
            gameTimer += Time.deltaTime;
            ChangeDifficulty();
        }
    }
    
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (!GameManager.instance.gameStarted)
            {
                yield return null;
                continue;
            }
            
            float randomX = Random.Range(minX, maxX);  
            Vector3 spawnPosition = new Vector3(randomX, groundY, 0);
            
            lastDebugPosition = spawnPosition; // Just for Debugging 
            
            GameObject currentWarning = Instantiate(warningPrefab, spawnPosition, Quaternion.identity);
            
            yield return new WaitForSeconds(warningDuration);
            
            Destroy(currentWarning);
            
            ScreenFlash.instance.TriggerFlash(maxAlpha, duration);
            
            Collider2D hit = Physics2D.OverlapCircle(spawnPosition, lightingDetectRadius, spearLayer);

            // Check if the spear is in the lighting area 
            if (hit != null && hit.CompareTag("Spear"))
            {
                // Spear Max Height
                float stubHeight = hit.bounds.max.y; 
                
                GameManager.instance.AddScore();
                
                //Pop up
                Instantiate(popUpPrefab, spawnPosition + new Vector3(spearDistance, 0, 0), Quaternion.identity); 
                
                if(AudioManager.instance != null) AudioManager.instance.PlayLightingSuck();

                // New spawn position in the spear stub
                spawnPosition = new Vector3(spawnPosition.x, stubHeight, 0);
                
                // Just for disbale the collider
                hit.GetComponent<BoxCollider2D>().enabled = false;
                
                // Score animation
                hit.GetComponent<SpearScript>().SuckEnergy();
                
                // Camera Shake feedback if score
                CameraShake.instance.Shake(0.1f, 0.1f);
                
                Debug.Log("Spear launched!"); 
            }
            
            else
            {
                GameManager.instance.LoseHealth();
                
                if(CameraShake.instance != null) CameraShake.instance.Shake(durationShake, magnitudeShake);
                if(ScreenFlash.instance != null) ScreenFlash.instance.TriggerFlash(maxAlpha, duration);
                
                if(AudioManager.instance != null) AudioManager.instance.PlayLightingHit();
            }
            
            Instantiate(lightingPrefab, spawnPosition, Quaternion.identity);
            
            Debug.Log(timeBetweenSpawns);
            
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void ChangeDifficulty()
    {
        if (phases == null || phases.Length == 0) return; // Check if array is not null 

        if (gameTimer > 10)
        {
            timeBetweenSpawns = phases[0];
        }

        if (gameTimer > 15)
        {
            timeBetweenSpawns = phases[1];
        }
        
        if (gameTimer > 20)
        {
            timeBetweenSpawns = phases[2];
        }

        if (gameTimer > 25)
        {
            timeBetweenSpawns = phases[3];
        }

        if (gameTimer > 35)
        {
            timeBetweenSpawns = phases[4];
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(lastDebugPosition, lightingDetectRadius);
    }
}

