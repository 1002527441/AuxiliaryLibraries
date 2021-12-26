using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace AuxiliaryLibraries.Core.AuxilaryServices
{
    /// <summary>
    /// AuxiliaryZip helps you to Compress files into zip and Decompress zip files
    /// </summary>
    public static class AuxiliaryZip
    {
        /// <summary>
        /// Create a ZIP file of the files provided.
        /// </summary>
        /// <param name="fileNames">The full path and name to store the ZIP file at.</param>
        /// <param name="destinationFileName">The list of files to be added.</param>
        public static void Compress(IEnumerable<string> fileNames, string destinationFileName)
        {
            // Create and open a new ZIP file
            if (File.Exists(destinationFileName))
                File.Delete(destinationFileName);

            foreach (var filePath in fileNames)
            {
                FileInfo fileToCompress = new FileInfo(filePath);
                using (FileStream originalFileStream = fileToCompress.OpenRead())
                {
                    if ((File.GetAttributes(fileToCompress.FullName) &
                       FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                    {
                        using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                        {
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                               CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);

                            }
                        }
                        FileInfo info = new FileInfo(destinationFileName);
                        Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                        fileToCompress.Name, fileToCompress.Length.ToString(), info.Length.ToString());
                    }

                }
            }
        }

        /// <summary>
        /// Compress entire folder by passing directory Path
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void Compress(this string directoryPath)
        {
            DirectoryInfo directorySelected = new DirectoryInfo(directoryPath);
            foreach (FileInfo fileToCompress in directorySelected.GetFiles())
            {
                using (FileStream originalFileStream = fileToCompress.OpenRead())
                {
                    if ((File.GetAttributes(fileToCompress.FullName) &
                       FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                    {
                        using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                        {
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                               CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);

                            }
                        }
                        FileInfo info = new FileInfo(directoryPath + "\\" + fileToCompress.Name + ".gz");
                        Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                        fileToCompress.Name, fileToCompress.Length.ToString(), info.Length.ToString());
                    }

                }
            }
        }

        /// <summary>
        /// Decompress *.zip file, if pathIsDirectory pass as false. (One Zip file)
        /// Decompress every *.zip files inside filder by passing directory Path, if pathIsDirectory pass as true. (Multiple Zip files)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pathIsDirectory"></param>
        public static void Decompress(this string path, bool pathIsDirectory = false)
        {
            if (pathIsDirectory)
            {
                DirectoryInfo directorySelected = new DirectoryInfo(path);
                foreach (FileInfo fileToDecompress in directorySelected.GetFiles("*.gz"))
                {
                    fileToDecompress.Decompress();
                }
            }
            else
            {
                FileInfo fileToDecompress = new FileInfo(path);
                fileToDecompress.Decompress();
            }
        }

        /// <summary>
        /// Decompress *.zip file by passing FileInfo
        /// If you pass newFileName, the zip file extracted on this path,
        /// otherwise it (if you pass newFileName as null, default value) the zip file extracted on the parent folder
        /// </summary>
        /// <param name="fileToDecompress">the file info of the *.zip file</param>
        /// <param name="newFileName">It is optional</param>
        public static void Decompress(this FileInfo fileToDecompress, string newFileName = null)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;

                if (string.IsNullOrEmpty(newFileName))
                    newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
                }
            }
        }
    }
}