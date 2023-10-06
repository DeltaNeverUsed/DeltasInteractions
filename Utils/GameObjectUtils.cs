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
    }
}
