using System;
using Random = UnityEngine.Random;

namespace Members.KJY.Scripts.Utils
{
    public static class ArrayUtils
    {
        public static T AllRandom<T>(this T[] array)
        {
            return array.Length == 0 ? default : array[Random.Range(0, array.Length)];
        }
    }
}