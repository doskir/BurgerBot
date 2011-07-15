using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BurgerBot
{
    class Log
    {
        public enum LoggingLevel {Spam,Debug,Info,Error};
        public static string LogString
        {
            get { return _log.ToString(); }
        }
        public static LoggingLevel LogLevel = LoggingLevel.Error;
        static StringBuilder _log = new StringBuilder();
        private static bool _saveLogToDisk;
        public static bool SaveLogToDisk
        {
            get { return _saveLogToDisk; }
            set
            {
                _saveLogToDisk = value;
#if DEBUG
                if (_saveLogToDisk)
                {
                    _logWriter = new StreamWriter("log.txt", true, Encoding.ASCII);
                    _logWriter.WriteLine("--------\r\nNew Log\r\n-------");
                }
                else
                {
                    _logWriter.Close();
                    _logWriter.Dispose();
                    _logWriter = null;
                }
#endif
            }
        }
        static StreamWriter _logWriter;
        public static void AddMessage(string message,LoggingLevel level)
        {
#if DEBUG
            if (level >= LogLevel)
            {
                var st = new StackTrace();
                Type t = st.GetFrame(1).GetMethod().ReflectedType;
                string s = t.Name + " " +
                           DateTime.Now.ToString("HH':'mm':'ss.fff ") + message;

                _log.AppendLine(s);
                if (SaveLogToDisk)
                {
                    if (_logWriter == null)
                        _logWriter = new StreamWriter("log.txt", true, Encoding.ASCII);
                    _logWriter.WriteLine(s);
                    _logWriter.Flush();
                }
            }
#endif
        }
        public static void Clear()
        {
            _log.Remove(0, _log.Length);
        }
    }
}
