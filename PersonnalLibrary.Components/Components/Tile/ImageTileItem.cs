using System.Windows;
using System.Windows.Media;

namespace PersonnalLibrary.Components
{
    public class ImageTileItem : BaseTileItem
    {
        #region Properties

        public ImageSource Source
        {
            get
            {
                return (ImageSource)this.GetValue(SourceProperty);
            }
            set
            {
                this.SetValue(SourceProperty, value);
            }
        }

        #endregion

        #region Dependency properties

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source",
            typeof(ImageSource),
            typeof(ImageTileItem),
            null);

        #endregion

        static ImageTileItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageTileItem), new FrameworkPropertyMetadata(typeof(ImageTileItem)));
        }
    }
}
