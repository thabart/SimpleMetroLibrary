using System.Windows;
using System.Windows.Controls;

namespace PersonnalLibrary.Components
{
    public class MetroMessageBox : ContentControl
    {
        #region Constructor

        static MetroMessageBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroMessageBox), new FrameworkPropertyMetadata(typeof(MetroMessageBox)));
        }

        public MetroMessageBox()
        {
        }

        #endregion
    }
}
