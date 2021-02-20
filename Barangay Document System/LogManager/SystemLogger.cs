using Serilog;
using Serilog.Enrichers;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Barangay_Document_System.LogManager
{
   public class SystemLogger
   {
      private static ILogger logger;
      private enum LogLevel
      {
         Error,
         Warn,
         Debug,
         Info,
         Fatal
      }

      public SystemLogger()
      {
         SetupLogger("System.log");
      }

      public SystemLogger(string logfilepath)
      {
         SetupLogger(logfilepath);
      }

      public static void SetupLogger(string logPath)
      {
         Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.With(new ThreadIdEnricher(), new MachineNameEnricher())
                .WriteTo.File($@"{logPath}", rollingInterval: RollingInterval.Day
                , fileSizeLimitBytes: 15000000
                , outputTemplate: "[{ThreadId}] {Timestamp:G} [{Level}] {Message}{NewLine}{Exception}"
                , shared: true)
                .CreateLogger();
         logger = Log.Logger;
      }

      /// <summary>
      /// To log error type of message
      /// </summary>
      /// <param name="message">Content to be displayed inside the log</param>
      /// <param name="args">Additional content to pass to method if any</param>
      public void LogError(string message)
      {
         StackFrame getStack = new StackFrame(1, true);
         string methodName = getStack.GetMethod().Name;
         Logger(methodName, LogLevel.Error, message);
      }

      /// <summary>
      /// To log warning type of message
      /// </summary>
      /// <param name="message">Content to be displayed inside the log</param>
      /// <param name="args">Additional content to pass to method if any</param>
      public void LogWarn(string message)
      {
         StackFrame getStack = new StackFrame(1, true);
         string methodName = getStack.GetMethod().Name;
         Logger(methodName, LogLevel.Warn, message);
      }

      /// /// <summary>
      /// To log debug type of message; only for debug and development usage, will be control by "EnableDebug" flag in config file;
      /// By default turn off with "false"
      /// </summary>
      /// <param name="message">Content to be displayed inside the log</param>
      /// <param name="args">Additional content to pass to method if any</param>
      public void LogDebug(string message)
      {
         StackFrame getStack = new StackFrame(1, true);
         string methodName = getStack.GetMethod().Name;
         Logger(methodName, LogLevel.Debug, message);

      }

      /// <summary>
      /// To log information type of message
      /// </summary>
      /// <param name="message">Content to be displayed inside the log</param>
      /// <param name="args">Additional content to pass to method if any</param>
      public void LogInfo(string message)
      {
         StackFrame getStack = new StackFrame(1, true);
         string methodName = getStack.GetMethod().Name;
         Logger(methodName, LogLevel.Info, message);
      }

      /// <summary>
      /// To log fatal error type of message
      /// </summary>
      /// <param name="message">Content to be displayed inside the log</param>
      /// <param name="args">Additional content to pass to method if any</param>
      public void LogFatal(string message)
      {
         StackFrame getStack = new StackFrame(1, true);
         string methodName = getStack.GetMethod().Name;
         Logger(methodName, LogLevel.Fatal, message);
      }

      static void Logger(string methodName, LogLevel logLevel, string message, Exception exception = null)
      {
         string logMessage = $"{methodName}() - {message}";
         switch (logLevel)
         {
            case LogLevel.Error:
               logger.Error(exception, logMessage);
               break;
            case LogLevel.Warn:
               logger.Warning(logMessage);
               break;
            case LogLevel.Debug:
               logger.Debug(logMessage);
               break;
            case LogLevel.Info:
               logger.Information(logMessage);
               break;
            case LogLevel.Fatal:
               logger.Fatal(logMessage);
               break;
         }
         Console.WriteLine(logMessage);
      }
   }
}
