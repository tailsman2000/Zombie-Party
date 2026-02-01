using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject SettingsMenu; 
    [SerializeField] private Animator idleGuy;

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
            this.GetComponent<Animator>().SetTrigger("FadeOut");
            
            idleGuy.SetTrigger("MenuGuyMove");

        });

        continueButton.onClick.AddListener(() =>
        {
            GameManager.Instance.StartGame();
            
            SceneLoader.Load(SceneLoader.Scene.Gameplay);
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

    public void ShowBackstory()
    {

    }
}
