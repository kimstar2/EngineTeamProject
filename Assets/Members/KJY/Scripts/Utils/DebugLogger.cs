using System;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public static class DebugLogger
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public static void Log(object message)
            => Debug.Log(message);
        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogWarning(object message)
            => Debug.LogWarning(message);
        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogError(object message)
            => Debug.LogError(message);
        // ReSharper disable Unity.PerformanceAnalysis
        public static void Assert(bool condition) 
            => Debug.Assert(condition);     

        // ReSharper disable Unity.PerformanceAnalysis
        public static void Assert(bool condition, string message) 
            => Debug.Assert(condition,message);
        
        
        // ReSharper disable Unity.PerformanceAnalysis
        public static void Assert(bool condition, string message, UnityEngine.Object context)
            => Debug.Assert(condition,message,context);
        
        // ReSharper disable Unity.PerformanceAnalysis
        public static void Assert(bool condition, UnityEngine.Object context)
            => Debug.Assert(condition,context);

        // ReSharper disable Unity.PerformanceAnalysis
        public static void LogException(Exception exception)
            => Debug.LogException(exception);
    }
}