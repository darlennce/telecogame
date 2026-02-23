using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private static bool isMenuOpened = false;
    
    [Header("Menu Configs")]
    public GameObject canvasMenu;
    
    [Header("Score")] 
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreRoundText;

    [Header("Life System")] 
    public int lives = 3;
    public Image wifiDisplay;
    public Sprite[] wifiSprites;

    [Header("Camera Shaking & Flash")] 
    public float duration;
    public float magnitude;
    private ScreenFlash screenFlash;

    [Header("Game State")]
    public bool gameStarted = false;
    public bool countingActivated;
    
    [Header("UI References")]
    public TextMeshProUGUI countingText;
    public GameObject hudGameplay;
    
    [Header("UI Game Over")]
    public GameObject panelGameOver;
    public CanvasGroup panelGameOverGroup;

    public float fadeGameOverDuration;

    void Awake()
    {
        if (instance == null) instance = this;
        
        if (panelGameOverGroup != null)
        {
            panelGameOver.SetActive(false);
            panelGameOverGroup.alpha = 0;
            panelGameOverGroup.interactable = false;
            panelGameOverGroup.blocksRaycasts = false;
        }
        
        screenFlash = GetComponent<ScreenFlash>();
    }

    void Start()
    {
        Time.timeScale = 1;
        
        if (!isMenuOpened)
        {
            canvasMenu.SetActive(true);
            hudGameplay.SetActive(false);
            countingText.gameObject.SetActive(false);
        }
        else
        {
            canvasMenu.SetActive(false);
            hudGameplay.SetActive(true);
            countingText.gameObject.SetActive(true);
            countingText.text = "CLIQUE PARA INICIAR";
        }
    }

    public void StartGame()
    {
        // Check if the game has started
        if (!gameStarted && !countingActivated && !canvasMenu.activeSelf)
        {
            StartCoroutine(CountingSequence()); 
        }
    }

    public void PlayButton()
    {
        isMenuOpened = true;
        canvasMenu.SetActive(false);
        hudGameplay.SetActive(true);
        countingText.gameObject.SetActive(true);
        countingText.text = "CLIQUE PARA INICIAR";
    }

    IEnumerator CountingSequence()
    {
        countingActivated = true;
        int time = 3;
        
        while (time > 0)
        {
            countingText.text = time.ToString();
            
            if(panelGameOverGroup.alpha == 0) 
            {
                AudioManager.instance.PlayCountingSFX();
            }
            
            yield return new WaitForSeconds(1f);
            time--;
        }

        AudioManager.instance.PlayGoSFX();
        
        if (AudioManager.instance != null) AudioManager.instance.PlayMusic();
        
        countingText.text = "GO!";
        yield return new WaitForSeconds(0.3f);
        
        countingText.gameObject.SetActive(false);
        countingActivated = false;
        gameStarted = true;
    }
    
    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void LoseHealth()
    {
        if (lives <= 0) return;
        lives--;
        UpdateHealthWifi();

        if (CameraShake.instance != null)
            CameraShake.instance.Shake(duration, magnitude);

        if (lives <= 0) GameOver();
    }

    void UpdateHealthWifi()
    {
        if (wifiDisplay != null && wifiSprites.Length > lives)
            wifiDisplay.sprite = wifiSprites[lives];
    }

    void GameOver()
    {
        gameStarted = false;
        
        
        if(hudGameplay != null) hudGameplay.SetActive(false);
        
        if (screenFlash != null) ScreenFlash.instance.FlashCancel();
        
        if(AudioManager.instance != null)
        {
            AudioManager.instance.StopMusic();
            AudioManager.instance.PlayGameOver();
        }
        
        scoreRoundText.text = "SCORE: " + score.ToString(); 

        StartCoroutine(AnimarGameOver());
    }

    IEnumerator AnimarGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        panelGameOver.SetActive(true);

        float duracao = fadeGameOverDuration;
        float t = 0;

        while (t < duracao)
        {
            t += Time.unscaledDeltaTime; 
            float progresso = t / duracao;
            
            panelGameOverGroup.alpha = progresso + Random.Range(-0.05f, 0.05f);
            yield return null;
        }

        panelGameOverGroup.alpha = 1;
        panelGameOverGroup.interactable = true; 
        panelGameOverGroup.blocksRaycasts = true; 
        
        Time.timeScale = 0; 
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        isMenuOpened = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}



