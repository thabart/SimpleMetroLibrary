using PersonnalLibrary.Navigation;
using PersonnalLibrary.Pivot.Navigation;
using PersonnalLibrary.Pivot.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PersonnalLibrary.Components
{
    [TemplatePart(Name = "PART_GlobalContainer", Type=typeof(Grid))]
    [TemplatePart(Name = "PART_NavigationBar", Type=typeof(ItemsControl))]
    [TemplatePart(Name = "PART_ElementsContainer", Type = typeof(ItemsControl))]
    [TemplatePart(Name = "PART_FooterContainer", Type = typeof(ItemsControl))]
    [TemplateVisualState(Name = "Inactive", GroupName = "Pivot")]
    [TemplateVisualState(Name = "Active", GroupName = "Pivot")]
    public class PivotContainer : ContentControl
    {
        #region Fields

        private Grid _gridContainer;

        private PivotContainerViewModel _pivotContainerViewModel;

        private ItemsControl _navigationBar;

        private ItemsControl _elementsContainer;

        private ItemsControl _footerContainer;

        private int _currentActiveIndex;

        #endregion

        #region Properties

        public List<PivotItem> PivotItems
        {
            get
            {
                return _pivotContainerViewModel.PivotController.PivotItems;
            }
        }

        public int SelectedIndex
        {
            get
            {
                return _pivotContainerViewModel.PivotController.CurrentIndex;
            }
            set
            {
                _pivotContainerViewModel.PivotController.CurrentIndex = value;
            }
        }

        #endregion

        #region Dependency properties


        #endregion

        #region Constructor

        static PivotContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PivotContainer), new FrameworkPropertyMetadata(typeof(PivotContainer)));
        }

        public PivotContainer()
        {
            _currentActiveIndex = -1;
            _pivotContainerViewModel = new PivotContainerViewModel();

            this.DataContext = _pivotContainerViewModel;

            this.Loaded += PivotContainerS_Loaded;
        }
        
        #endregion

        #region Public methods

        public override void OnApplyTemplate()
        {
            _gridContainer = Template.FindName("PART_GlobalContainer", this) as Grid;
            _navigationBar = Template.FindName("PART_NavigationBar", this) as ItemsControl;
            _elementsContainer = Template.FindName("PART_ElementsContainer", this) as ItemsControl;
            _footerContainer = Template.FindName("PART_FooterContainer", this) as ItemsControl;
            
            // The context buttons must have the same view model of their content pivot.
            PivotItems.ForEach((pivot) =>
            {
                var context = pivot.DataContext;
                pivot.ContextButtons.ForEach((contextButton) =>
                {
                    contextButton.DataContext = context;
                });
            });

            this.Loaded += PivotContainerS_Loaded;

            base.OnApplyTemplate();
        }
        
        #endregion

        #region Private methods

        private void PivotContainerS_Loaded(object sender, RoutedEventArgs e)
        {
            _pivotContainerViewModel.OnLoad();
            _pivotContainerViewModel.PivotController.PivotChangedEvent += NavigateToGivenPivotGuidOnChanged;
            
            UpdateLayout();

            // Update the size for all children in the navigation bar.
            for (int i = 0; i < _pivotContainerViewModel.PivotController.PivotItems.Count; i++)
            {
                var titleChild = (ContentPresenter)_navigationBar.ItemContainerGenerator.ContainerFromIndex(i);
                titleChild.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }

            // Update the size of the navigation bar.
            _navigationBar.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            _pivotContainerViewModel.PivotController.NavigateToIndice(_pivotContainerViewModel.PivotController.CurrentIndex);

            // TODO : replace this workaround : assign the keybindings to the window object.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                var window = Application.Current.MainWindow;
                var shortCuts = this._pivotContainerViewModel.ShortCuts;
                window.InputBindings.AddRange(this._pivotContainerViewModel.ShortCuts);
            }
        }

        private void NavigateToGivenPivotGuidOnChanged(object sender, PivotIndexChangedEventArgs args)
        {
            var pivotIndex = args.PivotIndex;
            NavigateTo(pivotIndex);
        }

        private void NavigateTo(int pivotIndex)
        {
            var size = this.DesiredSize;
            var parent = (Grid)this.Parent;
            var weight = parent.ActualWidth;

            if (_navigationBar.Items.Count - 1 < pivotIndex || _currentActiveIndex == pivotIndex)
            {
                return;
            }

            VisualStateManager.GoToState(this, "Inactive", false);

            var pivotItem = this._pivotContainerViewModel.PivotController.PivotItems[pivotIndex];
            var titleChild = (ContentPresenter)_navigationBar.ItemContainerGenerator.ContainerFromIndex(pivotIndex);

            Point relativePoint = pivotItem.TransformToAncestor(_elementsContainer).Transform(new Point(0, 0));
            Point titleRelativePoint = titleChild.TransformToAncestor(_navigationBar).Transform(new Point(0, 0));
            var story = new Storyboard();
            var titleTranslateX = -titleRelativePoint.X + 20;
            var translateX = -relativePoint.X + 20;
            // TODO : recalculate translateX.

            DoubleAnimation animation = new DoubleAnimation()
            {
                To = translateX,
                Duration = new Duration(new TimeSpan(0, 0, 1)),
                BeginTime = new TimeSpan(0, 0, 0),
                EasingFunction = new CubicEase()
                {
                    EasingMode = EasingMode.EaseOut
                }
            };

            DoubleAnimation titleAnimation = new DoubleAnimation()
            {
                To = titleTranslateX,
                Duration = new Duration(new TimeSpan(0, 0, 1)),
                BeginTime = new TimeSpan(0, 0, 0),
                EasingFunction = new CubicEase()
                {
                    EasingMode = EasingMode.EaseOut
                }
            };

            Storyboard.SetTarget(animation, _elementsContainer);
            Storyboard.SetTarget(titleAnimation, _navigationBar);
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.X"));
            Storyboard.SetTargetProperty(titleAnimation, new PropertyPath("RenderTransform.X"));
            story.Children.Add(animation);
            story.Children.Add(titleAnimation);
            story.Completed += story_Completed;

            story.Begin();

            PivotNavigateToViewModel(pivotItem);
            
            _currentActiveIndex = pivotIndex;
        }

        private void PivotNavigateToViewModel(PivotItem pivotItem)
        {
            var viewModel = pivotItem.DataContext as IPivotNavigationAware;
            if (viewModel != null)
            {
                var parameter = new PivotNavigationContext();
                parameter.PivotController = _pivotContainerViewModel.PivotController;

                viewModel.OnPivotNavigatedTo(parameter);
            }
        }

        private void story_Completed(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(this, "Active", false);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var size = new Size(double.PositiveInfinity, double.PositiveInfinity);
            return base.MeasureOverride(size);
        }

        #endregion
    }
}
