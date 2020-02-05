using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public interface ILogger
    {
        void LogInfo(string strMessage);
        void LogError(string strMessage);
        void LogCritical(string strMessage);
        void Warning(string strMessage);
    }
}
