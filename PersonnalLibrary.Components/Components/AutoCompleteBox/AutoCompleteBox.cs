using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PersonnalLibrary.Components
{

    [TemplatePart(Name = "PART_ContentForLabels", Type = typeof(StackPanel))]
    [TemplatePart(Name = "PART_ListItemsPopup", Type = typeof(Popup))]
    public class AutoCompleteBox : TextBox
    {
        #region Fields


        #endregion

        #region Constructor

        static AutoCompleteBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoCompleteBox), new FrameworkPropertyMetadata(typeof(AutoCompleteBox)));
        }

        public AutoCompleteBox()
        {

        }

        #endregion

        #region Properties

        public List<object> ItemsSource
        {
            get
            {
                return (List<object>)this.GetValue(ItemsSourceProperty);
            }
            set
            {
                this.SetValue(ItemsSourceProperty, value);
            }
        }

        #endregion

        #region Dependency properties

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(List<object>),
            typeof(AutoCompleteBox),
            new PropertyMetadata(new List<object>()));

        #endregion

        #region Public methods

        public override void OnApplyTemplate()
        {
            
            base.OnApplyTemplate();
        }

        #endregion

        #region Protected methods

        #endregion

        #region Private methods

        #endregion

    }
}
