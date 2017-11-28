using System;
using System.Collections.Generic;
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

            //sp1.Children.Add(new TextBox()
            //{
            //    PlaceholderText = "Иванов sp1",
            //    Header = "Информация sp1",
            //    FontSize = 16
            //});

            //sp2.Children.Add(new TextBox()
            //{
            //    PlaceholderText = "Иванов sp2",
            //    Header = "Информация sp2",
            //    FontSize = 16
            //});

            //if (_changedView)
            //{
            //    sp1.Visibility = Visibility.Collapsed;
            //    sp2.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    sp2.Visibility = Visibility.Collapsed;
            //    sp1.Visibility = Visibility.Visible;
            //}

            if (_changedView)
            {
                VisualStateManager.GoToState(this, "StateA", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "StateB", true);
            }

            _changedView = !_changedView;

            //RTDContentDialog.Content = new Grid { Width = 200, Height = 300, Background = new SolidColorBrush(Colors.AliceBlue) };
        }

        private void OnBackwardButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            //if (!RTDContentDialog.IsAnimationCompleted) return;

            //sp1.Children.Remove(sp1.Children.LastOrDefault());
            //sp2.Children.Remove(sp2.Children.LastOrDefault());

            //main.Height -= sp1.Height;
        }

        private void OnShowButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            RTDContentDialog.Show();
        }

        public MainPage()
        {
            this.InitializeComponent();
            VisualStateManager.GoToState(this, "DefaultState", true);
        }
    }
}
