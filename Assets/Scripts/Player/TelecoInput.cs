using UnityEngine;
using UnityEngine.InputSystem;

public class TelecoActions : MonoBehaviour
{
    private Animator animator;
    private Camera mainCamera;
    
    private PlayerInput playerInput;
    private InputAction clickAction;
    
    [Header("Cursor")]
    public Animator cursorAnimator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        
        playerInput = GetComponent<PlayerInput>();
        clickAction = playerInput.actions["Click"];
    }

    void OnEnable() { clickAction.Enable(); }
    void OnDisable() { clickAction.Disable(); }

    void Update()
    {
        if (clickAction.WasPressedThisFrame())
        {
            ProcessarAtaque();
            cursorAnimator.SetTrigger("Click");
        }
    }

    void ProcessarAtaque()
    {
        if (!GameManager.instance.gameStarted)
        {
            GameManager.instance.StartGame();
            return;
        }
        
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, 0, 0));
        
        if (mouseWorldPos.x < transform.position.x)
        {
            animator.SetTrigger("Cast_Left");
            Debug.Log("Left Attack!");
        }
        else
        {
            animator.SetTrigger("Cast_Right");
            Debug.Log("Right Attack!");
        }
    }
}