using System;

namespace Hulkey.Common
{
    // Log
    // *notuse*Trace - very detailed logs, which may include high-volume information such as protocol payloads.This log level is typically only enabled during development
    // Debug - debugging information, less detailed than trace, typically not enabled in production environment.
    // Info - information messages, which are normally enabled in production environment
    // *notuse*Warn - warning messages, typically for non-critical issues, which can be recovered or which are temporary failures
    // Error - error messages - most of the time these are Exceptions
    // *notuse*Fatal - very serious errors!
    public class Log
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static public void Trace(string sMsg)
        {
            logger.Trace(sMsg);
        }
        static public void Trace(string sMsg, Exception ex)
        {
            logger.Trace(ex, sMsg);
        }

        static public void Debug(string sMsg)
        {
            logger.Debug(sMsg);
        }
        static public void Debug(string sMsg, Exception ex)
        {
            logger.Debug(ex, sMsg);
        }

        static public void Info(string sMsg)
        {
            logger.Info(sMsg);
        }

        static public void Info(string sMsg, Exception ex)
        {
            logger.Info(ex, sMsg);
        }

        static public void Error(string sMsg)
        {
            logger.Error(sMsg);
        }

        static public void Error(string sMsg, Exception ex)
        {
            logger.Error(ex, sMsg);
        }

    }
}
