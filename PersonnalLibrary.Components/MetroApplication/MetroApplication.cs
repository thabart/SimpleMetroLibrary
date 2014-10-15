using PersonnalLibrary.Components;
using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PersonnalLibrary
{
    public static class MetroApplication
    {
        public static void ShowWindow(string path)
        {
            var fonts = Fonts.GetFontFamilies(new Uri("pack://application:,,,/PersonnalLibrary;component/Fonts/#"));

            var uri = new Uri(path, UriKind.Relative);
            var component = (FrameworkElement)Application.LoadComponent(uri);
            var window = new MetroWindow();
            window.Content = component;
            window.Show();
        }

        public static void DisplayMessageBox()
        {

            Action<object, EventArgs, Popup> placementTargetChanged = (sender, e, p) =>
            {
                var offset = p.HorizontalOffset;
                p.HorizontalOffset = offset + 1;
                p.HorizontalOffset = offset;
            };

            var popup = new Popup();
            popup.Child = new MetroMessageBox();
            popup.IsOpen = true;

            popup.PlacementTarget = Application.Current.MainWindow;
            popup.Placement = PlacementMode.Center;

            var placementTarget = popup.PlacementTarget;
            var window = Window.GetWindow(placementTarget);
            if (window != null)
            {
                window.LocationChanged += (s, e) => placementTargetChanged(s, e, popup);
                window.SizeChanged += (s, e) => placementTargetChanged(s, e, popup);
            }
        }
    }
}
