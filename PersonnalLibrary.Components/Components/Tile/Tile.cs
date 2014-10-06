using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace PersonnalLibrary.Components
{
    [ContentProperty("TileDescription")]
    [TemplatePart(Name = "PART_GlobalGridContainer", Type=typeof(Grid))]
    [TemplatePart(Name = "PART_BorderDisplayedOnHover", Type=typeof(Border))]
    [TemplatePart(Name = "PART_BorderDisplayedWhenSelected", Type=typeof(Border))]
    [TemplatePart(Name = "PART_DefaultBorder", Type=typeof(Border))]
    [TemplatePart(Name = "PART_GlobalContainer", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_TilesItemsControl", Type = typeof(ItemsControl))]
    [TemplatePart(Name = "PART_TilesPresenter", Type = typeof(ItemsPresenter))]
    [TemplatePart(Name = "PART_TilesContainer", Type = typeof(StackPanel))]
    [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Clicked", GroupName = "CommonStates")]
    public class Tile : ButtonBase
    {
        #region Fields

        private StackPanel _myStackPanelContainer;

        private Grid _myGridContainer;

        private Dictionary<TileAnimationDirection, Action> _listActions;

        private AnimateAction _animateAction;

        #endregion

        #region Properties

        public TileAnimationDirection AnimationDirection
        {
            get
            {
                return (TileAnimationDirection)this.GetValue(AnimationDirectionProperty);
            }
            set
            {
                this.SetValue(AnimationDirectionProperty, value);
            }
        }

        public List<BaseTileItem> TileItems
        {
            get
            {
                return (List<BaseTileItem>)GetValue(TileItemsProperty);
            }
            set
            {
                SetValue(TileItemsProperty, value);
            }
        }

        public string TileDescription
        {
            get
            {
                return (string)this.GetValue(TileDescriptionProperty);
            }
            set
            {
                this.SetValue(TileDescriptionProperty, value);
            }
        }

        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        #endregion

        #region Dependency properties

        public static readonly DependencyProperty AnimationDirectionProperty = DependencyProperty.Register(
            "AnimationDirection",
            typeof(TileAnimationDirection),
            typeof(Tile),
            new PropertyMetadata(TileAnimationDirection.TopDown));

        public static readonly DependencyProperty TileItemsProperty = DependencyProperty.Register(
            "TileItems",
            typeof(List<BaseTileItem>),
            typeof(Tile),
            new PropertyMetadata(new List<BaseTileItem>()));

        public static readonly DependencyProperty TileDescriptionProperty = DependencyProperty.Register(
            "TileDescription",
            typeof(string),
            typeof(Tile),
            null);

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected",
            typeof(bool),
            typeof(Tile),
            new PropertyMetadata(false));

        #endregion

        #region Constructor

        static Tile()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Tile), new FrameworkPropertyMetadata(typeof(Tile)));
        }

        public Tile()
        {
            SetValue(TileItemsProperty, new List<BaseTileItem>());
        }

        #endregion

        #region Private methods

        private void SetVertical()
        {
            _myGridContainer.Height = TileItems.Count * Height;
        }

        private void SetHorizontal()
        {
            _myGridContainer.Width = TileItems.Count * Width;
            _myStackPanelContainer.Width = TileItems.Count * Width;
            _myStackPanelContainer.Orientation = Orientation.Horizontal;
        }

        private void InvertTheOrderOfTileItems()
        {
            var newOrder = new List<BaseTileItem>();
            for (var indice = TileItems.Count - 1; indice >= 0; indice--)
            {
                newOrder.Add(TileItems[indice]);
            }

            TileItems = newOrder;
        }

        private void AnimateChildTopToDown()
        {
            SetVertical();
            InvertTheOrderOfTileItems();
            _animateAction.InitPosition(_myStackPanelContainer, -(TileItems.Count - 1) * Height, 0);
            _animateAction.InitAnimation(_myStackPanelContainer, TileItems.Count, Height, "RenderTransform.Y", true);
        }

        private void AnimateChildBottomTop()
        {
            SetVertical();
            _animateAction.InitPosition(_myStackPanelContainer, 0, 0);
            _animateAction.InitAnimation(_myStackPanelContainer, TileItems.Count, -Height, "RenderTransform.Y", true);
        }

        private void AnimateChildLeftRight()
        {
            SetHorizontal();
            InvertTheOrderOfTileItems();
            _animateAction.InitPosition(_myStackPanelContainer, 0, (TileItems.Count - 1) * -Width);
            _animateAction.InitAnimation(_myStackPanelContainer, TileItems.Count, Width, "RenderTransform.X", false);
        }

        private void AnimateChildRightLeft()
        {
            SetHorizontal();
            _animateAction.InitPosition(_myStackPanelContainer, 0, 0);
            _animateAction.InitAnimation(_myStackPanelContainer, TileItems.Count, -Width, "RenderTransform.X", false);
        }

        #endregion

        #region Protected methods

        protected override void OnMouseRightButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            IsSelected = true;
            base.OnMouseRightButtonDown(e);
        }

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            IsSelected = false;
            VisualStateManager.GoToState(this, "Clicked", false);
            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            IsSelected = false;
            VisualStateManager.GoToState(this, "Normal", false);
            base.OnMouseLeftButtonUp(e);
        }

        #endregion

        #region Public methods

        public override void OnApplyTemplate()
        {
            _myGridContainer = base.Template.FindName("PART_GlobalContainer", this) as Grid;
            var itemsControl = base.Template.FindName("PART_TilesItemsControl", this) as ItemsControl;

            // TODO : pb with the search of elements in the custom control. 
            _animateAction = new AnimateAction();
            _listActions = new Dictionary<TileAnimationDirection, Action>()
            {
                {
                    TileAnimationDirection.TopDown, AnimateChildTopToDown
                },
                {
                    TileAnimationDirection.BottomUp, AnimateChildBottomTop
                }, 
                {
                    TileAnimationDirection.LeftRight, AnimateChildLeftRight
                },
                {
                    TileAnimationDirection.RightLeft, AnimateChildRightLeft
                }
            };

            base.OnApplyTemplate();

            // Apply the templates and retrieve the needed part.
            itemsControl.ApplyTemplate();
            var presenter = itemsControl.Template.FindName("PART_TilesPresenter", itemsControl) as ItemsPresenter;
            presenter.ApplyTemplate();
            _myStackPanelContainer = itemsControl.ItemsPanel.FindName("PART_TilesContainer", presenter) as StackPanel;

            _listActions[AnimationDirection]();
        }

        #endregion
    }
}
