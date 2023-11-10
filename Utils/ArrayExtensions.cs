using System;

namespace DeltasInteractions.Utils
{
    public static class ArrayExtensions
    {
        public static bool Contains<T>(this T[] haystack, T needle)
        {
            foreach (var hay in haystack)
                if (hay.Equals(needle))
                    return true;
            return false;
        }
        
        public static int IndexOf<T>(this T[] haystack, T needle)
        {
            return Array.IndexOf(haystack, needle);
        }

        public static T[] Add<T>(this T[] array, T item)
        {
            var len = array.Length + 1;
            var tempArray = new T[len];
            array.CopyTo(tempArray, 0);
            tempArray[len-1] = item;
            return tempArray;
        }
    }
}
