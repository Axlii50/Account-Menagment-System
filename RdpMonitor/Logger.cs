using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RdpMonitor
{
    internal static class Logger
    {
        private static string logFilePath = "logfile.txt";
        public static EventLog eventLog = new EventLog();

        public static void WriteLine(string message)
        {
#if DEBUG
            Console.WriteLine($"{DateTime.Now}: {message}");
#else
            new EventLog().WriteEntry(message);
            LogToFile(message);
#endif
        }

        public static void WriteLine(int message)
        {
#if DEBUG
            Console.WriteLine(message.ToString());
#else
            new EventLog().WriteEntry(message.ToString());
            LogToFile(message.ToString());
#endif
        }

        public static void WriteLine(long message)
        {
#if DEBUG
            Console.WriteLine(message.ToString());
#else
            new EventLog().WriteEntry(message.ToString());
            LogToFile(message.ToString());
#endif
        }

        private static void LogToFile(string message)
        {
            try
            {
                File.AppendAllText(logFilePath, $"{DateTime.Now}: {message} {Environment.NewLine}");
               
            }
            catch (Exception ex)
            {
                // Można tutaj dodać dodatkowe logowanie błędów, jeśli potrzebne
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }
    }
}
