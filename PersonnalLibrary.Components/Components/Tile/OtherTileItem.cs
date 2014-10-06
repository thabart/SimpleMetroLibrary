using System.Windows;

namespace PersonnalLibrary.Components
{
    public class OtherTileItem : BaseTileItem
    {
        #region Constructor

        static OtherTileItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OtherTileItem), new FrameworkPropertyMetadata(typeof(OtherTileItem)));
        }

        #endregion
    }
}
