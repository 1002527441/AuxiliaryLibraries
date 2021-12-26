using System;
using System.IO;

namespace AuxiliaryLibraries.Core.AuxilaryServices
{
    /// <summary>
    /// AuxiliaryLog helps you to log texts, to a *.log file
    /// </summary>
    public static class AuxiliaryLog
    {
        /// <summary>
        /// Default Log Path
        /// </summary>
        public static string DefaultLogPath { get { return $"{Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))}/Log.log"; } }

        /// <summary>
        /// "Log" saves Exception
        /// You can call Log multiple times, and texts every time appends to the one log file
        /// If you don't path log file path, it saves inside the bin folder
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="path"></param>
        public static void Log(this Exception ex, string path = "")
        {
            ex.ToString().Log(path);
        }

        /// <summary>
        /// Log save text inside the path
        /// You can call Log multiple times, and pass the same path, texts every time appends to the one log file
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        public static void Log(this string text, string path = "")
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    path = DefaultLogPath;
                using (StreamWriter SW = File.AppendText(path))
                {
                    SW.WriteLine("{0}", text);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        /// <summary>
        /// LogToNewFile saves text
        /// If you don't path log file path, it saves inside the bin folder
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        public static void LogToNewFile(this string text, string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = DefaultLogPath;
            try
            {
                File.WriteAllText(path, text);
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// You can read log files by Read
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Read(this string path)
        {
            try
            {
                string messageBody = string.Empty;
                var message = File.Exists(path) ? File.OpenText(path) : null;
                messageBody = message != null ? message.ReadToEnd() : string.Empty;
                return messageBody;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}