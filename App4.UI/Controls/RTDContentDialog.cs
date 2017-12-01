using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

namespace App4.UI.Controls
{
    public sealed class RTDContentDialog : ContentControl
    {
        public event RoutedEventHandler AnimationStarted;
        public event RoutedEventHandler AnimationCompleted;

        private RTDContentPresenter _contentPresenter;
        private FrameworkElement _layoutBackground;

        public RTDContentDialog()
        {
            this.DefaultStyleKey = typeof(RTDContentDialog);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_contentPresenter != null)
            {
                _contentPresenter.AnimationStarted -= OnAnimationStarted;
                _contentPresenter.AnimationCompleted -= OnAnimationCompleted;
            }

            _contentPresenter = this.GetTemplateChild("ContentPresenter") as RTDContentPresenter;
            if (_contentPresenter != null)
            {

                _contentPresenter.StoryboardDuration = this.StoryboardDuration;
                _contentPresenter.EasingFunction = this.EasingFunction;
                _contentPresenter.CanAnimate = this.CanAnimate;

                _contentPresenter.AnimationStarted += OnAnimationStarted;
                _contentPresenter.AnimationCompleted += OnAnimationCompleted;
            }
            
            if (_layoutBackground != null)
                _layoutBackground.Tapped -= OnLayoutBackgroundTapped;

            _layoutBackground = GetTemplateChild("LayoutBackground") as Border;
            if (_layoutBackground != null)
                _layoutBackground.Tapped += OnLayoutBackgroundTapped;

            VisualStateManager.GoToState(this, "CollapsedState", true);
            UpdateSizeState();
        }

        private void UpdateStoryboardDuration()
        {
            if (_contentPresenter == null) return;
            _contentPresenter.StoryboardDuration = this.StoryboardDuration;
        }

        private void UpdateEasingFunction()
        {
            if (_contentPresenter == null) return;
            _contentPresenter.EasingFunction = this.EasingFunction;
        }

        private void UpdateCanAnimate()
        {
            if (_contentPresenter == null) return;
            _contentPresenter.CanAnimate = this.CanAnimate;
        }

        private void OnAnimationStarted(object sender, RoutedEventArgs e)
        {
            this.IsAnimationCompleted = _contentPresenter.IsAnimationCompleted;

            AnimationStarted?.Invoke(sender, e);
        }

        private void OnAnimationCompleted(object sender, RoutedEventArgs e)
        {
            this.IsAnimationCompleted = _contentPresenter.IsAnimationCompleted;

            AnimationCompleted?.Invoke(sender, e);
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

        public bool IsAnimationCompleted
        {
            get => (bool)GetValue(IsAnimationCompletedProperty);
            private set => SetValue(IsAnimationCompletedProperty, value);
        }

        public static readonly DependencyProperty IsAnimationCompletedProperty =
            DependencyProperty.Register("IsAnimationCompleted", typeof(bool), typeof(RTDContentDialog), new PropertyMetadata(true));

        public bool CanAnimate
        {
            get => (bool)GetValue(CanAnimateProperty);
            set => SetValue(CanAnimateProperty, value);
        }

        public static readonly DependencyProperty CanAnimateProperty =
            DependencyProperty.Register("CanAnimate", typeof(bool), typeof(RTDContentDialog), new PropertyMetadata(false, OnCanAnimateChanged));

        private static void OnCanAnimateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var contentControl = d as RTDContentDialog;
            if (contentControl == null) return;

            contentControl.UpdateCanAnimate();
        }

        public Duration StoryboardDuration
        {
            get => (Duration)GetValue(StoryboardDurationProperty);
            set => SetValue(StoryboardDurationProperty, value);
        }

        public static readonly DependencyProperty StoryboardDurationProperty =
            DependencyProperty.Register("StoryboardDuration", typeof(Duration), typeof(RTDContentDialog), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(0.15)), OnStoryboardDurationChanged));

        private static void OnStoryboardDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var contentControl = d as RTDContentDialog;
            if (contentControl == null) return;

            contentControl.UpdateStoryboardDuration();
        }

        public EasingFunctionBase EasingFunction
        {
            get => (EasingFunctionBase)GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(EasingFunctionBase), typeof(RTDContentDialog), new PropertyMetadata(null, OnEasingFunctionChanged));

        private static void OnEasingFunctionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var contentControl = d as RTDContentDialog;
            if (contentControl == null) return;

            contentControl.UpdateEasingFunction();
        }
    }
}
