using System;
using UdonSharp;
using UnityEngine.Serialization;

namespace DeltasInteractions.TransformDrivers
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SnapTarget : UdonSharpBehaviour
    {
        public string Tag;
        public int maxSnaps = -1;
        [NonSerialized] public int CurrentSnaps = 0;
    }
}
