using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ProContentDialog.UI.Controls
{
    public class ProContentDialog : ContentControl
    {
        private Grid _root;
        private Grid _rootContent;
        public ProContentDialog()
        {
            this.DefaultStyleKey = typeof(ProContentDialog);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _root = this.GetTemplateChild("Root") as Grid;
            _rootContent = this.GetTemplateChild("RootContent") as Grid;

            UpdateBackground(this.Background);
            UpdateFullSizeDesired(this.FullSizeDesired);
            UpdateHorizontalContentAlignment(this.HorizontalContentAlignment);
            UpdateVerticalContentAlignment(this.VerticalContentAlignment);
            UpdateContentMargin(this.ContentMargin);
            UpdateContentPadding(this.ContentPadding);
            UpdateContentBackground(this.ContentBackground);
        }

        #region Private Methods

        private void UpdateVerticalContentAlignment(VerticalAlignment value)
        {
            if (_rootContent == null) return;

            Grid.SetRow(_rootContent, 0);
            Grid.SetRowSpan(_rootContent, 3);

            switch (value)
            {
                case VerticalAlignment.Stretch:break;
                case VerticalAlignment.Center: break;
                case VerticalAlignment.Top:
                    if (this.FullSizeDesired)
                    {
                        UpdateContentHeight(new GridLength(1, GridUnitType.Star));
                    }
                    else
                    {
                        UpdateContentHeight(new GridLength(0, GridUnitType.Auto));
                    }
                    break;
                case VerticalAlignment.Bottom:
                    if (this.FullSizeDesired)
                    {
                        UpdateContentHeight(new GridLength(1, GridUnitType.Star));
                    }
                    else
                    {
                        UpdateContentHeight(new GridLength(0, GridUnitType.Auto));
                        Grid.SetRow(_rootContent, 2);
                        Grid.SetRowSpan(_rootContent, 3);
                    }
                    break;
            }

            _rootContent.VerticalAlignment = value;
        }

        private void UpdateHorizontalContentAlignment(HorizontalAlignment value)
        {
            if (_rootContent == null) return;

            Grid.SetColumn(_rootContent, 0);
            Grid.SetColumnSpan(_rootContent, 3);

            switch (value)
            {
                case HorizontalAlignment.Stretch: break;
                case HorizontalAlignment.Center: break;
                case HorizontalAlignment.Left:
                    if (this.FullSizeDesired)
                    {
                        UpdateContentWidth(new GridLength(1, GridUnitType.Star));
                    }
                    else
                    {
                        UpdateContentWidth(new GridLength(0, GridUnitType.Auto));
                    }
                    break;
                case HorizontalAlignment.Right:
                    if (this.FullSizeDesired)
                    {
                        UpdateContentWidth(new GridLength(1, GridUnitType.Star));
                    }
                    else
                    {
                        UpdateContentWidth(new GridLength(0, GridUnitType.Auto));
                        Grid.SetColumn(_rootContent, 2);
                        Grid.SetColumnSpan(_rootContent, 3);
                    }
                    break;
            }

            _rootContent.HorizontalAlignment = value;
        }

        private void UpdateContentHeight(GridLength value)
        {
            if (_root == null || !_root.RowDefinitions.Any()) return;

            var row = _root.RowDefinitions.ElementAt(1);
            if (row == null) return;

            row.Height = value;
        }

        private void UpdateContentWidth(GridLength value)
        {
            if (_root == null || !_root.ColumnDefinitions.Any()) return;

            var column = _root.ColumnDefinitions.ElementAt(1);
            if (column == null) return;

            column.Width = value;
        }

        private void UpdateContentMargin(Thickness value)
        {
            if (_rootContent == null) return;
            _rootContent.Margin = value;
        }

        private void UpdateContentPadding(Thickness value)
        {
            if (_rootContent == null) return;
            _rootContent.Padding = value;
        }

        private void UpdateContentBackground(Brush value)
        {
            if (_rootContent == null) return;
            _rootContent.Background = value;
        }

        private void UpdateBackground(Brush value)
        {
            if (_root == null) return;
            _root.Background = value;
        }

        private void UpdateContentPanel(bool value)
        {
            if (_rootContent == null) return;
            if (value)
            {
                Grid.SetColumn(_rootContent, 0);
                Grid.SetRow(_rootContent, 0);
                Grid.SetColumnSpan(_rootContent, 3);
                Grid.SetRowSpan(_rootContent, 3);
            }
            else
            {
                Grid.SetColumn(_rootContent, 1);
                Grid.SetRow(_rootContent, 1);
                Grid.SetColumnSpan(_rootContent, 1);
                Grid.SetRowSpan(_rootContent, 1);
            }
        }

        private void UpdateFullSizeDesired(bool value)
        {
            if (_rootContent == null) return;
            if (value)
            {
                UpdateBackground(new SolidColorBrush(Color.FromArgb(100, 200, 200, 200)));
                UpdateContentWidth(new GridLength(1, GridUnitType.Star));
                UpdateContentHeight(new GridLength(1, GridUnitType.Star));
                UpdateContentPanel(true);
            }
            else
            {
                UpdateBackground(new SolidColorBrush(Color.FromArgb(100, 200, 200, 200)));
                UpdateContentWidth(new GridLength(0, GridUnitType.Auto));
                UpdateContentHeight(new GridLength(0, GridUnitType.Auto));
                UpdateContentPanel(false);
            }
        }
        #endregion

        public async Task ShowAsync()
        {
            await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                //TODO: Start Showing task;
            });
        }

        public void Hide()
        {
            //TODO: Abort Showing task;
        }

        #region Public Dependency Properties

        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(ProContentDialog), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(100, 200, 200, 200)), OnBackgroundChanged));

        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProContentDialog;
            if (control == null) return;

            control.UpdateBackground((Brush)e.NewValue);
        }

        public Brush ContentBackground
        {
            get { return (Brush)GetValue(ContentBackgroundProperty); }
            set { SetValue(ContentBackgroundProperty, value); }
        }

        public static readonly DependencyProperty ContentBackgroundProperty =
            DependencyProperty.Register("ContentBackground", typeof(Brush), typeof(ProContentDialog), new PropertyMetadata(new SolidColorBrush(Colors.LightGray), OnContentBackgroundChanged));

        private static void OnContentBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProContentDialog;
            if (control == null) return;

            control.UpdateContentBackground((Brush)e.NewValue);
        }

        public Thickness ContentPadding
        {
            get { return (Thickness)GetValue(ContentPaddingProperty); }
            set { SetValue(ContentPaddingProperty, value); }
        }

        public static readonly DependencyProperty ContentPaddingProperty =
            DependencyProperty.Register("ContentPadding", typeof(Thickness), typeof(ProContentDialog), new PropertyMetadata(new Thickness(0), OnContentPaddingChanged));

        private static void OnContentPaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProContentDialog;
            if (control == null) return;

            control.UpdateContentPadding((Thickness)e.NewValue);
        }

        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin", typeof(Thickness), typeof(ProContentDialog), new PropertyMetadata(new Thickness(0), OnContentMarginChanged));

        private static void OnContentMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProContentDialog;
            if (control == null) return;

            control.UpdateContentMargin((Thickness)e.NewValue);
        }

        public new VerticalAlignment VerticalContentAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalContentAlignmentProperty); }
            set { SetValue(VerticalContentAlignmentProperty, value); }
        }

        public new static readonly DependencyProperty VerticalContentAlignmentProperty =
            DependencyProperty.Register(nameof(VerticalContentAlignment), typeof(VerticalAlignment), typeof(ProContentDialog), new PropertyMetadata(VerticalAlignment.Center, OnVerticalContentAlignmentChanged));

        private static void OnVerticalContentAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProContentDialog;
            if (control == null) return;

            control.UpdateVerticalContentAlignment((VerticalAlignment)e.NewValue);
        }

        public new HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }

        public new static readonly DependencyProperty HorizontalContentAlignmentProperty =
            DependencyProperty.Register(nameof(HorizontalContentAlignment), typeof(HorizontalAlignment), typeof(ProContentDialog), new PropertyMetadata(HorizontalAlignment.Center, OnHorizontalContentAlignmentChanged));

        private static void OnHorizontalContentAlignmentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProContentDialog;
            if (control == null) return;

            control.UpdateHorizontalContentAlignment((HorizontalAlignment)e.NewValue);
        }

        public GridLength ContentWidth
        {
            get { return (GridLength)GetValue(ContentWidthProperty); }
            private set { SetValue(ContentWidthProperty, value); }
        }

        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.Register(nameof(ContentWidth), typeof(GridLength), typeof(ProContentDialog), new PropertyMetadata(new GridLength(0, GridUnitType.Auto), OnContentWidthChanged));

        private static void OnContentWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProContentDialog;
            if (control == null) return;

            control.UpdateContentWidth((GridLength)e.NewValue);
        }

        public GridLength ContentHeight
        {
            get { return (GridLength)GetValue(ContentHeightProperty); }
            private set { SetValue(ContentHeightProperty, value); }
        }

        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register(nameof(ContentHeight), typeof(GridLength), typeof(ProContentDialog), new PropertyMetadata(new GridLength(0, GridUnitType.Auto), OnContentHeightChanged));

        private static void OnContentHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProContentDialog;
            if (control == null) return;

            control.UpdateContentHeight((GridLength)e.NewValue);
        }

        public bool FullSizeDesired
        {
            get { return (bool)GetValue(FullSizeDesiredProperty); }
            set { SetValue(FullSizeDesiredProperty, value); }
        }

        public static readonly DependencyProperty FullSizeDesiredProperty =
            DependencyProperty.Register(nameof(FullSizeDesired), typeof(bool), typeof(ProContentDialog), new PropertyMetadata(false, OnFullSizeDesired));

        private static void OnFullSizeDesired(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ProContentDialog;
            if (control == null) return;

            control.UpdateFullSizeDesired((bool)e.NewValue);
        }
        #endregion
    }
}
