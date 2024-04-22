using UnityEngine;

public class PlayerInputRouter : IInputRouter
{
    private GameInput _gameInput;

    public PlayerInputRouter()
    {
        _gameInput = new();
    }

    public void OnEnable()
    {
        _gameInput.Enable();
    }

    public void OnDisable()
    {
        _gameInput.Disable();
    }

    public Vector3 GetNewDirection()
    {
        Vector2 inputDirection = _gameInput.Player.Move.ReadValue<Vector2>();

        return new Vector3(inputDirection.x, 0, inputDirection.y);
    }
}