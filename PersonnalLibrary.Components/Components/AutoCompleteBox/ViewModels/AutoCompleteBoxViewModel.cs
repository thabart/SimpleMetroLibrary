using PersonnalLibrary.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace PersonnalLibrary.Components.ViewModels
{
    public class AutoCompleteBoxViewModel : INotifyPropertyChanged
    {
        #region Fields

        private string _waterMarkValue;

        private object _selectedItem;
        
        #endregion

        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public AutoCompleteBoxViewModel()
        {
            _waterMarkValue = string.Empty;

            LabelsSource = new ObservableCollection<object>();
            SelectedLabelsSource = new ObservableCollection<object>();

            DeleteLabelCommand = new DelegateCommand(OnDeleteLabelCommandExecuted);
        }

        #endregion

        #region Properties

        public ICommand DeleteLabelCommand { get; set; }

        public IEnumerable<object> ItemsSource { get; set; }

        public ObservableCollection<object> LabelsSource { get; set; }

        public ObservableCollection<object> SelectedLabelsSource { get; set; }

        public object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (value != null)
                {
                    _selectedItem = value;
                    LabelsSource.Remove(_selectedItem);
                    SelectedLabelsSource.Add(_selectedItem);
                    RaisePropertyChanged("SelectedItem");
                }
            }
        }

        public string WaterMarkValue
        {
            get
            {
                return _waterMarkValue;
            }
            set
            {
                if (_waterMarkValue.ToUpper() != value.ToUpper())
                {
                    _waterMarkValue = value;
                    RaisePropertyChanged("WaterMarkValue");
                }
            }
        }

        #endregion

        #region Private methods

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OnDeleteLabelCommandExecuted(object parameter)
        {
            SelectedLabelsSource.Remove(parameter);
        }

        #endregion
    }
}
