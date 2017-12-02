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
        private Storyboard _storyboard = new Storyboard();
        private Size _animationSize;

        public event RoutedEventHandler AnimationCompleted;
        public event RoutedEventHandler AnimationStarted;

        public RTDContentPresenter()
        {
            this.DefaultStyleKey = typeof(RTDContentPresenter);
        }
        
        protected override Size ArrangeOverride(Size finalSize)
        {
            var arrangeOverride = this.CanAnimate ? StartAnimation(finalSize) : finalSize;

            return base.ArrangeOverride(arrangeOverride);
        }

        private Size StartAnimation(Size finalSize)
        {
            if (!this.IsAnimationCompleted)
                return finalSize;

            _animationSize = finalSize;

            if (!double.IsInfinity(this.ActualHeight) && !double.IsNaN(this.ActualHeight))
                finalSize.Height = this.ActualHeight;
            if (!double.IsInfinity(this.ActualWidth) && !double.IsNaN(this.ActualWidth))
                finalSize.Width = this.ActualWidth;

            this.IsAnimationCompleted = false;

            AnimationStarted?.Invoke(this, new RoutedEventArgs());

            _storyboard.Completed -= OnCompleted;

            _storyboard = new Storyboard();
            _storyboard.Completed += OnCompleted;

            var animations = GetDoubleAnimations();
            foreach (var animation in animations)
                _storyboard.Children.Add(animation);
            _storyboard.Begin();

            return finalSize;
        }

        private IEnumerable<Timeline> GetDoubleAnimations()
        {
            if (this.AnimationType == RTDAnimationType.FullSize)
                return new Timeline[]
                {
                    GetDoubleAnimation(true),
                    GetDoubleAnimation(false)
                };
            else if (this.AnimationType == RTDAnimationType.OnlyHeight)
                return new Timeline[] { GetDoubleAnimation(true) };
            else
                return new Timeline[] { GetDoubleAnimation(false) };
        }

        private void OnCompleted(object sender, object e)
        {
            this.IsAnimationCompleted = true;

            AnimationCompleted?.Invoke(this, new RoutedEventArgs());
        }

        private DoubleAnimation GetDoubleAnimation(bool animationType)
        {
            var animation = new DoubleAnimation
            {
                FillBehavior = FillBehavior.Stop,
                Duration = this.StoryboardDuration,
                From = animationType ? this.ActualHeight : this.ActualWidth,
                To = animationType ? _animationSize.Height : _animationSize.Width,
                EnableDependentAnimation = true,
                AutoReverse = false,
                EasingFunction = this.EasingFunction
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
