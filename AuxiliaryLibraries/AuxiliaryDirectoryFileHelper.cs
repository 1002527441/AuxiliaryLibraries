using System;
using System.IO;
using System.Net;

namespace AuxiliaryLibraries
{
    public static class AuxiliaryDirectoryFileHelper
    {
        public static bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    // TODO: I must process this exception.
                    result = false;
                }
            }
            return result;
        }

        public static bool CopyFolder(string sourcePath, string destinationPath)
        {
            try
            {
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);

                return true;
            }
            catch { return false; }
        }

        public static bool CopyFile(string sourcePath, string destinationPath)
        {
            try
            {
                File.Copy(sourcePath, destinationPath, true);
                return true;
            }
            catch { return false; }
        }

        public static bool Download(string url, string destination)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(url, destination);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Undrasting this file is image or not
        /// </summary>
        /// <param name="contentType">Your file</param>
        /// <returns>If the file be png, jpeg, gif, bmp or icon, it will return true.</returns>
        public static bool IsImage(this string contentType)
        {
            const string pattern = "(image/(png|jpeg|gif|pjpeg|bmp|x-bmp|exif|icon|wmf|pjpeg|x-png))";
            var regex = new System.Text.RegularExpressions.Regex(pattern);

            return regex.IsMatch(contentType);
        }
    }
}
