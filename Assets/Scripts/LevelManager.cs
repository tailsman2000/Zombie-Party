using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public static Action LevelStart;
    public static Action OnDeath;
    private float radiation;
    //[Header("Radiation")]
    [SerializeField] private float radiationRate;
    [SerializeField] private Slider radiationSlider;

    //[Header("Game Over")]
    public enum GameOverReason { RADIATION, ZOMBIE };
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private TextMeshProUGUI gameOverDescription;
    [SerializeField] private CinemachineCamera playerCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        LevelStart?.Invoke();
        radiation = 0;
        radiationSlider.value = 0f;
        StartCoroutine(RadiationRoutine());
    }

    private IEnumerator RadiationRoutine()
    {
        while(radiation < 100)
        {
            radiation += radiationRate * Time.deltaTime;
            radiationSlider.value = radiation / 100f;
            yield return null;
        }
        StartCoroutine(GameOver(GameOverReason.RADIATION));
    }
    private void DecreaseRadiation(float amount)
    {
        radiation = Mathf.Clamp(radiation - amount, 0f, 100f);
        radiationSlider.value = radiation / 100f;
    }
    private void SetRadiationRate(float rate)
    {
        radiationRate = rate;
    }

    public IEnumerator GameOver(GameOverReason reason)
    {
        OnDeath?.Invoke();

        StartCoroutine(Zoom());

        gameOverCanvasGroup.gameObject.SetActive(true);
        gameOverCanvasGroup.alpha = 0;
        
        if(reason == GameOverReason.RADIATION)
        {
            gameOverDescription.text = "The radiation got to you and you decided to join the gang...";
        }
        else if(reason == GameOverReason.ZOMBIE)
        {
            gameOverDescription.text = "A zombie noticed you weren't one of their groupies...";
        }
        float duration = 2;
        while(gameOverCanvasGroup.alpha < 1)
        {
            gameOverCanvasGroup.alpha += Time.deltaTime / duration;
            yield return null;
        }
    }

    private IEnumerator Zoom()
    {
        float start = playerCamera.Lens.OrthographicSize;
        float target = 3f;
        float duration = 2f;
        float t = 0f;
        while (t < duration) {
            t += Time.deltaTime;
            playerCamera.Lens.OrthographicSize = Mathf.Lerp(start, target, t / duration);
            yield return null;
        }
        
        playerCamera.Lens.OrthographicSize = 3f;
    }

    public void ReturnToMenu()
    {
       // SceneManager.LoadScene("MainMenu");
        SceneLoader.Load(SceneLoader.Scene.StartScreen);
    }

    public void RestartLevel()
    {
        SceneLoader.Load(SceneLoader.Scene.Gameplay);

       // SceneManager.LoadScene("Gameplay");
    }
}
