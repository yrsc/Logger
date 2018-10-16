using System.Diagnostics;
using System.Text;
using System;

namespace Framework
{
    public class Logger
    {
        public enum LoggerType
        {
            Unity,
            Console
        }

        private static Action<string, string> _InternalLog = null;

        public static void RegisterLogFunc(Action<string, string> logFunc)
        {
            _InternalLog = logFunc;
        }

        public static void UnRegisterLogFunc(Action<string, string> logFunc)
        {
            _InternalLog = null;
        }

        public static void Log(string str)
        {
            StackTrace st = new StackTrace(true);
            StackFrame[] sf = st.GetFrames();
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < sf.Length; i++)
            {
                sb.AppendLine(string.Format("{0}:{1}()(at {2}:{3})", sf[i].GetMethod().DeclaringType.FullName, sf[i].GetMethod().Name, sf[i].GetFileName(), sf[i].GetFileLineNumber()));
            }
            LoggerConsole.Show(str, sb.ToString());
            if (_InternalLog != null)
            {
                _InternalLog(str, sb.ToString());
            }     
        }
    }
}

