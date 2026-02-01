using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject SettingsMenu; 

    [Header("Buttons")]
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button quitButton;


    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            //Start game right now this just enables the input
            GameManager.Instance.StartGame();
            
            //Hide the main menu 
            this.gameObject.SetActive(false);

        });

        settingsButton.onClick.AddListener(() =>
        {   
            SettingsMenu.SetActive(true);

            this.gameObject.SetActive(false);
        });

        quitButton.onClick.AddListener(() =>
        {
            //Quit game
            Application.Quit();
        });

    }
}
