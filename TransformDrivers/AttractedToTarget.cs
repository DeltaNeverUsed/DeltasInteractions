using System;
using UdonSharp;
using UnityEngine;

namespace DeltasInteractions.TransformDrivers
{
    [RequireComponent(typeof(Rigidbody))]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class AttractedToTarget : PIDController
    {
        public Transform target;
        public float maxVelocity = 10f;

        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            ResetPID();
        }

        private void FixedUpdate()
        {
            var newVel = _rb.velocity + UpdatePID(Time.fixedDeltaTime, target.position, transform.position);
            _rb.velocity = newVel.Clamp(-maxVelocity, maxVelocity);
        }
    }
}
