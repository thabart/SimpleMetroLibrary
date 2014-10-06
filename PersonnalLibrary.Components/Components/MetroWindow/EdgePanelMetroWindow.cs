using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PersonnalLibrary.Components
{
    [TemplatePart(Name = "PART_ContentEdgePanel", Type = typeof(Grid))]
    public class EdgePanelMetroWindow : ContentControl
    {
        #region Fields

        private Grid _contentEdgePanel;

        #endregion

        #region Constructor

        static EdgePanelMetroWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EdgePanelMetroWindow), new FrameworkPropertyMetadata(typeof(EdgePanelMetroWindow)));
        }

        public EdgePanelMetroWindow()
        {
            this.DataContext = this;   
        }

        #endregion

        #region Properties

        public string Title { get; set; }

        public ICommand BackCommmand { get; set; }

        #endregion

        #region Public methods

        public override void OnApplyTemplate()
        {
            var template = this.Template;
            _contentEdgePanel = this.FindName("PART_ContentEdgePanel") as Grid;

 	        base.OnApplyTemplate();
        }

        public void Initialize()
        {

        }

        #endregion
    }
}
