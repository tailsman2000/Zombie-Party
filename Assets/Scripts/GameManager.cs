using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public event EventHandler OnStartGame;

    [SerializeField] 
    private PauseMenu pauseMenu;

    public bool IsGamePaused = false;

    void Awake()
    {
        Instance = this; 
    }

    void Start()
    {
        GameInput.Instance.OnGamePaused += Game_Paused;
    }

    private void Game_Paused(object sender, EventArgs e)
    {
        if(!IsGamePaused)
        {
            pauseMenu.Pause();

        } else
        {
            pauseMenu.Unpause();
        }


    }

    public void StartGame()
    {
        OnStartGame?.Invoke(this, EventArgs.Empty);

    }
}
