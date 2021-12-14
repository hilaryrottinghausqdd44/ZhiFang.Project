using System;
using System.Diagnostics;
using System.Reflection;
using ZhiFang.Common.Log;

namespace ZhiFang.LabStar.Common
{
    /// <summary>
    /// 日志公共方法
    /// </summary>
    public class LogHelp
    {
        public LogHelp() { }

        public static void BeginTimeTick() { }

        public static void Debug(string message)
        {
            StackTrace trace = new StackTrace();
            MethodBase methodBase = trace.GetFrame(1).GetMethod();
            string logCurInfo = GetLogCurInfo(methodBase);
            Log.Debug(logCurInfo + "   " + message);
        }

        public static void Debug(string message, Exception ex) { }

        public static void EndTimeTick(string message) { }

        public static void Error(string message)
        {
            StackTrace trace = new StackTrace();
            MethodBase methodBase = trace.GetFrame(1).GetMethod();
            string logCurInfo = GetLogCurInfo(methodBase);
            Log.Error(logCurInfo + "   " + message);
        }

        public static void Error(string message, Exception ex) { }

        public static void Fatal(string message)
        {
            StackTrace trace = new StackTrace();
            MethodBase methodBase = trace.GetFrame(1).GetMethod();
            string logCurInfo = GetLogCurInfo(methodBase);
            Log.Fatal(logCurInfo + "   " + message);
        }

        public static void Fatal(string message, Exception ex) { }

        public static void Info(string message)
        {
            StackTrace trace = new StackTrace();
            MethodBase methodBase = trace.GetFrame(1).GetMethod();
            string logCurInfo = GetLogCurInfo(methodBase);
            Log.Info(logCurInfo + "   " + message);
        }

        public static void Info(string message, Exception ex) { }

        public static void Warn(string message)
        {
            StackTrace trace = new StackTrace();
            MethodBase methodBase = trace.GetFrame(1).GetMethod();
            string logCurInfo = GetLogCurInfo(methodBase);
            Log.Warn(logCurInfo + "   " + message);
        }

        public static void Warn(string message, Exception ex) { }

        private static string GetLogCurInfo(MethodBase methodBase)
        {
            string logCurInfo = "";
            if (methodBase != null)
            {
                //logCurStr = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace ;
                //取得当前方法类全名 包括命名空间    
                logCurInfo = methodBase.DeclaringType.FullName;
                //取得当前方法名    
                logCurInfo += "." + methodBase.Name;
            }
            return logCurInfo;
        }

    }
}
