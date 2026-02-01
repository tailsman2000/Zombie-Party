using UnityEngine;

public class PanelOpener : MonoBehaviour, I_Interactable
{
    [SerializeField] private GameObject panelToOpen; // Assign the panel GameObject in the Inspector

    public void Interact()
    {
        if (panelToOpen != null)
        {
            panelToOpen.SetActive(true);
            Debug.Log("Panel opened: " + panelToOpen.name);
        }
        else
        {
            Debug.LogWarning("No panel assigned to open!");
        }
    }
}