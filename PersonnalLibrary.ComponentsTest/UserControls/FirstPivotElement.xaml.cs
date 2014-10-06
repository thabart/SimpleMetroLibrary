using PersonnalLibrary.Components;
using PersonnalLibrary.Navigation;
using PersonnalLibrary.Pivot.Navigation;

namespace PersonnalLibrary.Tests.UserControls
{
    /// <summary>
    /// Interaction logic for FirstPivotElement.xaml
    /// </summary>
    public partial class FirstPivotElement : PivotItem, IPivotNavigationAware
    {
        private PivotController _pivotController;

        public FirstPivotElement()
        {
            InitializeComponent();
        }

        public void OnPivotNavigatedTo(PivotNavigationContext context)
        {
            _pivotController = context.PivotController;
        }

        public void OnPivotNavigatedFrom()
        {
        }
        
        private void OpenEdgePanel(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainWindow = App.Current.MainWindow as MetroWindow;
            if (mainWindow != null)
            {
                mainWindow.DisplayViewInSplitPanel("/PersonnalLibrary.Tests;component/UserControls/EdgePanel.xaml", null);
            }
        }

        private void OpenNewWindow(object sender, System.Windows.RoutedEventArgs e)
        {
            MetroApplication.ShowWindow("/PersonnalLibrary.Tests;component/UserControls/Tiles.xaml");
        }
    }
}
