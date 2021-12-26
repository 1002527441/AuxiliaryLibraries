using System;
using System.IO;
using System.Net;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// AuxiliaryDirectoryFileHelper
    /// </summary>
    public static class AuxiliaryDirectoryFileHelper
    {
        /// <summary>
        /// Create Folder If it doesn't Exist, if folder has already exists, it retuens true
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreateFolderIfNeeded(this string path)
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

        /// <summary>
        /// Copy a Folder (including every files and sub folders in it) from 'sourcePath' to 'destinationPath'
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public static bool CopyFolder(this string sourcePath, string destinationPath)
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

        /// <summary>
        /// Copy a File from 'sourcePath' to 'destinationPath'
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public static bool CopyFile(this string sourcePath, string destinationPath)
        {
            try
            {
                File.Copy(sourcePath, destinationPath, true);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Download a file by its 'url' and save it to 'destination'
        /// </summary>
        /// <param name="url"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static bool Download(this string url, string destination)
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

        /// <summary>
        /// Get Mime Type From File Path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetMimeTypeFromFilePath(this string filePath)
        {
            var index = filePath.LastIndexOf('.');
            if (index <= 0) return string.Empty;
            var format = filePath.Substring(index, filePath.Length - index).Replace(".", string.Empty);
            return GetMimeTypeFromFileFormat(format);
        }

        /// <summary>
        /// Get Mime Type From File Format
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetMimeTypeFromFileFormat(this string format)
        {
            format = format.Replace(".", string.Empty);
            return format switch
            {
                "jpeg" => "image/jpeg",
                "jpg" => "image/jpeg",
                "mov" => "video/quicktime",
                "gif" => "image/gif",
                "ico" => "image/x-icon",
                "png" => "image/png",
                "bmp" => "image/bmp",
                "tif" => "image/tiff",
                "tiff" => "image/tiff",
                //"mp3" => "audio/mp3",
                //"mp3" => "audio/mpeg3",
                //"mp3" => "audio/x-mpeg-3",
                "mp3" => "audio/mpeg",
                "mp2" => "audio/mpeg",
                "mpga" => "audio/mpeg",
                "mpe" => "audio/mpeg",
                "mpeg" => "audio/mpeg",
                "mpg" => "audio/mpeg",
                "ogg" => "application/ogg",
                "3gp" => "video/3gpp",
                "mp4" => "video/mp4",
                "pdf" => "application/pdf",
                "zip" => "application/zip",
                "json" => "application/json",
                "xml" => "application/xml",
                "epub" => "application/epub+zip",
                "htm" => "text/html",
                "html" => "text/html",
                "css" => "text/css",
                "doc" => "application/msword",
                "exe" => "application/octet-stream",
                "ppt" => "application/vnd.ms-powerpoint",
                "txt" => "text/plain",
                "xls" => "application/vnd.ms-excel",
                "pem" => "application/x-x509-ca-cert",
                _ => string.Empty
            };
        }
    }
}
