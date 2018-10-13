using System;
using System.IO;

namespace AuxiliaryLibraries
{
    public static class AuxiliaryLog
    {
        public static string DefaultLogPath { get { return $"{Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))}/Log.log"; } }

        public static void SaveAsLog(string Message)
        {
            SaveAsLog(Message, DefaultLogPath);
        }

        public static void SaveAsLog(string Message, string Path)
        {
            try
            {
                using (System.IO.StreamWriter SW = System.IO.File.AppendText(Path))
                {
                    SW.WriteLine("{0}", Message);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public static void SaveAsFile(string content)
        {
            SaveAsFile(content, DefaultLogPath);
        }

        public static void SaveAsFile(string content, string path)
        {
            try
            {
                File.WriteAllText(path, content);
            }
            catch (Exception)
            {

            }
        }

        public static string ReadLogFile(string path)
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

        public static System.Collections.Generic.List<object> GetAsJsonString(this System.Net.Http.HttpRequestMessage Request)
        {
            System.Collections.Generic.List<object> Response = new System.Collections.Generic.List<object>();
            var header = Request.Headers.GetEnumerator();
            while (header.MoveNext())
            {
                Response.Add(new { Key = header.Current.Key.ToString(), Value = string.Join(",", header.Current.Value) });
            }
            return Response;
        }
    }
}
