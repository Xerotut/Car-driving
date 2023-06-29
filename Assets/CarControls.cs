using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarDriving
{
    public class CarControls : MonoBehaviour
    {
        [SerializeField] private List<AxleInfo> _axleInfos;
        [SerializeField] private float _maxMotorTorque;
        [SerializeField] private float _maxSteeringAngle;


        private Vector2 _moveInput;

        private void Awake()
        {
            InputReader.OnMove += SetMoveInput;
        }

        private void FixedUpdate()
        {
            MoveCar();
        }

        private void SetMoveInput(Vector2 input)
        {
            _moveInput = input;
        }

        private void MoveCar()
        {
            float motor = _maxMotorTorque * _moveInput.y;
            float steering = _maxSteeringAngle * _moveInput.x;
            foreach (AxleInfo axleInfo in _axleInfos)
            {
                if (axleInfo.Steering)
                {
                    axleInfo.LeftWheel.steerAngle = steering;
                    axleInfo.RightWheel.steerAngle = steering;
                }
                if (axleInfo.Motor)
                {
                    axleInfo.LeftWheel.motorTorque = motor;
                    axleInfo.RightWheel.motorTorque = motor;
                }
            }

        }

    }



    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider LeftWheel;
        public WheelCollider RightWheel;
        public bool Motor; // is this wheel attached to motor?
        public bool Steering; // does this wheel apply steer angle?
    }

}