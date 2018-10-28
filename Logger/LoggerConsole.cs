using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    public class LoggerConsole
    {
#if Unity

        private static string _defaultColor = "black";

        public static void Show(LoggerLevel level, string str, string stackTrac, string color = null)
        {
            var logColor = _defaultColor;
            if(!string.IsNullOrEmpty(color))
            {
                logColor = color;
            }
            if(level == LoggerLevel.Warning)
            {
                UnityEngine.Debug.LogWarning(string.Format("<color={0}>{1}</color>", logColor, str));
            }
            else if(level == LoggerLevel.Error)
            {
                UnityEngine.Debug.LogError(string.Format("<color={0}>{1}</color>", logColor, str));
            }
            else
            {
                UnityEngine.Debug.Log(string.Format("<color={0}>{1}</color>", logColor, str));
            }
        }
#else
        public static void Show(LoggerLevel level, string str, string stackTrace)
        {
            Console.WriteLine(str);
        }
#endif
    }
}
