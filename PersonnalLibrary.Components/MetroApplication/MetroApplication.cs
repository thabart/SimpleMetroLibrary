using PersonnalLibrary.Components;
using System;
using System.Windows;

namespace PersonnalLibrary
{
    public static class MetroApplication
    {
        public static void ShowWindow(string path)
        {
            var uri = new Uri(path, UriKind.Relative);
            var component = (FrameworkElement)Application.LoadComponent(uri);
            var window = new MetroWindow();
            window.Content = component;
            window.Show();
        }
    }
}
