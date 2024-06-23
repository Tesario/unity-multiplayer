using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnPauseAction;

    public PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Pause.performed += Pause_performed; ;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return playerInputActions.Player.Movement.ReadValue<Vector2>().normalized;
    }
}
