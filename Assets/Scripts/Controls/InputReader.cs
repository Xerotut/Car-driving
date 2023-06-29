using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CarDriving
{
    public static class InputReader
    {
        static InputReader()
        {
            _playerControls = new PlayerControls();
            SubscribeToInputs();

            _playerControls.Enable();
        }

        private readonly static PlayerControls _playerControls;


        public static event Action<Vector2> OnMove;

        private static void SubscribeToInputs()
        {
            _playerControls.CarControls.Move.performed += ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
            _playerControls.CarControls.Move.canceled += ctx => OnMove?.Invoke(Vector2.zero);
        }
    }
}
