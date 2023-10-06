using UdonSharp;

namespace DeltasInteractions.TransformDrivers
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class SnapTarget : UdonSharpBehaviour
    {
        public string Tag;
    }
}
