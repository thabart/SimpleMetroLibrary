using PersonnalLibrary.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PersonnalLibrary.Pivot.Navigation
{
    public class PivotController : INotifyPropertyChanged
    {
        #region Fields

        private List<PivotItem> _pivotItems;

        private PivotItem _selectedPivotItem;

        private int _currentIndex;

        #endregion

        #region Constructor

        public PivotController()
        {
            _pivotItems = new List<PivotItem>();
        }

        #endregion

        #region Properties

        public List<PivotItem> PivotItems
        {
            get
            {
                return _pivotItems;
            }
        }

        public PivotItem SelectedPivotItem
        {
            get
            {
                return _selectedPivotItem;
            }
            private set
            {
                if (_selectedPivotItem != value)
                {
                    _selectedPivotItem = value;
                    RaisePropertyChanged("SelectedPivotItem");
                }
            }
        }

        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                _currentIndex = value;
            }
        }

        #endregion

        #region events

        public event EventHandler<PivotIndexChangedEventArgs> PivotChangedEvent;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public methods

        public void NavigateToElement(object parameter)
        {
            var pivotItem = parameter as PivotItem;
            if (pivotItem == null)
            {
                return;
            }

            SelectedPivotItem = pivotItem;
            _currentIndex = PivotItems.IndexOf(pivotItem);
            if (PivotChangedEvent != null)
            {
                PivotChangedEvent(
                    this,
                    new PivotIndexChangedEventArgs() { PivotIndex = _currentIndex });
            }
        }

        public void NavigateToIndice(int indice)
        {
            if (PivotItems.Count < indice)
            {
                return;
            }

            SelectedPivotItem = PivotItems[indice];
            NavigateToElement(SelectedPivotItem);
        }

        #endregion

        #region private methods

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
