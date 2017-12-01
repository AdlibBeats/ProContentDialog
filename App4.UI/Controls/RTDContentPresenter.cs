using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace App4.UI.Controls
{
    public class RTDContentPresenter : ContentControl
    {
        public event RoutedEventHandler AnimationCompleted;
        public event RoutedEventHandler AnimationStarted;

        public RTDContentPresenter()
        {
            this.DefaultStyleKey = typeof(RTDContentPresenter);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return StartAnimation(this.ActualHeight, finalSize);
        }

        private Size StartAnimation(double from, Size finalSize)
        {
            if (!this.IsAnimationCompleted) return base.ArrangeOverride(finalSize);

            this.IsAnimationCompleted = false;

            AnimationStarted?.Invoke(this, new RoutedEventArgs());

            Timeline animation = null;
            if (this.CanAnimate)
                animation = GetDoubleAnimation(from, finalSize.Height, this.StoryboardDuration, this.EasingFunction);
            else
                animation = GetDoubleAnimation(from, finalSize.Height, new Duration(TimeSpan.FromSeconds(0.001)));

            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, "Height");

            Storyboard storyboard = new Storyboard();

            storyboard.Completed -= OnCompleted;
            storyboard.Completed += OnCompleted;

            storyboard.Children.Add(animation);
            storyboard.Begin();

            return finalSize;
        }

        private void OnCompleted(object sender, object e)
        {
            this.IsAnimationCompleted = true;

            AnimationCompleted?.Invoke(this, new RoutedEventArgs());
        }

        private DoubleAnimation GetDoubleAnimation(double from, double to, Duration duration, EasingFunctionBase easingFunction = null) => new DoubleAnimation
        {
            FillBehavior = FillBehavior.Stop,
            Duration = duration,
            From = from,
            To = to,
            EnableDependentAnimation = true,
            AutoReverse = false,
            EasingFunction = easingFunction
        };

        public bool IsAnimationCompleted
        {
            get => (bool)GetValue(IsAnimationCompletedProperty);
            private set => SetValue(IsAnimationCompletedProperty, value);
        }

        public static readonly DependencyProperty IsAnimationCompletedProperty =
            DependencyProperty.Register("IsAnimationCompleted", typeof(bool), typeof(RTDContentPresenter), new PropertyMetadata(true));

        public bool CanAnimate
        {
            get => (bool)GetValue(CanAnimateProperty);
            set => SetValue(CanAnimateProperty, value);
        }

        public static readonly DependencyProperty CanAnimateProperty =
            DependencyProperty.Register("CanAnimate", typeof(bool), typeof(RTDContentPresenter), new PropertyMetadata(true));

        public Duration StoryboardDuration
        {
            get => (Duration)GetValue(StoryboardDurationProperty);
            set => SetValue(StoryboardDurationProperty, value);
        }

        public static readonly DependencyProperty StoryboardDurationProperty =
            DependencyProperty.Register("StoryboardDuration", typeof(Duration), typeof(RTDContentPresenter), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(0.15))));

        public EasingFunctionBase EasingFunction
        {
            get => (EasingFunctionBase)GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(EasingFunctionBase), typeof(RTDContentPresenter), new PropertyMetadata(null));
    }
}
