using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Renamer.Helpers
{
    /// <summary>Work with files utils</summary>
    static class UtilsFiles
    {
        /// <summary>Seek if "filename" is not already taken in "directory"</summary>
        /// <param name="filename">Filename</param>
        /// <param name="directory">Directory where seek will goes</param>
        /// <returns></returns>
        public static string SeekAvailableFilename(string filename, string directory)
        {
            if (!File.Exists(Path.Combine(directory, filename))) { return filename; }

            const string FilenameFormat = "{0} ({1}){2}";
            var copyCount = 0;

            var extension = Path.GetExtension(filename);
            var name = Path.GetFileNameWithoutExtension(filename);

            while (File.Exists(Path.Combine(directory, String.Format(FilenameFormat, name, copyCount, extension))))
            {
                copyCount++;
            }

            
            return String.Format(FilenameFormat, name, copyCount, extension);
        }

        /// <summary>Calc file md5 hash code</summary>
        /// <param name="filename">File path</param>
        /// <returns>md5 hash code</returns>
        public static string GetMD5HashCode(string filename)
        {
            var hashcode = new StringBuilder();

            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                fs.Seek(0, SeekOrigin.Begin);
                
                var md5 = MD5.Create();

                foreach (var b in md5.ComputeHash(fs))
                {
                    hashcode.Append(b.ToString("x2"));
                }

                fs.Seek(0, SeekOrigin.Begin);

                return hashcode.ToString();
            }
        }

    }
}
