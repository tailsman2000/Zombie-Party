using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu; 

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private Scrollbar volumeScrollbar; 


    void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            mainMenu.SetActive(true);

            this.gameObject.SetActive(false);
        });

        volumeScrollbar.onValueChanged.AddListener((float value) =>
        {
            //call function on game manager to adjust value
            Debug.Log("Current volume: " + value);
        });
    }
}
