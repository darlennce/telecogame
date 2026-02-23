using UnityEngine;
using System.Collections;

public class SpearScript : MonoBehaviour
{
    [Header("Falling Configs")] 
    public float fallingSpeed = 50f;
    public float disapearTime = 2f;
    
    [Header("Impact VFX Configs")]
    public GameObject impactVFX;
    public float vfxHeight;
    
    [Header("Bright VFX Configs")]
    public SpriteRenderer brightOverlay;
    public float fadeVelocity; 
    
    private float groundY;
    private bool stuck = false;

    private Animator anim;
    
    private BoxCollider2D boxCollider;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>(); 
    }
    
    public void StartFalling(float destinyY)
    {
        groundY = destinyY;
    }

    public void SuckEnergy()
    {
        StopAllCoroutines();
        StartCoroutine(FlashEffect());
    }

    IEnumerator FlashEffect()
    {
        if (brightOverlay != null)
        {
            Color c = brightOverlay.color;
            c.a = 1f;
            brightOverlay.color = c;

            while (c.a > 0)
            {
                c.a -= Time.deltaTime * fadeVelocity;
                brightOverlay.color = c;
                yield return null;
            }

            c.a = 0;
            brightOverlay.color = c;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stuck) return;
        
        transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime, Space.World);

        if (transform.position.y <= groundY)
        {
            transform.position = new Vector3(transform.position.x, groundY, transform.position.z);
            stuck = true;
            StuckOnGround();

            if (!impactVFX != null)
            {
                Instantiate(impactVFX, new Vector3(transform.position.x, vfxHeight, transform.position.z), Quaternion.identity);
            }
        }
    }

    void StuckOnGround()
    {
        Debug.Log("Stucked!");
        
        Destroy(gameObject, disapearTime);
    }
}



