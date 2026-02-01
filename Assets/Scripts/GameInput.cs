using System;
using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}

    private PlayerInputActions playerInputActions;

    public bool IsSprinting = false;
    
    public event EventHandler OnInteractPerformed;
    public event EventHandler OnMaskTogglePerformed; 

    public event EventHandler OnGamePaused; 

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);

        playerInputActions = new PlayerInputActions();

        //Only enable input when game should start so when player presses play button
        //playerInputActions.Enable();

        playerInputActions.Player.Sprint.performed += (InputAction.CallbackContext context) =>
        {
            IsSprinting = true;
        };   

        playerInputActions.Player.Sprint.canceled += (InputAction.CallbackContext context) =>
        {
            IsSprinting = false;
        };   

        playerInputActions.Player.Interact.performed += GameInput_OnInteractPerformed;

        playerInputActions.Player.Interact.performed += GameInput_OnMaskTogglePerformed; 

        playerInputActions.Player.Pause.performed += GameInput_Pause;

    }

    private void GameInput_Pause(InputAction.CallbackContext context)
    {
       OnGamePaused?.Invoke(this, EventArgs.Empty);
    }

    void Start()
    {
        GameManager.Instance.OnStartGame += EnableInput;
    }

    private void EnableInput(object sender, EventArgs e)
    {
        playerInputActions.Enable();
    }

    private void GameInput_OnMaskTogglePerformed(InputAction.CallbackContext context)
    {
        OnMaskTogglePerformed?.Invoke(this, EventArgs.Empty);
    }

    private void GameInput_OnInteractPerformed(InputAction.CallbackContext context)
    {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
    }
}