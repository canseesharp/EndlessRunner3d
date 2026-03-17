using System;
using UnityEngine.InputSystem;

public class PlayerInput
{
    private readonly PlayerInputActions _inputActions = new();

    public event Action Jumped;
    public event Action Slided;
    public event Action MovedLeft;
    public event Action MovedRight;

    public void Enable()
    {
        _inputActions.Player.Up.performed += OnJump;
        _inputActions.Player.Down.performed += OnSlide;
        _inputActions.Player.Left.performed += OnMoveLeft;
        _inputActions.Player.Right.performed += OnMoveRight;
        _inputActions.Enable();
    }
    
    public void Disable()
    {
        _inputActions.Player.Up.performed -= OnJump;
        _inputActions.Player.Down.performed -= OnSlide;
        _inputActions.Player.Left.performed -= OnMoveLeft;
        _inputActions.Player.Right.performed -= OnMoveRight;
        _inputActions.Disable();
    }

    private void OnJump(InputAction.CallbackContext callbackContext) => Jumped?.Invoke();

    private void OnSlide(InputAction.CallbackContext callbackContext) => Slided?.Invoke();

    private void OnMoveLeft(InputAction.CallbackContext callbackContext) => MovedLeft?.Invoke();

    private void OnMoveRight(InputAction.CallbackContext callbackContext) => MovedRight?.Invoke();
}
