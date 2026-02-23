using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [Header("Configurations")]
    public GameObject wavePrefab;
    public Transform spawnPoint;

    [Header("Randomizer")] 
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;

    [Header("Height")] 
    public float minY = -2f;
    public float maxY = 2f;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
            
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPos = new Vector3(spawnPoint.position.x, spawnPoint.position.y + randomY, 0);
            
            Instantiate(wavePrefab, spawnPos, Quaternion.identity);
        }
    }
}
