using System.Diagnostics;
using System.Text;
using System.IO;

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

    public struct LogColor
    {
        public float r;
        public float g;
        public float b;

        public LogColor(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public static LogColor red = new LogColor(1f, 0f, 0f);

        public static LogColor green = new LogColor(0f, 1f, 0f);

        public static LogColor blue = new LogColor(0f, 0f, 1f);

        public static LogColor white = new LogColor(1f, 1f, 1f);

        public static LogColor black = new LogColor(0f, 0f, 0f);

        public static LogColor yellow = new LogColor(1f, 0.921568632f, 0.0156862754f);

        public static LogColor cyan = new LogColor(0f, 1f, 1f);

        public static LogColor magenta = new LogColor(1f, 0f, 1f);

        public static LogColor gray = new LogColor(0.5f, 0.5f, 0.5f);
    }

    public static class CommonLogger 
    {

        static CommonLogger()
        {
        }

        private static void ClearWriter()
        {
            if (_writer != null)
            {
                _writer.Flush();
                _writer.Close();
                _writer.Dispose();
                _writer = null;
            }
        }

        public static void Dispose()
        {
            ClearWriter();
        }

        private static int _mask = -1;
        private static string _logPath = "";
        private static StreamWriter _writer = null;

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

        public static void SetWritePath(string path)
        {
            _logPath = path;
            ClearWriter();
        }

        private static void Write(string log, string stackTrace)
        {
            if(string.IsNullOrEmpty(_logPath))
            {
                Log(LoggerLevel.Error, "Can not write log because log path not initialized, initialize the log path via function 'SetWritePath()' ");
                return;
            }
            if(_writer == null)
            {
                _writer = File.AppendText(_logPath);
            }
            _writer.WriteLine(string.Format("{0}\n{1}", log, stackTrace));
        }

        private static string GetStackTrace()
        {
            StackTrace st = new StackTrace(true);
            StackFrame[] sf = st.GetFrames();
            StringBuilder sb = new StringBuilder();
            for (int i = 3; i < sf.Length; i++)
            {
                sb.AppendLine(string.Format("{0}:{1}()(at {2}:{3})", sf[i].GetMethod().DeclaringType.FullName, sf[i].GetMethod().Name, sf[i].GetFileName(), sf[i].GetFileLineNumber()));
            }
            return sb.ToString();
        }


#if Unity

        private static string ConvertColor(LogColor color)
        {
            string colorStr = string.Format("#{0}{1}{2}", 
                              ((int)(color.r * 255)).ToString("X2"),
                              ((int)(color.g * 255)).ToString("X2"),
                              ((int)(color.b * 255)).ToString("X2"));
            return colorStr;
        }

        private static void ShowLog(LoggerLevel loggerLevel, string log, string stackTrace, LogColor color)
        {
            if (loggerLevel == LoggerLevel.Error)
            {
                UnityEngine.Debug.LogError(string.Format("<color={0}>{1}</color>\n{2}", ConvertColor(color), log, stackTrace));
            }
            else if (loggerLevel == LoggerLevel.Warning)
            {
                UnityEngine.Debug.LogWarning(string.Format("<color={0}>{1}</color>\n{2}", ConvertColor(color), log, stackTrace));
            }
            else
            {
                UnityEngine.Debug.Log(string.Format("<color={0}>{1}</color>\n{2}", ConvertColor(color), log, stackTrace));
            }
        }

#else
        private static void ShowLog(LoggerLevel loggerLevel, string log, string stackTrace, LogColor color)
        {
            Console.WriteLine(log);
        }
#endif

        public static void LogAndWrite(LoggerLevel logLevel, string log)
        {
            LogAndWrite(logLevel, log, LogColor.white);
        }

        public static void LogAndWrite(LoggerLevel logLevel, string log, bool needStackTrace)
        {
            LogAndWrite(logLevel, log, needStackTrace, LogColor.white);
        }

        public static void LogAndWrite(LoggerLevel logLevel, string log, LogColor color)
        {
            LogAndWrite(logLevel, log, false, LogColor.white);
        }

        public static void LogAndWrite(LoggerLevel logLevel, string log, bool needStackTrace, LogColor color)
        {
            string stackTrace = null; 
            if(needStackTrace)
            {
                stackTrace = GetStackTrace();
            }
            ShowLog(logLevel, log, stackTrace, color);
            Write(log, stackTrace);
        }

        public static void Log(LoggerLevel logLevel, string log)
        {
            Log(logLevel, log, LogColor.white);
        }

        public static void Log(LoggerLevel logLevel, string log, LogColor color)
        {
            Log(logLevel, log, false, color);
        }

        public static void Log(LoggerLevel logLevel, string log, bool needStackTrace)
        {
            Log(logLevel, log, needStackTrace, LogColor.white);
        }

        public static void Log(LoggerLevel logLevel, string log, bool needStackTrace, LogColor color)
        {
            if (CheckCanLog(logLevel))
            {
                string stackTrace = null;
                if (needStackTrace)
                {
                    stackTrace = GetStackTrace();
                }
                ShowLog(logLevel, log, stackTrace, color);
            }
        }
    }
}

