using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace App4.UI.Controls
{
    public sealed class RTDContentDialog : BaseAnimationContentControl
    {
        private Grid _layoutRoot;
        private Grid _layoutBackground;

        public RTDContentDialog()
        {
            this.DefaultStyleKey = typeof(RTDContentDialog);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _layoutRoot = GetTemplateChild("LayoutRoot") as Grid;

            if (_layoutBackground != null)
            {
                _layoutBackground.Tapped -= OnLayoutBackgroundTapped;
            }

            _layoutBackground = GetTemplateChild("LayoutBackground") as Grid;

            if (_layoutBackground != null)
            {
                _layoutBackground.Tapped += OnLayoutBackgroundTapped;
            }

            VisualStateManager.GoToState(this, "CollapsedState", true);
            UpdateSizeState();
        }

        private void OnLayoutBackgroundTapped(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
        {
            Hide();
        }

        public bool FullSizeDesire
        {
            get => (bool)GetValue(FullSizeDesireProperty);
            set => SetValue(FullSizeDesireProperty, value);
        }

        public static readonly DependencyProperty FullSizeDesireProperty =
            DependencyProperty.Register(nameof(FullSizeDesire), typeof(bool), typeof(RTDContentDialog), new PropertyMetadata(false, OnFullSizeDesireChanged));

        public double BackgroundOpacity
        {
            get => (double)GetValue(BackgroundOpacityProperty);
            set => SetValue(BackgroundOpacityProperty, value);
        }

        public static readonly DependencyProperty BackgroundOpacityProperty =
            DependencyProperty.Register(nameof(BackgroundOpacity), typeof(double), typeof(RTDContentDialog), new PropertyMetadata(0.0));

        private static void OnFullSizeDesireChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is RTDContentDialog contentDialog)) return;
            contentDialog.UpdateSizeState();
        }

        private void UpdateSizeState()
        {
            VisualStateManager.GoToState(this, FullSizeDesire ? "FullSizeState" : "NormalState", true);
        }

        public void Show()
        {
            VisualStateManager.GoToState(this, "VisibleState", true);
        }

        public void Hide()
        {
            VisualStateManager.GoToState(this, "CollapsedState", true);
        }
    }
}
