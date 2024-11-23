using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, GameInputs.IGameplayActions
{

    private GameInputs _gameInputs;
    

    public static UnityEvent<Vector2> OnMoveEvent = new();

    private void OnEnable()
    {
        if (_gameInputs == null)
        {
            _gameInputs = new GameInputs();
            _gameInputs.Gameplay.SetCallbacks(this);
            _gameInputs.Gameplay.Enable();
        }
    }

    public void OnCharge(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent.Invoke(context.ReadValue<Vector2>());
        Debug.Log("YIPPIE");
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

}