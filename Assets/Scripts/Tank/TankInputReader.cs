using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "TankInputReader", menuName = "Scriptable Objects/TankInputReader")]
public class TankInputReader : ScriptableObject, TankInput.IMovementActions
{
    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction<Vector2> TurretMoveEvent;

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

    public void OnTurretMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
        {
            TurretMoveEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }
}
