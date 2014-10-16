using PersonnalLibrary.Common;
using PersonnalLibrary.Navigation;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PersonnalLibrary.Components
{
    [TemplatePart(Name = "PART_MoveRectangle", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_SplitWindow", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_SplitWindowContent", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_ResizableGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_ContentControlMoveRectangle", Type = typeof(ContentControl))]
    [TemplatePart(Name = "PART_AlertWindow", Type = typeof(Grid))]
    public class MetroWindow : Window
    {
        #region Fields

        private Rectangle _draggableRectangle;

        private Grid _splitWindow;

        private Grid _splitWindowContent;

        private TranslateTransform _splitWindowTranslateTransform;

        private bool _isEdgePanelOpen;

        private FrameworkElement _loadedEdgePanel;

        private Grid _alterWindow;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        private HwndSource _hwndSource;
        
        #endregion

        #region Properties

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        #endregion

        #region Constructor

        static MetroWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroWindow), new FrameworkPropertyMetadata(typeof(MetroWindow)));
        }

        public MetroWindow()
        {
            this.DataContext = this;

            _isEdgePanelOpen = false;

            PreviewMouseMove += OnWindowPreviewMouseMove;

            RegisterCommands();
        }

        #endregion

        #region Public methods

        public void DisplayViewInSplitPanel(string url)
        {
            DisplayViewInSplitPanel(url, null);
        }

        public void DisplayViewInSplitPanel(string url, NavigationParameters parameter)
        {
            Action operation = () => 
            {
                var uri = new Uri(url, UriKind.Relative);
                _loadedEdgePanel = (FrameworkElement)Application.LoadComponent(uri);

                _loadedEdgePanel.Loaded += (sender, evt) => SplitterPanelLoaded(sender, evt, parameter);
                _splitWindowContent.Children.Clear();
                var contains = _splitWindowContent.Children.Contains(_loadedEdgePanel);
                _splitWindowContent.Children.Add(_loadedEdgePanel);

                // Force the metro window to calculate the size of the user control.
                _loadedEdgePanel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                _splitWindowContent.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                _splitWindow.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                DisplayEdgePanel();
            };

            if (_isEdgePanelOpen)
            {
                HideEdgePanel(operation);
            }
            else
            {
                operation();
            }
        }

        public void DisplayPopup()
        {
            _alterWindow.Visibility = Visibility.Visible;
        }

        public void DisplayMetroWindow(string url)
        {

        }

        public void DisplayMetroWindow(string url, NavigationParameters parameter)
        {
            
        }

        public override void OnApplyTemplate()
        {
            _draggableRectangle = (Rectangle)this.Template.FindName("PART_MoveRectangle", this);
            _splitWindow = (Grid)this.Template.FindName("PART_SplitWindow", this);
            _splitWindowContent = (Grid)this.Template.FindName("PART_SplitWindowContent", this);
            _splitWindowTranslateTransform = (TranslateTransform)this.Template.FindName("SplitWindowTranslateTransform", this);
            _alterWindow = (Grid)this.Template.FindName("PART_AlertWindow", this);

            var resizeGrid = (Grid)this.Template.FindName("PART_ResizableGrid", this);
            foreach (var element in resizeGrid.Children)
            {
                var resizeRectangle = element as Rectangle;
                if (resizeRectangle != null)
                {
                    resizeRectangle.PreviewMouseDown += ResizeRectanglePreviewMouseDown;
                    resizeRectangle.MouseMove += ResizeRectangleMouseMove;
                }
            }

            var contentControl = (ContentControl)this.Template.FindName("PART_ContentControlMoveRectangle", this);
            contentControl.MouseDoubleClick += (s, e) => OnMaximizeCommandExecuted(null);

            var settingsButton = (Button)this.Template.FindName("PART_SettingsButton", this);
            var maximizeButton = (ToggleButton)this.Template.FindName("PART_MaximizeButton", this);
            var closeButton = (Button)this.Template.FindName("PART_CloseButton", this);

            settingsButton.PreviewMouseMove += OnHoverButton;
            maximizeButton.PreviewMouseMove += OnHoverButton;
            closeButton.PreviewMouseMove += OnHoverButton;

            _draggableRectangle.MouseDown += MoveWindow;
            MouseDown += MetroWindowMouseDown;
            _splitWindow.MouseDown += SplitWindowMouseDown;

            base.OnApplyTemplate();
        }

        #endregion

        #region Protected methods

        protected override void OnInitialized(EventArgs e)
        {
            SourceInitialized += OnSourceInitialized;
            base.OnInitialized(e);
        }

        #endregion

        #region Private enumeration

        private enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        #endregion

        #region Private methods

        private void OnHoverButton(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void OnSourceInitialized(object sender, EventArgs e)
        {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }

        private void OnWindowPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                Cursor = Cursors.Arrow;
            }
        }

        private void ResizeRectanglePreviewMouseDown(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle != null)
            {
                switch (rectangle.Name)
                {
                    case "top":
                        Cursor = Cursors.SizeNS;
                        ResizeWindow(ResizeDirection.Top);
                        break;
                    case "bottom":
                        Cursor = Cursors.SizeNS;
                        ResizeWindow(ResizeDirection.Bottom);
                        break;
                    case "left":
                        Cursor = Cursors.SizeWE;
                        ResizeWindow(ResizeDirection.Left);
                        break;
                    case "right":
                        Cursor = Cursors.SizeWE;
                        ResizeWindow(ResizeDirection.Right);
                        break;
                    case "topLeft":
                        Cursor = Cursors.SizeNWSE;
                        ResizeWindow(ResizeDirection.TopLeft);
                        break;
                    case "topRight":
                        Cursor = Cursors.SizeNESW;
                        ResizeWindow(ResizeDirection.TopRight);
                        break;
                    case "bottomLeft":
                        Cursor = Cursors.SizeNESW;
                        ResizeWindow(ResizeDirection.BottomLeft);
                        break;
                    case "bottomRight":
                        Cursor = Cursors.SizeNWSE;
                        ResizeWindow(ResizeDirection.BottomRight);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ResizeRectangleMouseMove(object sender, MouseEventArgs e)
        {
            var rectangle = sender as Rectangle;
            if (rectangle != null)
            {
                switch (rectangle.Name)
                {
                    case "top":
                    case "bottom":
                        Cursor = Cursors.SizeNS;
                        break;
                    case "left":
                    case "right":
                        Cursor = Cursors.SizeWE;
                        break;
                    case "topLeft":
                    case "bottomRight":
                        Cursor = Cursors.SizeNWSE;
                        break;
                    case "topRight":
                    case "bottomLeft":
                        Cursor = Cursors.SizeNESW;
                        break;
                    default:
                        break;
                }
            }
        }

        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_hwndSource.Handle, 0x112, (IntPtr)(61440 + direction), IntPtr.Zero);
        }

        private void MetroWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_isEdgePanelOpen)
            {
                HideEdgePanel();
            }
        }

        private void SplitWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        
        private void SplitterPanelLoaded(object sender, RoutedEventArgs e, NavigationParameters parameter)
        {
            var userControl = (FrameworkElement)sender;
            var dataContext = userControl.DataContext as INavigationAware;

            _splitWindow.Visibility = Visibility.Visible;
            if (dataContext != null)
            {
                dataContext.OnNavigatedFrom(parameter);
            }
        }

        private void DisplayEdgePanel(Action callback = null)
        {
            AnimateEdgePanel(true, callback);
            _isEdgePanelOpen = true;
        }

        private void HideEdgePanel(Action callback = null)
        {
            AnimateEdgePanel(false, callback);
            _isEdgePanelOpen = false;
        }

        private void AnimateEdgePanel(bool display, Action callback = null)
        {
            var width = _loadedEdgePanel.ActualWidth == 0 ? _loadedEdgePanel.DesiredSize.Width : _loadedEdgePanel.ActualWidth;
            if (display)
            {
                _splitWindowTranslateTransform.X = width;
            }

            var story = new Storyboard();
            var toValue = display ? 0 : width;

            DoubleAnimation animation = new DoubleAnimation()
            {
                To = toValue,
                Duration = new Duration(new TimeSpan(0, 0, 1)),
                BeginTime = new TimeSpan(0, 0, 0),
                EasingFunction = new CubicEase()
                {
                    EasingMode = EasingMode.EaseOut
                }
            };

            Storyboard.SetTarget(animation, _splitWindow);
            Storyboard.SetTargetProperty(animation, new PropertyPath("RenderTransform.X"));
            story.Children.Add(animation);
            if (callback != null)
            {
                story.Completed += (sender, e) =>
                {
                    callback();
                };
            }

            story.Begin();
        }

        private void RegisterCommands()
        {
            MaximizeCommand = new DelegateCommand(OnMaximizeCommandExecuted);
            CloseCommand = new DelegateCommand(OnCloseCommandExecuted);
        }
        
        private void OnMaximizeCommandExecuted(object parameter)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void OnCloseCommandExecuted(object parameter)
        {
            this.Close();
        }
        
        private void MoveWindow(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        #endregion
    }
}
