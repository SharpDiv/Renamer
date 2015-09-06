using System;
using System.IO;
using Renamer.Helpers;
using System.Collections.Generic;
using Renamer.Properties;

namespace Renamer.Model
{
    /// <summary> Create date file rename mechanism. Names will be file date creation</summary>
    class CreateDateMechanism : IMechanism
    {
        /// <summary>Get friendly (for UI) name </summary>
        public string FriendlyName
        {
            get
            {
                return Resources.MechanismCreateDate;
            }
        }

        /// <summary>Rename files</summary>
        /// <param name="format">Filename format.
        /// Like String.Format(), I expect that threre is no invalid characters and this word:"{0}"</param>
        /// <param name="files">Paths to files</param>
        public void Rename(string format, List<string> files)
        {
            if (files == null) { return; }

            foreach (var filename in files)
            {
                var newFilename = String.Format
                    (
                        format,
                        File.GetCreationTime(filename).ToString("yyyy-MM-dd HH-mm-ss") + Path.GetExtension(filename)
                    );

                if (Path.GetFileName(filename) == newFilename) { continue; }

                var dirName = Path.GetDirectoryName(filename);
                if (dirName == null) { return; }

                var newFilePath = Path.Combine
                    (
                        dirName,
                        UtilsFiles.SeekAvailableFilename(newFilename, dirName)
                    );


                File.Move(filename, newFilePath);
            }
        }

    }
}
