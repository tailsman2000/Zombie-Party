using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject SettingsMenu; 
    [SerializeField] private GameObject Backstory; 

    [Header("Buttons")]
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private Button continueButton;


    private void Start()
    {

        playButton.onClick.AddListener(() =>
        {
            Backstory.SetActive(true);
            this.gameObject.SetActive(false);
        });

        continueButton.onClick.AddListener(() =>
        {
            Debug.Log("yoo");
            // Start game right now this just enables the input

            GameManager.Instance.StartGame();
            
            SceneLoader.Load(SceneLoader.Scene.SidScene);
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
