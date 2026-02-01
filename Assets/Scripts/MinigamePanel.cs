using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MinigamePanel : MonoBehaviour
{
    [SerializeField] private Button[] icons; // Assign the 4 icon buttons in the Inspector
    private HashSet<Button> pressedIcons = new HashSet<Button>();

    private void OnEnable()
    {
        // Reset when panel opens
        pressedIcons.Clear();
        foreach (Button icon in icons)
        {
            if (icon != null)
            {
                icon.interactable = true; // Make buttons pressable again
                icon.onClick.AddListener(() => OnIconPressed(icon));
                // Optionally, reset button appearance here
            }
        }
    }

    private void OnDisable()
    {
        // Clean up listeners
        foreach (Button icon in icons)
        {
            if (icon != null)
            {
                icon.onClick.RemoveAllListeners();
            }
        }
    }

    private void OnIconPressed(Button pressedIcon)
    {
        if (!pressedIcons.Contains(pressedIcon))
        {
            pressedIcons.Add(pressedIcon);
            Debug.Log("Icon pressed: " + pressedIcon.name);

            // Optional: Change button appearance (e.g., disable or change color)
            pressedIcon.interactable = false;

            if (pressedIcons.Count >= 4)
            {
                ClosePanel();
            }
        }
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
        Debug.Log("Minigame completed! Panel closed.");
    }
}