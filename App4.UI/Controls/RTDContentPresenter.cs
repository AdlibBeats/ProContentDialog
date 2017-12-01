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
        private Size _finalSize;

        public event RoutedEventHandler AnimationCompleted;
        public event RoutedEventHandler AnimationStarted;

        public RTDContentPresenter()
        {
            this.DefaultStyleKey = typeof(RTDContentPresenter);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _finalSize = finalSize;

            return this.CanAnimate ? StartAnimation() : base.ArrangeOverride(_finalSize);
        }

        private Size StartAnimation()
        {
            if (!this.IsAnimationCompleted) return base.ArrangeOverride(_finalSize);

            this.IsAnimationCompleted = false;

            AnimationStarted?.Invoke(this, new RoutedEventArgs());

            var storyboard = new Storyboard();
            storyboard.Completed += OnCompleted;

            foreach (var animation in GetAnimations().Where(i => i != null))
                storyboard.Children.Add(animation);
            storyboard.Begin();

            return _finalSize;
        }

        private Timeline[] GetAnimations()
        {
            Timeline[] timeLines = new Timeline[2];
            switch (this.AnimationType)
            {
                case RTDAnimationType.FullSize:
                    {
                        timeLines[0] = GetDoubleAnimation(true, this.StoryboardDuration, this.EasingFunction);
                        timeLines[1] = GetDoubleAnimation(false, this.StoryboardDuration, this.EasingFunction);
                        break;
                    }
                case RTDAnimationType.OnlyHeight:
                    {
                        timeLines[0] = GetDoubleAnimation(true, this.StoryboardDuration, this.EasingFunction);
                        break;
                    }
                case RTDAnimationType.OnlyWidth:
                    {
                        timeLines[0] = GetDoubleAnimation(false, this.StoryboardDuration, this.EasingFunction);
                        break;
                    }
            }
            return timeLines;
        }

        private void OnCompleted(object sender, object e)
        {
            this.IsAnimationCompleted = true;

            AnimationCompleted?.Invoke(this, new RoutedEventArgs());
        }

        private DoubleAnimation GetDoubleAnimation(bool animationType, Duration duration, EasingFunctionBase easingFunction = null)
        {
            var animation = new DoubleAnimation
            {
                FillBehavior = FillBehavior.Stop,
                Duration = duration,
                From = animationType ? this.ActualHeight : this.ActualWidth,
                To = animationType ? _finalSize.Height : _finalSize.Width,
                EnableDependentAnimation = true,
                AutoReverse = false,
                EasingFunction = easingFunction
            };

            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, animationType ? "Height" : "Width");

            return animation;
        }

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
            DependencyProperty.Register("CanAnimate", typeof(bool), typeof(RTDContentPresenter), new PropertyMetadata(false));

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

        public RTDAnimationType AnimationType
        {
            get => (RTDAnimationType)GetValue(AnimationTypeProperty);
            set => SetValue(AnimationTypeProperty, value);
        }

        public static readonly DependencyProperty AnimationTypeProperty =
            DependencyProperty.Register("AnimationType", typeof(RTDAnimationType), typeof(RTDContentPresenter), new PropertyMetadata(RTDAnimationType.FullSize));
    }
}
