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
        private void OnFinishButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            if (!RTDContentDialog.IsAnimationCompleted) return;

            sp1.Children.Add(new TextBox()
            {
                PlaceholderText = "Иванов sp1",
                Header = "Информация sp1",
                FontSize = 16
            });

            sp2.Children.Add(new TextBox()
            {
                PlaceholderText = "Иванов sp2",
                Header = "Информация sp2",
                FontSize = 16
            });

            //RTDContentDialog.Content = new Grid { Width = 200, Height = 300, Background = new SolidColorBrush(Colors.AliceBlue) };
        }

        private void OnBackwardButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            if (!RTDContentDialog.IsAnimationCompleted) return;

            sp1.Children.Remove(sp1.Children.LastOrDefault());
            sp2.Children.Remove(sp2.Children.LastOrDefault());

            main.Height -= sp1.Height;
        }

        private void OnShowButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            RTDContentDialog.Show();
        }

        public MainPage()
        {
            this.InitializeComponent();
        }
    }
}
