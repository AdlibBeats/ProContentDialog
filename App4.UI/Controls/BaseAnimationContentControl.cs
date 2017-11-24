using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace App4.UI.Controls
{
    public class BaseAnimationContentControl : Control
    {
        public BaseAnimationContentControl()
        {
            this.DefaultStyleKey = typeof(BaseAnimationContentControl);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateContent(this.Content);
        }

        public object Content
        {
            get { return GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(BaseAnimationContentControl), new PropertyMetadata(null, OnContentChanged));

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var baseAnimationContentControl = d as BaseAnimationContentControl;
            if (d == null) return;

            baseAnimationContentControl.UpdateContent(e.NewValue);
        }

        public bool CanAnimate
        {
            get { return (bool)GetValue(CanAnimateProperty); }
            set { SetValue(CanAnimateProperty, value); }
        }

        public static readonly DependencyProperty CanAnimateProperty =
            DependencyProperty.Register("CanAnimate", typeof(bool), typeof(BaseAnimationContentControl), new PropertyMetadata(false));

        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Duration), typeof(BaseAnimationContentControl), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(0.5))));

        private void UpdateContent(object value)
        {
            var rootContent = this.GetTemplateChild("RootContent") as ContentPresenter;
            if (rootContent != null)
                rootContent.Content = value;

            var frameworkElement = value as FrameworkElement;
            if (frameworkElement == null) return;

            frameworkElement.SizeChanged -= Content_SizeChanged;
            frameworkElement.SizeChanged += Content_SizeChanged;

            void Content_SizeChanged(object sender, SizeChangedEventArgs e)
            {
                StartAnimation(frameworkElement, e.PreviousSize.Height, e.NewSize.Height);
            }
        }

        private void StartAnimation(FrameworkElement frameworkElement, double from, double to)
        {
            if (this.CanAnimate)
            {
                var animation = new DoubleAnimation()
                {
                    Duration = this.Duration,
                    From = from,
                    To = to,
                    EnableDependentAnimation = true,
                    AutoReverse = false
                };

                var easingFunction = new ElasticEase()
                {
                    EasingMode = EasingMode.EaseOut,
                    Oscillations = 2
                };

                animation.EasingFunction = easingFunction;

                Storyboard.SetTarget(animation, frameworkElement);
                Storyboard.SetTargetProperty(animation, "Height");

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(animation);
                storyboard.Begin();

                this.CanAnimate = false;
            }
        }
    }
}
