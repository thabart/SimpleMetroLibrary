using System.Windows;
using System.Windows.Media;

namespace PersonnalLibrary.Components
{
    public class TextTileItem : BaseTileItem
    {
        #region Properties

        public string Title
        {
            get
            {
                return (string)this.GetValue(TitleProperty);
            }
            set
            {
                this.SetValue(TitleProperty, value);
            }
        }

        public Color Theme
        {
            get
            {
                return (Color)this.GetValue(ThemeProperty);
            }
            set
            {
                this.SetValue(ThemeProperty, value);
            }
        }

        public Thickness TextMargin
        {
            get
            {
                return (Thickness)this.GetValue(TextMarginProperty);
            }
            set
            {
                this.SetValue(TextMarginProperty, value);
            }
        }

        #endregion

        #region Dependency properties

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(string),
            typeof(TextTileItem),
            new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ThemeProperty = DependencyProperty.Register(
            "Theme",
            typeof(Color),
            typeof(TextTileItem),
            new PropertyMetadata(Color.FromRgb(200, 200, 200)));

        public static readonly DependencyProperty TextMarginProperty = DependencyProperty.Register(
            "TextMargin",
            typeof(Thickness),
            typeof(TextTileItem),
            new PropertyMetadata(new Thickness(6)));

        #endregion

        static TextTileItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextTileItem), new FrameworkPropertyMetadata(typeof(TextTileItem)));
        }
    }
}
