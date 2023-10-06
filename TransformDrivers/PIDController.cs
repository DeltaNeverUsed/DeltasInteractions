using System;
using DeltasInteractions.Extensions;
using UdonSharp;
using UnityEngine;
using UnityEngine.Serialization;

namespace DeltasInteractions.TransformDrivers
{
    public class PIDController : UdonSharpBehaviour
    {
        public float Kp = 1f;
        public float Ki = 0f;
        public float Kd = 0f;
        public float MaxChange = 1f;

        private Vector3 integral = Vector3.zero;
        private Vector3 previousError = Vector3.zero;

        public void ResetPID()
        {
            integral = Vector3.zero;
            previousError = Vector3.zero;
        }

        public Vector3 UpdatePID(float dt, Vector3 targetPosition, Vector3 currentPosition)
        {
            Vector3 error = targetPosition - currentPosition;
            integral += error * dt;
            Vector3 derivative = (error - previousError) / dt;

            Vector3 output = Vector3.Scale(Vector3.one * Kp, error) + Vector3.Scale(Vector3.one * Ki, integral) + Vector3.Scale(Vector3.one * Kd, derivative);
            
            // Update the previous error
            previousError = error;

            return output.Clamp(-MaxChange, MaxChange);
        }
    }
}
