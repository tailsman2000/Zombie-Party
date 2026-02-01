using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] 
    private GameObject pauseMenuContent; 

    [SerializeField]
    private Button unpauseButton;

     [SerializeField]
    private Button quitButton;


    [SerializeField]
    private Scrollbar volumeScrollbar; 

    private void Start()
    {
        unpauseButton.onClick.AddListener(() =>
        {
            Unpause();
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
       
    }

    public void Pause()
    {
        GameManager.Instance.IsGamePaused = true;

        pauseMenuContent.SetActive(true);

        Time.timeScale = 0f;

    }

    public void Unpause()
    {
        Time.timeScale = 1f;

        GameManager.Instance.IsGamePaused = false;

        pauseMenuContent.SetActive(false);
    }
}