using PersonnalLibrary.Common;
using PersonnalLibrary.Components;
using PersonnalLibrary.Components.Extensions;
using PersonnalLibrary.Pivot.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PersonnalLibrary.Pivot.ViewModels
{
    public class PivotContainerViewModel
    {
        #region Fields

        private PivotController _pivotController;

        #endregion

        #region Properties

        public ObservableCollection<MenuElement> NavigationMenuElements { get; set; }

        public PivotController PivotController
        {
            get
            {
                return _pivotController;
            }
        }

        public List<KeyBinding> ShortCuts { get; set;}

        #endregion

        #region Constructor

        public PivotContainerViewModel()
        {
            NavigationMenuElements = new ObservableCollection<MenuElement>();
            ShortCuts = new List<KeyBinding>();
            _pivotController = new PivotController();
        }

        #endregion

        #region Public methods

        public void OnLoad()
        {
            if (_pivotController.PivotItems != null && !_pivotController.PivotItems.Any())
            {
                return;
            }

            RefreshNavigationMenu();
            RefreshSelectedElement(GetCurrentItem());
            RefreshNavigationShortCuts();
        }

        public PivotItem GetCurrentItem()
        {
            return _pivotController.SelectedPivotItem;
        }

        #endregion

        #region Private methods

        private void RefreshNavigationMenu()
        {
            NavigationMenuElements.Clear();
            NavigationMenuElements.AddRange
                (_pivotController.PivotItems.Select(p => new MenuElement()
                {
                    Title = p.Title,
                    NavigateCmd = new DelegateCommand(CanExecuteNavigation, ExecuteNavigation),
                    PivotElement = p,
                    IsCurrent = false
                }).ToList());
        }

        private bool CanExecuteNavigation(object parameter)
        {
            return true;
        }

        private void ExecuteNavigation(object parameter)
        {
            var pivotItem = parameter as PivotItem;
            if (pivotItem == null)
            {
                return;
            }

            _pivotController.NavigateToElement(pivotItem);
            RefreshSelectedElement(pivotItem);
        }

        private void RefreshSelectedElement(PivotItem element)
        {
            var currentIndex = _pivotController.CurrentIndex;
            int index = 0;
            NavigationMenuElements.ToList().ForEach((menuElement) =>
            {
                if (currentIndex == index)
                {
                    menuElement.IsCurrent = true;
                    menuElement.PivotElement.IsEnabled = true;
                }
                else
                {
                    menuElement.IsCurrent = false;
                    menuElement.PivotElement.IsEnabled = false;
                }

                index++;
            });
        }

        private void RefreshNavigationShortCuts()
        {
            NavigationMenuElements.ToList().ForEach((element) =>
             {
                 if (element.PivotElement.NavigationShortCut == null)
                 {
                     return;
                 }

                 ShortCuts.Add(new KeyBinding()
                 {
                     Key = element.PivotElement.NavigationShortCut,
                     Modifiers = ModifierKeys.Alt,
                     Command = element.NavigateCmd,
                     CommandParameter = element.PivotElement
                 });
             });
        }

        #endregion
    }
}
