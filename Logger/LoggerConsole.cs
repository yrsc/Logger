using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    public class LoggerConsole
    {
#if Unity
        public static void Show(Logger.LoggerLevel level, string str, string stackTrace)
        {
            if (level == Logger.LoggerLevel.Info)
            {
                UnityEngine.Debug.Log(string.Format("<color={0}>{1}</color>", "green", str));
            }
            if (level == Logger.LoggerLevel.Debug)
            {
                UnityEngine.Debug.Log(string.Format("<color={0}>{1}</color>", "white", str));
            }
            else if(level == Logger.LoggerLevel.Warning)
            {
                UnityEngine.Debug.LogWarning(string.Format("<color={0}>{1}</color>", "yellow", str));
            }
            else if(level == Logger.LoggerLevel.Error)
            {
                UnityEngine.Debug.LogError(string.Format("<color={0}>{1}</color>", "red", str));
            }
        }
#else
        public static void Show(string str, string stackTrace)
        {
            Console.WriteLine(str);
        }
#endif
    }
}
