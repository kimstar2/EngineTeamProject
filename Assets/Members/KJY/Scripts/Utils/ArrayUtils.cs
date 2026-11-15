using System;
using Random = UnityEngine.Random;

namespace Members.KJY.Scripts.Utils
{
    public static class ArrayUtils
    {
        public static T AllRandom<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
    }
}