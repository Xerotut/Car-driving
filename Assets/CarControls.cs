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
                axleInfo.ApplyLocalPositionToVisuals(axleInfo.LeftWheel, axleInfo.VisualLeft);
                axleInfo.ApplyLocalPositionToVisuals(axleInfo.RightWheel, axleInfo.VisualRight);
            }

        }

        
    }



    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider LeftWheel;
        public Transform VisualLeft;
        public WheelCollider RightWheel;
        public Transform VisualRight;
        public bool Motor; // is this wheel attached to motor?
        public bool Steering; // does this wheel apply steer angle?

        public void ApplyLocalPositionToVisuals(WheelCollider collider, Transform visualWheel)
        {

            Vector3 position;
            Quaternion rotation;
            collider.GetWorldPose(out position, out rotation);

          //  visualWheel.transform.position = position;
            visualWheel.transform.rotation = rotation;
        }
    }

}