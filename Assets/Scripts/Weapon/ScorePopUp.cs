using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    public float speed = 2f;
    public float fadeTime = 1f;
    
    private TextMeshPro textMesh;
    private Color textColor;

    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
        textColor = textMesh.color;
        
        Destroy(gameObject, fadeTime);
    }

    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        
        textColor.a -= (1f / fadeTime) * Time.deltaTime;
        textMesh.color = textColor;
    }
}