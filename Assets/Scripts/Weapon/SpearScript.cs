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
    
    [Header("Dissolve VFX")]
    public GameObject dissolveVFX;
    public SpriteRenderer mainSpriteRenderer;
    public float dissolveSpeed;
    public float dissolveHeight;
    
    private float groundY;
    private bool stuck = false;

    private Animator anim;
    
    private BoxCollider2D boxCollider;
    
    private Coroutine flashCoroutine;
    private Coroutine vanishCoroutine;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        mainSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    public void StartFalling(float destinyY)
    {
        groundY = destinyY;
    }

    public void SuckEnergy()
    {
        if(flashCoroutine != null) StopCoroutine(flashCoroutine);
        flashCoroutine = StartCoroutine(FlashEffect());
        StartCoroutine(VanishRoutine());
    }

    IEnumerator FlashEffect()
    {
        // Bright Effect on Spear
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

            if (impactVFX != null)
            {
                Instantiate(impactVFX, new Vector3(transform.position.x, vfxHeight, transform.position.z), Quaternion.identity);
            }
            
            StuckOnGround();
        }
    }

    void StuckOnGround()
    {
        Debug.Log("Stucked!");
        Debug.Log("Main Sprite Alpha: " +  mainSpriteRenderer.color.a);
        StartCoroutine(VanishRoutine());
    }

    IEnumerator VanishRoutine()
    {
        yield return new WaitForSeconds(disapearTime);
        
        //Spear alpha
        Color cM = mainSpriteRenderer.color;
        cM.a = 1f;
        mainSpriteRenderer.color = cM;
        
        while (cM.a > 0)
        {
            cM.a -= Time.deltaTime * dissolveSpeed;
            mainSpriteRenderer.color = cM;
            yield return null;
        }
        
        cM.a = 0;
        mainSpriteRenderer.color = cM;
        //

        // Dissolve Effect
        if (dissolveVFX != null)
        {
            Instantiate(dissolveVFX, new Vector2(transform.position.x, transform.position.y + dissolveHeight), Quaternion.identity);
        }
        
        yield return new WaitForSeconds(0.1f);
        
        Destroy(gameObject);
    }
    
}



