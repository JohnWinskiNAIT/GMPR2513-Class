using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader2", menuName = "Input/InputReader2")]
public class InputReader2 : ScriptableObject, PlayerControls.IPlayerActions
{
    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction JumpEvent;
    public event UnityAction JumpCanceledEvent;

    private PlayerControls _playerActions;

    private void OnEnable()
    {
        if (_playerActions == null)
        {
            _playerActions = new PlayerControls();
            _playerActions.Player.SetCallbacks(this);
        }

        _playerActions.Player.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnJump(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            JumpEvent?.Invoke();
        }
        else if (context.canceled)
        {
            JumpCanceledEvent?.Invoke();
        }
    }

    public void MovePlayerForward()
    {
        MoveEvent?.Invoke(new Vector2(0, 1));
    }

    public void MovePlayerBackward()
    {
        MoveEvent?.Invoke(new Vector2(0, -1));
    }
}
