using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework
{
    public class LoggerConsole
    {
#if Unity
        public static void Show(string str, string stackTrace)
        {
            UnityEngine.Debug.Log(str);
        }
#else
        public static void Show(string str, string stackTrace)
        {
            Console.WriteLine(str);
        }
#endif
    }
}
