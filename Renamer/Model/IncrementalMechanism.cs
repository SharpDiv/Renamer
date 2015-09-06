using System;
using System.IO;
using Renamer.Helpers;
using System.Collections.Generic;
using System.Globalization;
using Renamer.Properties;

namespace Renamer.Model
{
    /// <summary> Incremental file rename mechanism. Names will be from 0 to n </summary>
    public class IncrementalMechanism : IMechanism
    {
        /// <summary>Get friendly (for UI) name </summary>
        public string FriendlyName
        {
            get
            {
                return Resources.MechanismIncremental;
            }
        }

        /// <summary>Rename files</summary>
        /// <param name="format">Filename format.
        /// Like String.Format(), I expect that threre is no invalid characters and this word:"{0}"</param>
        /// <param name="files">Paths to files</param>
        public void Rename(string format, List<string> files)
        {
            if (files == null) { return;}

            for (var i = 0; i < files.Count; i++)
            {
                var newFilename = String.Format
                    (
                        format, 
                        i.ToString("D" + files.Count.ToString(CultureInfo.InvariantCulture).Length) + Path.GetExtension(files[i])
                    );

                if (Path.GetFileName(files[i]) == newFilename) { continue; }

                var dirName = Path.GetDirectoryName(files[i]);
                if (dirName == null) { return; }

                var newFilePath = Path.Combine
                    (
                        dirName,
                        UtilsFiles.SeekAvailableFilename(newFilename, dirName)
                    );

                
                File.Move(files[i], newFilePath);
            }
        }

    }
}
 