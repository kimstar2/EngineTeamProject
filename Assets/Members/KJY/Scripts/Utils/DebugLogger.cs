using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public static class DebugLogger
    {
        public static void Log(object message)
            => Debug.Log(message);
        public static void LogWarning(object message)
            => Debug.LogWarning(message);
        public static void LogError(object message)
            => Debug.LogError(message);
        public static void Assert(bool condition) 
            => Debug.Assert(condition);     
        public static void Assert(bool condition, string message) 
            => Debug.Assert(condition,message);
        public static void Assert(bool condition, string message, UnityEngine.Object context)
            => Debug.Assert(condition,message,context);
        public static void Assert(bool condition, UnityEngine.Object context)
            => Debug.Assert(condition,context);
    }
}