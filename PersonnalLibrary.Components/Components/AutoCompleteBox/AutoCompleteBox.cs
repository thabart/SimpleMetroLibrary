using PersonnalLibrary.Components.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using PersonnalLibrary.Components.Extensions;

namespace PersonnalLibrary.Components
{

    [TemplatePart(Name = "PART_ContentForLabels", Type = typeof(ItemsControl))]
    [TemplatePart(Name = "PART_ListItemsPopup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_WaterMark", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_ListItemsPopup", Type = typeof(Popup))]
    [TemplatePart(Name = "PART_LabelsList", Type = typeof(ListBox))]
    public class AutoCompleteBox : TextBox
    {
        #region Fields

        private ItemsControl _contentForLabel;

        private AutoCompleteBoxViewModel _viewModel;

        private TextBlock _waterMarkTextBlock;

        private ScrollViewer _contentHost;

        private Popup _popup;

        private ListBox _labelsList;

        #endregion

        #region Constructor

        static AutoCompleteBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoCompleteBox), new FrameworkPropertyMetadata(typeof(AutoCompleteBox)));
        }

        public AutoCompleteBox()
        {
            _viewModel = new AutoCompleteBoxViewModel();

            this.DataContext = _viewModel;

            this.KeyUp += OnAutoCompleteBoxKeyUp;
        }

        #endregion

        #region Properties

        [Bindable(true)]
        public IEnumerable<object> ItemsSource
        {
            get
            {
                return _viewModel.ItemsSource;
            }
            set
            {
                _viewModel.ItemsSource = value;
            }
        }

        #endregion
        
        #region Public methods

        public override void OnApplyTemplate()
        {
            _contentForLabel = (ItemsControl)this.Template.FindName("PART_ContentForLabels", this);
            _waterMarkTextBlock = (TextBlock)this.Template.FindName("PART_WaterMark", this);
            _contentHost = (ScrollViewer)this.Template.FindName("PART_ContentHost", this);
            _popup = (Popup)this.Template.FindName("PART_ListItemsPopup", this);
            _labelsList = (ListBox)this.Template.FindName("PART_LabelsList", this);

            var placementTarget = _popup.PlacementTarget;
            var window = Window.GetWindow(placementTarget);
            if (window != null)
            {
                window.LocationChanged += PlacementTargetChanged;
                window.SizeChanged += PlacementTargetChanged;
            }

            base.OnApplyTemplate();
        }

        #endregion

        #region Protected methods

        #endregion

        #region Private methods

        private void OnAutoCompleteBoxKeyUp(object sender, KeyEventArgs e)
        {
            string searchText = this.Text;
            var lstRecords = from record in _viewModel.ItemsSource
                         where record.ToString().ToUpper().StartsWith(searchText.ToUpper()) && 
                         !_viewModel.SelectedLabelsSource.Any(sl => sl != null && sl.ToString().ToUpper().Equals(record.ToString().ToUpper()))
                         select record;

            if (lstRecords.Any())
            {
                _popup.IsOpen = true;
                _viewModel.LabelsSource.Clear();
                _viewModel.LabelsSource.AddRange(lstRecords.ToList());
            }
        }

        private void PlacementTargetChanged(object sender, System.EventArgs e)
        {
            var offset = _popup.HorizontalOffset;
            _popup.HorizontalOffset = offset + 1;
            _popup.HorizontalOffset = offset;
        }

        #endregion
    }
}
