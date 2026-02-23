using UnityEngine;
using System.Collections.Generic;

public class RainImpact : MonoBehaviour
{
    public GameObject splashPrefab;
    public ParticleSystem partSytem;
    
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    
    void Awake()
    {
        partSytem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = partSytem.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            Vector3 position = collisionEvents[i].intersection;
            Instantiate(splashPrefab, position, Quaternion.identity);
        }
    }
    
}
