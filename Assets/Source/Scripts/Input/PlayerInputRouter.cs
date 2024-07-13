using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SlimeScience.Input
{
    public class PlayerInputRouter : IInputRouter
    {
        private const float Step = 3f;

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

            return new Vector3(inputDirection.x, 0, inputDirection.y) * Step;
        }

    }
}