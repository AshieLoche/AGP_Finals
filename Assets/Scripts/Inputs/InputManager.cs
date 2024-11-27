using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, GameInputs.IGameplayActions
{

    #region Attribute Declaration

    #region Script Attributes
    private GameInputs _gameInputs;
    #endregion

    #region Event Attrbiutes
    public static UnityEvent OnAimEvent = new();
    public static UnityEvent OnFireEvent = new();
    public static UnityEvent OnAimCancelEvent = new();
    public static UnityEvent<Vector2> OnMoveEvent = new();
    public static UnityEvent<Vector2> OnRotateEvent = new();
    #endregion

    #endregion

    #region Method Definition

    #region Native Methods
    private void OnEnable()
    {
        if (_gameInputs == null)
        {
            _gameInputs = new GameInputs();
            _gameInputs.Gameplay.SetCallbacks(this);
            _gameInputs.Gameplay.Enable();
        }
    }
    #endregion

    #region Interface Methods
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            OnAimEvent.Invoke();
        else if (context.phase == InputActionPhase.Canceled)
            OnAimCancelEvent.Invoke();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
            OnFireEvent.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        OnRotateEvent.Invoke(context.ReadValue<Vector2>());
    }
    #endregion

    #endregion

}