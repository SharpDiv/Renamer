using System.Collections.Generic;

namespace Renamer.Model
{
    /// <summary>Group files renamer</summary>
    public interface IMechanism
    {
        /// <summary>Get friendly (for UI) name </summary>
        string FriendlyName { get; }

        /// <summary>Rename files</summary>
        /// <param name="format">Filename format.
        /// Like String.Format(), I expect that threre is no invalid characters and this word:"{0}"</param>
        /// <param name="files">Paths to files</param>
        void Rename(string format, List<string> files);
    }
}
