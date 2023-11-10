using UdonSharp;
using UnityEngine;

namespace DeltasInteractions.Utils
{
    public static class GameObjectUtils
    {
        public static string GetPath(Transform from, Transform to)
        {
            var currentParent = to;
            var path = "";
            while (true)
            {
                if (currentParent == null)
                    return "";
                if (currentParent == from)
                    return path;
                
                path = currentParent.name + "/" + path;
                currentParent = currentParent.parent;
            }
        }

        public static Transform GetObjectFromPath(Transform start, string path)
        {
            var splits = path.Split('/');
            var current = start;
            foreach (var split in splits)
            {
                current = current.Find(split);
                if (current == null)
                    return current;
            }
            return current;
        }
        
        public static Transform GetHighestParent(this Component startObject)
        {
            return GetHighestParent(startObject.transform);
        }
        
        public static Transform GetHighestParent(this Transform startObject)
        {
            var currentObject = startObject;
            for (var i = 0; i < 10; i++)
            {
                if (currentObject.parent == null)
                    break;
                currentObject = currentObject.parent;
            }

            return currentObject;
        }
        
        public static T[] GetComponentsInChildrenOfHighestParent<T>(this Component startObject)
        {
            return startObject.GetHighestParent().GetComponentsInChildren<T>();
        }
        
        public static T[] GetComponentsInChildrenOfHighestParent<T>(this Transform startObject)
        {
            return startObject.GetHighestParent().GetComponentsInChildren<T>();
        }

        public static T GetComponentInChildrenOfHighestParent<T>(this Component startObject)
        {
            return startObject.GetHighestParent().GetComponentInChildren<T>();
        }
        
        public static T GetComponentInChildrenOfHighestParent<T>(this Transform startObject)
        {
            return startObject.GetHighestParent().GetComponentInChildren<T>();
        }
        
        public static T GetComponentInHighestParent<T>(this Component startObject)
        {
            return startObject.GetHighestParent().GetComponent<T>();
        }
        
        public static T GetComponentInHighestParent<T>(this Transform startObject)
        {
            return startObject.GetHighestParent().GetComponent<T>();
        }
    }
}
