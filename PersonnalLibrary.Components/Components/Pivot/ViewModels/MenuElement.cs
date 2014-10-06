using PersonnalLibrary.Components;
using System.ComponentModel;
using System.Windows.Input;

namespace PersonnalLibrary.Pivot.ViewModels
{
    public class MenuElement : INotifyPropertyChanged
    {
        #region Fields

        private bool _isCurrent;

        #endregion

        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public string Title { get; set; }

        public ICommand NavigateCmd { get; set; }

        public PivotItem PivotElement { get; set; }

        public bool IsCurrent
        {
            get
            {
                return _isCurrent;
            }
            set
            {
                if (_isCurrent != value)
                {
                    _isCurrent = value;
                    RaisePropertyChanged("IsCurrent");
                }
            }
        }

        #endregion

        #region Private fields

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
