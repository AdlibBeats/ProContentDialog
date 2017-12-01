using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace App4.UI.Controls
{
    public class RTDAnimationContentControl : ContentControl
    {
        private RTDContentPresenter _contentPresenter;

        public event RoutedEventHandler AnimationStarted;
        public event RoutedEventHandler AnimationCompleted;

        public RTDAnimationContentControl()
        {
            this.DefaultStyleKey = typeof(RTDAnimationContentControl);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentPresenter = this.GetTemplateChild("ContentPresenter") as RTDContentPresenter;
            if (_contentPresenter == null) return;
            
            _contentPresenter.StoryboardDuration = this.StoryboardDuration;
            _contentPresenter.EasingFunction = this.EasingFunction;
            _contentPresenter.CanAnimate = this.CanAnimate;

            _contentPresenter.AnimationStarted -= OnAnimationStarted;
            _contentPresenter.AnimationStarted += OnAnimationStarted;

            _contentPresenter.AnimationCompleted -= OnAnimationCompleted;
            _contentPresenter.AnimationCompleted += OnAnimationCompleted;
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

        public bool IsAnimationCompleted
        {
            get => (bool)GetValue(IsAnimationCompletedProperty);
            private set => SetValue(IsAnimationCompletedProperty, value);
        }

        public static readonly DependencyProperty IsAnimationCompletedProperty =
            DependencyProperty.Register("IsAnimationCompleted", typeof(bool), typeof(RTDAnimationContentControl), new PropertyMetadata(true));

        public bool CanAnimate
        {
            get => (bool)GetValue(CanAnimateProperty);
            set => SetValue(CanAnimateProperty, value);
        }

        public static readonly DependencyProperty CanAnimateProperty =
            DependencyProperty.Register("CanAnimate", typeof(bool), typeof(RTDAnimationContentControl), new PropertyMetadata(true, OnCanAnimateChanged));

        private static void OnCanAnimateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var contentControl = d as RTDAnimationContentControl;
            if (contentControl == null) return;

            contentControl.UpdateCanAnimate();
        }

        public Duration StoryboardDuration
        {
            get => (Duration)GetValue(StoryboardDurationProperty);
            set => SetValue(StoryboardDurationProperty, value);
        }

        public static readonly DependencyProperty StoryboardDurationProperty =
            DependencyProperty.Register("StoryboardDuration", typeof(Duration), typeof(RTDAnimationContentControl), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(0.15)), OnStoryboardDurationChanged));

        private static void OnStoryboardDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var contentControl = d as RTDAnimationContentControl;
            if (contentControl == null) return;

            contentControl.UpdateStoryboardDuration();
        }

        public EasingFunctionBase EasingFunction
        {
            get => (EasingFunctionBase)GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(EasingFunctionBase), typeof(RTDAnimationContentControl), new PropertyMetadata(null, OnEasingFunctionChanged));

        private static void OnEasingFunctionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var contentControl = d as RTDAnimationContentControl;
            if (contentControl == null) return;

            contentControl.UpdateEasingFunction();
        }
    }
}
