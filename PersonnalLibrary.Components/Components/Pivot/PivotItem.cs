using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace PersonnalLibrary.Components
{
    public class PivotItem : ContentControl
    {
        #region Properties

        public string Title { get; set; }

        public List<ButtonBase> ContextButtons { get; set; }

        public Key NavigationShortCut { get; set; }

        #endregion

        #region Constructor

        static PivotItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PivotItem), new FrameworkPropertyMetadata(typeof(PivotItem)));
        }

        public PivotItem()
        {
            ContextButtons = new List<ButtonBase>();
        }

        #endregion

        #region Protected methods

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (!IsEnabled)
            {
                e.Handled = true;
            }

            base.OnMouseDown(e);
        }

        #endregion
    }
}
