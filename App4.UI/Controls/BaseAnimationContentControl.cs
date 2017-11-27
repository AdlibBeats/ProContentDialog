﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace App4.UI.Controls
{
    public class BaseAnimationContentControl : Control
    {
        private FrameworkElement _frameworkElementContent;

        public event RoutedEventHandler AnimationCompleted;

        public BaseAnimationContentControl()
        {
            this.DefaultStyleKey = typeof(BaseAnimationContentControl);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateContent(this.Content);
        }

        private void UpdateCanAnimate(bool value)
        {
            _frameworkElementContent = this.Content as FrameworkElement;
            if (_frameworkElementContent == null) return;

            _frameworkElementContent.SizeChanged -= OnSizeChanged;

            if (value)
                _frameworkElementContent.SizeChanged += OnSizeChanged;

            void OnSizeChanged(object sender, SizeChangedEventArgs e) =>
                StartAnimation(_frameworkElementContent, e.PreviousSize.Height, e.NewSize.Height, this.StoryboardDuration, this.StoryboardPath);
        }

        private void UpdateContent(object value)
        {
            var contentPresenter = this.GetTemplateChild("ContentPresenter") as ContentPresenter;
            if (contentPresenter == null) return;

            contentPresenter.Content = value;

            UpdateCanAnimate(this.CanAnimate);
        }
        
        private void StartAnimation(FrameworkElement frameworkElement, double from, double to, Duration storyboardDuration, string storyboardPath)
        {
            if (!this.IsAnimationCompleted) return;

            this.IsAnimationCompleted = false;

            var doubleAnimation = GetDoubleAnimation(from, to, storyboardDuration);
            var easingFunction = GetEasingFunction(EasingMode.EaseOut, 2);

            doubleAnimation.EasingFunction = easingFunction;

            Storyboard.SetTarget(doubleAnimation, frameworkElement);
            Storyboard.SetTargetProperty(doubleAnimation, storyboardPath);

            Storyboard storyboard = new Storyboard();

            storyboard.Completed -= OnCompleted;
            storyboard.Completed += OnCompleted;

            void OnCompleted(object sender, object e)
            {
                this.IsAnimationCompleted = true;
                AnimationCompleted?.Invoke(this, new RoutedEventArgs());
            };

            storyboard.Children.Add(doubleAnimation);
            storyboard.Begin();
        }

        private DoubleAnimation GetDoubleAnimation(double from, double to, Duration storyboardDuration) => new DoubleAnimation
        {
            Duration = storyboardDuration,
            From = from,
            To = to,
            EnableDependentAnimation = true,
            AutoReverse = false
        };

        private ElasticEase GetEasingFunction(EasingMode easingMode, int oscillations) => new ElasticEase
        {
            EasingMode = EasingMode.EaseOut,
            Oscillations = 2
        };

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

        public bool IsAnimationCompleted
        {
            get { return (bool)GetValue(IsAnimationCompletedProperty); }
            private set { SetValue(IsAnimationCompletedProperty, value); }
        }

        public static readonly DependencyProperty IsAnimationCompletedProperty =
            DependencyProperty.Register("IsAnimationCompleted", typeof(bool), typeof(BaseAnimationContentControl), new PropertyMetadata(true));

        public bool CanAnimate
        {
            get { return (bool)GetValue(CanAnimateProperty); }
            set { SetValue(CanAnimateProperty, value); }
        }

        public static readonly DependencyProperty CanAnimateProperty =
            DependencyProperty.Register("CanAnimate", typeof(bool), typeof(BaseAnimationContentControl), new PropertyMetadata(true, OnCanAnimateChanged));

        private static void OnCanAnimateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var baseAnimationContentControl = d as BaseAnimationContentControl;
            if (baseAnimationContentControl == null) return;

            baseAnimationContentControl.UpdateCanAnimate((bool)e.NewValue);
        }

        public Duration StoryboardDuration
        {
            get { return (Duration)GetValue(StoryboardDurationProperty); }
            set { SetValue(StoryboardDurationProperty, value); }
        }

        public static readonly DependencyProperty StoryboardDurationProperty =
            DependencyProperty.Register("StoryboardDuration", typeof(Duration), typeof(BaseAnimationContentControl), new PropertyMetadata(new Duration(TimeSpan.FromSeconds(0.5))));

        public string StoryboardPath
        {
            get { return (string)GetValue(StoryboardPathProperty); }
            set { SetValue(StoryboardPathProperty, value); }
        }

        public static readonly DependencyProperty StoryboardPathProperty =
            DependencyProperty.Register("StoryboardPath", typeof(string), typeof(BaseAnimationContentControl), new PropertyMetadata("Height"));
    }
}
