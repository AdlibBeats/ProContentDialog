using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace App4
{
    public sealed partial class MainPage : Page
    {
        private bool _changedView = true;
        private void OnFinishButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            if (!RTDContentDialog.IsAnimationCompleted) return;

            //RTDContentDialog.CanAnimate = true;

            //Can comment this
            if (_changedView)
                VisualStateManager.GoToState(this, "StateA", true);
            else
                VisualStateManager.GoToState(this, "StateB", true);

            _changedView = !_changedView;

            Debug.WriteLine(RTDContentDialog.CanAnimate);
            //Can comment this
            //RTDContentDialog.Content = new Grid { Width = 460, Height = 100, Background = new SolidColorBrush(Colors.Red) };
        }

        private bool _changedDuration = true;
        private void OnBackwardButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            if (!RTDContentDialog.IsAnimationCompleted) return;

            //sp1.Children.Remove(sp1.Children.LastOrDefault());
            //sp2.Children.Remove(sp2.Children.LastOrDefault());

            if (_changedDuration)
                RTDContentDialog.StoryboardDuration = new Duration(TimeSpan.FromSeconds(1));
            else
                RTDContentDialog.StoryboardDuration = new Duration(TimeSpan.FromSeconds(0.15));

            _changedDuration = !_changedDuration;
        }

        private void OnShowButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            RTDContentDialog.Show();
        }

        public MainPage()
        {
            this.InitializeComponent();

            VisualStateManager.GoToState(this, "DefaultState", true);

            //if (RTDContentDialog != null)
            //    RTDContentDialog.AnimationCompleted += OnAnimationCompleted;

            //void OnAnimationCompleted(object sender, RoutedEventArgs e) =>
            //    RTDContentDialog.CanAnimate = false;
        }
    }
}
