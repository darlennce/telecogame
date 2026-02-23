using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Spear Configs")]
    public GameObject spearPrefab;
    
    public float spawnHeight = 10f;
    public float groundY = -3.5f;
    
    [Header("Click Time")]
    public float clickTime = 0.5f;
    private bool  canClick = true;
    
    void OnClick(InputValue value)
    {
        if (value.isPressed && GameManager.instance.gameStarted && canClick)
        {
            StartCoroutine(CanClick());
            
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayThrowSound();
            }
            
            Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPosition); 
            
            Vector3 spawnPos = new Vector3(mousePos.x, spawnHeight, 0);
            
            GameObject newSpear = Instantiate(spearPrefab, spawnPos, Quaternion.Euler(0, 0, 0));
            
            newSpear.GetComponent<SpearScript>().StartFalling(groundY);
        }
    }

    IEnumerator CanClick()
    {
        canClick = false;
        yield return new WaitForSeconds(clickTime);
        canClick = true;
    }
}


















