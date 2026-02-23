using System;
using UnityEngine;

public class WaveBehavior : MonoBehaviour
{
    public float speed = 2.0f;
    public float lifeTime = 5.0f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
