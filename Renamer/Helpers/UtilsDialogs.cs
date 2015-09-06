using System.Collections.Generic;
using Microsoft.Win32;
using System.Linq;

namespace Renamer.Helpers
{
    public static class UtilsDialogs
    {
        /// <summary>Show open file dialog</summary>
        /// <param name="title">Dialog title</param>
        /// <param name="multiselect">Allow multiselect</param>
        /// <returns>Selected files</returns>
        public static List<string> ShowOpenFileDialog(string title, bool multiselect = true)
        {
            //NOTE: MVVM break, and... oh, well
            var dlg = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = multiselect,
                ReadOnlyChecked = true,
                Title = title
            };

            if (dlg.ShowDialog() == true)
            {
                return dlg.FileNames.ToList();
            }

            return new List<string>();
        }
    }
}
