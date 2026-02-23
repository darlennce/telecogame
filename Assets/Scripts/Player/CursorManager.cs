using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; 

public class CursorManager : MonoBehaviour
{
    [Header("Objetos de Cursor")]
    public GameObject cursorGameplay;
    public GameObject cursorMenu;
    
    public bool cursorEnabled;

    void Awake()
    {
        Cursor.visible = cursorEnabled;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        
        transform.position = mousePos;
        
        bool mostrarMenu = !GameManager.instance.gameStarted || GameManager.instance.panelGameOver.activeSelf;

        if (mostrarMenu)
        {
            cursorGameplay.SetActive(false);
            cursorMenu.SetActive(true);
        }
        else
        {
            cursorGameplay.SetActive(true);
            cursorMenu.SetActive(false);
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus) Cursor.visible = cursorEnabled;
    }
}