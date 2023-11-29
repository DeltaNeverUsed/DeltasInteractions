using System;
using UdonSharp;
using VRC.Udon.Common.Enums;

namespace DeltasInteractions.Utils
{
    public abstract class UpdateSleepable : UdonSharpBehaviour
    {
        [NonSerialized] public bool UpdateAwake;
        [NonSerialized] public bool LateUpdateAwake;
        
        public void UpdateWake(bool now = false)
        {
            if (UpdateAwake || !(gameObject.activeInHierarchy && enabled))
                return;
            UpdateAwake = true;
            
            if (now)
                SubUpdate();
            else
                SendCustomEventDelayedFrames(nameof(SubUpdate), 1);
        }
        
        public void UpdateSleep()
        {
            UpdateAwake = false;
        }
        
        public void LateUpdateWake(bool now = false)
        {
            if (LateUpdateAwake || !(gameObject.activeInHierarchy && enabled))
                return;
            LateUpdateAwake = true;
            
            if (now)
                LateSubUpdate();
            else
                SendCustomEventDelayedFrames(nameof(LateSubUpdate), 1, EventTiming.LateUpdate);
        }
        
        public void LateUpdateSleep()
        {
            LateUpdateAwake = false;
        }

        public virtual void SubUpdate()
        {
            if (UpdateAwake && gameObject.activeInHierarchy && enabled)
                SendCustomEventDelayedFrames(nameof(SubUpdate), 1);
            else
                UpdateSleep();
        }
        
        public virtual void LateSubUpdate()
        {
            if (LateUpdateAwake && gameObject.activeInHierarchy && enabled)
                SendCustomEventDelayedFrames(nameof(LateSubUpdate), 1, EventTiming.LateUpdate);
            else
                LateUpdateSleep();
        }
    }
}
