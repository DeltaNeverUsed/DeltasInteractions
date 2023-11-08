using System;
using UdonSharp;
using UnityEngine;

using DeltasInteractions.Utils;
using UnityEngine.Serialization;
using VRC.SDK3.Components;
using VRC.SDK3.Data;

namespace DeltasInteractions.TransformDrivers
{
    [DefaultExecutionOrder(100)]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class Snapper : UdonSharpBehaviour
    {
        public string[] snappingTags = Array.Empty<string>();
        public bool snapToAnyTag;

        [Space]
        public float snappingDistance = 0.2f;
        public float snappingSpeed = 0.5f;
        
        //
        
        private SnapTarget[] _snapTargets = Array.Empty<SnapTarget>();
        private bool _hasSnapTargets;

        private Rigidbody _rb;
        private VRCPickup _pickup;
        private bool _hasVRCPickup;
        
        [NonSerialized] public SnapTarget SnapTarget;
        [NonSerialized] public bool IsSnapping;
        [NonSerialized] public bool IsFullySnapped;
        
        

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _pickup = GetComponent<VRCPickup>();
            _hasVRCPickup = _pickup != null;
            
            var allSnapTargets = transform.GetComponentsInHighestParentChildren<SnapTarget>();
            foreach (var target in allSnapTargets)
            {
                if (snapToAnyTag || snappingTags.Contains(target.Tag))
                    _snapTargets = _snapTargets.Add(target);
            }

            _hasSnapTargets = _snapTargets.Length > 0;
        }

        public override void OnPickup()
        {
            StopSnapping();
        }


        private int _checkIndex;
        private void Update()
        {
            if ( (_hasVRCPickup && _pickup.IsHeld) || !_hasSnapTargets)
                return;
            
            if (IsSnapping)
            {
                if (IsFullySnapped)
                    Snapped();
                else
                    GotoSnapped();
            }
            else
                NotSnapped();
        }

        public void StartSnapping(SnapTarget target)
        {
            target.CurrentSnaps += 1;
            
            _snapProgress = 0;
            SnapTarget = target;
            IsSnapping = true;
            IsFullySnapped = snappingSpeed < 0.01;
            
            _startPos = transform.position;
            _startRot = transform.rotation;
        }
        public void StopSnapping()
        {
            if (IsSnapping)
                SnapTarget.CurrentSnaps -= 1;
            SnapTarget = null;
            IsSnapping = false;
            IsFullySnapped = false;
        }

        private void NotSnapped()
        {
            var curr = _snapTargets[_checkIndex % _snapTargets.Length];
            
            if (curr.gameObject.activeInHierarchy && curr.CurrentSnaps != curr.maxSnaps)
                if (Vector3.Distance(curr.transform.position, transform.position) <= snappingDistance)
                    StartSnapping(curr);
            
            _checkIndex++;
        }

        private float _snapProgress;
        private Vector3 _startPos;
        private Quaternion _startRot;
        private void GotoSnapped()
        {
            _snapProgress += Time.deltaTime / snappingSpeed;

            var progger = Mathf.SmoothStep(0, 1, _snapProgress);

            var snapTargetTransform = SnapTarget.transform;
            
            transform.position = Vector3.Lerp(_startPos, snapTargetTransform.position, progger);
            transform.rotation = Quaternion.Slerp(_startRot, snapTargetTransform.rotation, progger);

            if (_snapProgress >= 1)
                IsFullySnapped = true;
        }
        private void Snapped()
        {
            var snapTargetTransform = SnapTarget.transform;
            transform.position = snapTargetTransform.position;
            transform.rotation = snapTargetTransform.rotation;

            if (_hasVRCPickup)
            {
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
