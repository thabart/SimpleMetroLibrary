using PersonnalLibrary.Components.Extensions;
using PersonnalLibrary.Components.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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

        private string _previousTextValue;

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

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;

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

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate) this.GetValue(ItemTemplateProperty); }
            set { this.SetValue(ItemTemplateProperty, value); }
        }

        #endregion

        #region Dependency properties

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate",
            typeof (DataTemplate),
            typeof (AutoCompleteBox),
            new PropertyMetadata());

        #endregion

        #region Public methods

        public override void OnApplyTemplate()
        {
            _contentForLabel = (ItemsControl)this.Template.FindName("PART_ContentForLabels", this);
            _waterMarkTextBlock = (TextBlock)this.Template.FindName("PART_WaterMark", this);
            // _contentHost = (ScrollViewer)this.Template.FindName("PART_ContentHost", this);
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
            var searchText = this.Text;
            var lstRecords = from record in _viewModel.ItemsSource
                             where record.ToString().ToUpper().StartsWith(searchText.ToUpper()) &&
                             !_viewModel.SelectedLabelsSource.Any(sl => sl != null && sl.ToString().ToUpper().Equals(record.ToString().ToUpper()))
                             select record;

            var deleteLatestLabel = string.IsNullOrEmpty(searchText) && e.Key == Key.Back &&
                                      _viewModel.SelectedLabelsSource.Any() && string.IsNullOrEmpty(_previousTextValue);
            var addNewLabel = lstRecords.Any() && !string.IsNullOrEmpty(searchText);
            var automaticallyInsertWord = lstRecords.Count() == 1 && e.Key == Key.Tab;

            _previousTextValue = searchText;

            ResetPopup();
            
            if (automaticallyInsertWord)
            {
                _viewModel.SelectedLabelsSource.Add(lstRecords.First());
                
                ResetTextBox();

                return;
            }

            if (deleteLatestLabel)
            {
                _viewModel.SelectedLabelsSource.Remove(_viewModel.SelectedLabelsSource.Last());
                return;
            }

            if (addNewLabel)
            {
                _popup.PopupAnimation = PopupAnimation.Slide;
                _popup.IsOpen = true;
                _viewModel.LabelsSource.AddRange(lstRecords.ToList());
                return;
            }
        }

        private void PlacementTargetChanged(object sender, System.EventArgs e)
        {
            var offset = _popup.HorizontalOffset;
            _popup.HorizontalOffset = offset + 1;
            _popup.HorizontalOffset = offset;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName.Equals("SelectedItem"))
            {
                ResetTextBox();

                ResetPopup();
            }
        }

        private void ResetTextBox()
        {
            this.Text = string.Empty;
            this.Focus();
        }

        private void ResetPopup()
        {
            _viewModel.LabelsSource.Clear();
            _popup.IsOpen = false;
        }

        #endregion
    }
}
