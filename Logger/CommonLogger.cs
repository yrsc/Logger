using System.Diagnostics;
using System.Text;
using System;
using System.Collections.Generic;

namespace Framework
{
    public enum LoggerLevel
    {
        Info,
        Debug,
        Warning,
        Exception,
        Error
    }

    public static class CommonLogger
    {
        private static Dictionary<LoggerLevel, string> _loggerColor = new Dictionary<LoggerLevel, string>();

        static CommonLogger()
        {
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

        public static void SetLogColor(LoggerLevel level, string color)
        {
            _loggerColor[level] = color;
        }

        private static int _mask = -1;

        private static bool CheckCanLog(LoggerLevel logLevel)
        {
            if ((_mask & (1 << (int)logLevel)) > 0)
                return true;
            return false;
        }

        public static void SetMask(params LoggerLevel[] level)
        {
            if(level != null)
            {
                _mask = 0;
                for (int i = 0; i < level.Length; i++)
                {
                    _mask |= (1 << (int)level[i]);
                }
            }
        }

        public static void Log(LoggerLevel logLevel, string str)
        {            
            if(CheckCanLog(logLevel))
            {
                StackTrace st = new StackTrace(true);
                StackFrame[] sf = st.GetFrames();
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i < sf.Length; i++)
                {
                    sb.AppendLine(string.Format("{0}:{1}()(at {2}:{3})", sf[i].GetMethod().DeclaringType.FullName, sf[i].GetMethod().Name, sf[i].GetFileName(), sf[i].GetFileLineNumber()));
                }
                string color = null;
                _loggerColor.TryGetValue(logLevel, out color);
                LoggerConsole.Show(logLevel, str, sb.ToString(), color);
                if (_InternalLog != null)
                {
                    _InternalLog(str, sb.ToString());
                }
            }           
        }
    }
}

