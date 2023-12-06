using System;
using VRC.SDKBase;

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
            return array.RemoveIndex(array.IndexOf(item));
        }
        
        public static T[] RemoveIndex<T>(this T[] array, int index)
        {
            if (index < 0)
                return array;

            var newArray = new T[array.Length - 1];
            if (index == 0)
            {
                Array.Copy(array, 1, newArray, 0, newArray.Length);
                return newArray;
            }
            
            Array.Copy(array, 0, newArray, 0, index);

            if (index == newArray.Length)
                return newArray;
            
            Array.Copy(array, index+1, newArray, index, newArray.Length - index);

            return newArray;
        }
        
        public static T[] RemoveAll<T>(this T[] array, T item)
        {
            var tempArr = new T[array.Length];
            array.CopyTo(tempArr, 0);
            
            var offset = 0;
            for (var index = 0; index < tempArr.Length; index++)
            {
                if (!tempArr[index].Equals(item))
                    continue;
                array = array.RemoveIndex(index - offset);
                offset++;
            }

            return array;
        }
        
        public static T[] RemoveAllInvalid<T>(this T[] array)
        {
            var tempArr = new T[array.Length];
            array.CopyTo(tempArr, 0);
            
            var offset = 0;
            for (var index = 0; index < tempArr.Length; index++)
            {
                if (Utilities.IsValid(tempArr[index]))
                    continue;
                array = array.RemoveIndex(index - offset);
                offset++;
            }

            return array;
        }
    }
}
