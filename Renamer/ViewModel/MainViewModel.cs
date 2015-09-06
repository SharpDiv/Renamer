using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Renamer.Helpers;
using Renamer.Model;
using Renamer.Properties;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Renamer.ViewModel
{
    /// <summary> This class contains properties that the main View can data bind to. </summary>
    public class MainViewModel : ViewModelBase,  IDataErrorInfo
    {
        /// <summary> <see cref="SelectAndRenameCommand"/> </summary>
        private IMechanism _selectedMechanism;

        /// <summary> <see cref="Format"/> </summary>
        private string _format = "{0}";

        /// <summary>Renaming mechanisms</summary>
        public ObservableCollection<IMechanism> RenameMechanisms { get; private set; }

        /// <summary> Command, select files & rename them all</summary>
        public RelayCommand SelectAndRenameCommand { get; set; }

        /// <summary> Get/set filename format</summary>
        public string Format
        {
            get
            {
                return _format;
            }
            set
            {
                if (_format == value) { return; }

                _format = value;
                RaisePropertyChanged();
                SelectAndRenameCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>Selected renaming mechanism</summary>
        public IMechanism SelectedMechanism
        {
            get
            {
                return _selectedMechanism;
            }
            set
            {
                if (_selectedMechanism == value) { return; }

                _selectedMechanism = value;
                RaisePropertyChanged();
                SelectAndRenameCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary> Initializes a new instance of the MainViewModel class. </summary>
        public MainViewModel()
        {
            SelectAndRenameCommand = new RelayCommand(Run, () => SelectedMechanism != null && Format.Contains("{0}"));

            RenameMechanisms = new ObservableCollection<IMechanism>
            {
                new IncrementalMechanism(), 
                new MD5Mechanism(),
                new CreateDateMechanism()
            };
        }

        /// <summary> Run selected mechanism </summary>
        private void Run()
        {
            if (SelectedMechanism == null) { return;}

            var files = UtilsDialogs.ShowOpenFileDialog(Resources.DialogTitleFileSelect);
            if (files == null || !files.Any()) { return;}

            if (!Format.Contains("{0}")) { return;}

            _selectedMechanism.Rename(Format, files);
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Format":
                        {
                            if (!Format.Contains("{0}")) { return Resources.FormatZeroError; }

                            break;
                        }
                }

                return String.Empty;
            }
        }
    }
}