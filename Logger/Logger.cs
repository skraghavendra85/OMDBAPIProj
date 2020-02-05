using System;

namespace Logger
{
    public class Logger : ILogger
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void LogCritical(string strMessage)
        {
            log.Fatal(strMessage);
        }

        public void LogError(string strMessage)
        {
            log.Error(strMessage);
        }

        public void LogInfo(string strMessage)
        {
            log.Info(strMessage);
        }

        public void Warning(string strMessage)
        {
            log.Warn(strMessage);
        }
    }
}
