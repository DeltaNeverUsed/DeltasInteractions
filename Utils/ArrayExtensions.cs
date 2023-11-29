using System;

namespace DeltasInteractions.Utils
{
    public static class ArrayExtensions
    {
        public static bool Contains<T>(this T[] haystack, T needle)
        {
            return Array.IndexOf(haystack, needle) != -1;
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
        
        public static T[] Concat<T>(this T[] array, T[] array2)
        {
            var ar1Len = array.Length;
            var ar2Len = array2.Length;

            if (ar2Len == 0)
                return array;
            
            var tempArray = new T[ar1Len + ar2Len];
            Array.Copy(array, 0, tempArray, 0, ar1Len);

            Array.Copy(array2, 0, tempArray, ar1Len, ar2Len);

            return tempArray;
        }

        public static T[] RemoveFirst<T>(this T[] array, T item)
        {
            var index = array.IndexOf(item);
            if (index == -1)
                return array;

            var newArray = new T[array.Length - 1];
            Array.Copy(array, 0, newArray, 0, index);

            if (index == newArray.Length)
                return newArray;
            
            Array.Copy(array, index+1, newArray, index, newArray.Length - index);

            return newArray;
        }
        
        public static T[] RemoveAll<T>(this T[] array, T item)
        {
            while (array.Contains(item))
                array = array.RemoveFirst(item);

            return array;
        }
    }
}
