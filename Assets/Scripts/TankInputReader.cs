using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "TankInputReader", menuName = "Scriptable Objects/TankInputReader")]
public class TankInputReader : ScriptableObject, TankInput.IMovementActions
{
    public event UnityAction<Vector2> MoveEvent;

    private TankInput _movementActions;

    private void OnEnable()
    {
        if (_movementActions == null)
        {
            _movementActions = new TankInput();
            _movementActions.Movement.SetCallbacks(this);
        }

        _movementActions.Movement.Enable();
    }

    private void OnDisable()
    {
        _movementActions.Movement.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }
}
