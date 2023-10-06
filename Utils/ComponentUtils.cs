using UnityEngine;

namespace DeltasInteractions.Utils
{
    public static class ComponentUtils
    {
        public static T GetComponentInHighestParent<T>(this Transform startObject)
            where T : Component
        {
            return startObject.GetHighestParent().GetComponent<T>();
        }
        
        public static T[] GetComponentsInHighestParent<T>(this Transform startObject)
            where T : Component
        {
            return startObject.GetHighestParent().GetComponents<T>();
        }
        
        public static T GetComponentInHighestParentChildren<T>(this Transform startObject)
            where T : Component
        {
            return startObject.GetHighestParent().GetComponentInChildren<T>();
        }
        
        public static T[] GetComponentsInHighestParentChildren<T>(this Transform startObject)
            where T : Component
        {
            return startObject.GetHighestParent().GetComponentsInChildren<T>();
        }
    }
}
