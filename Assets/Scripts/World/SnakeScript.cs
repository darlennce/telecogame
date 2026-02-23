using UnityEngine;
using System.Collections;

public class AmbientRandomizer : MonoBehaviour
{
    private Animator anim;
    
    [Header("Configurar chances")]
    
    [Tooltip("Tempo mínimo entre as tentativas de aparecer")]
    public float minCheckTime = 10f; 
    
    [Tooltip("Tempo máximo entre as tentativas de aparecer")]
    public float maxCheckTime = 20f;
    
    [Tooltip("Chance de 0 a 100 de aparecer em cada tentativa")]
    public float spawnChance = 15f; 

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(SecretOccurrenceRoutine());
    }

    IEnumerator SecretOccurrenceRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minCheckTime, maxCheckTime);
            yield return new WaitForSeconds(waitTime);
            
            if (GameManager.instance.gameStarted)
            {
                float roll = Random.Range(0f, 100f);

                if (roll <= spawnChance)
                {
                    Debug.Log("Spawned!");
                    anim.SetTrigger("Jump");
                    
                    yield return new WaitForSeconds(30f); 
                }
            }
        }
    }
}