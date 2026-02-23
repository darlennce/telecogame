using UnityEngine;

public class SplashDestroyer : MonoBehaviour
{
    public float timer = 0.3f;
    void Start()
    {
        Destroy(gameObject, timer);
    }
    
}
