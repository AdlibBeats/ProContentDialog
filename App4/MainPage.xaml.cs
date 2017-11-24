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
            //RTDContentDialog.FullSizeDesire = true;

            RTDContentDialog.CanAnimate = true;
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
        }

        private void OnBackwardButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            RTDContentDialog.CanAnimate = true;
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

            RTDContentDialog.CanAnimate = true;
        }

        //bool IsControlChangedOrSetNewControl = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (!IsControlChangedOrSetNewControl)
            //{
            //    SetNewContent();
            //    IsControlChangedOrSetNewControl = false;
            //}
            //else
            //    ChangeContentSize();

            //IsControlChangedOrSetNewControl = true;
        }

        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //content.Measure(e.NewSize);
        }

        //private void SetNewContent()
        //{
        //    customControl.CanAnimate = true;

        //    var stackPanel = new StackPanel()
        //    {
        //        VerticalAlignment = VerticalAlignment.Top,
        //        Background = new SolidColorBrush(Colors.Aqua),
        //        Width = 600,
        //        Height = 700
        //    };

        //    var content = new TextBlock()
        //    {
        //        VerticalAlignment = VerticalAlignment.Top,
        //        HorizontalAlignment = HorizontalAlignment.Center,
        //        Text = "sfsdfsdfsdfsfsdfsdfsdfsdfsdfsdfsfsdfsdfsdfsdfsfsfsdfsdfsdfsdfsdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfsdfdfsfsf",
        //        TextWrapping = TextWrapping.WrapWholeWords
        //    };

        //    stackPanel.Children.Add(content);

        //    customControl.Content = stackPanel;
        //}

        //private void SetAnotherNewContent()
        //{
        //    customControl.CanAnimate = true;

        //    var stackPanel = new StackPanel()
        //    {
        //        VerticalAlignment = VerticalAlignment.Top,
        //        Background = new SolidColorBrush(Colors.Aqua),
        //        Width = 600,
        //        Height = 200
        //    };

        //    var content = new TextBlock()
        //    {
        //        VerticalAlignment = VerticalAlignment.Top,
        //        HorizontalAlignment = HorizontalAlignment.Center,
        //        Text = "sfsdfsdfsdfsfsdfsdfsdfsdfsdfsdfsfsdfsdfsdfsdfsfsfsdfsdfsdfsdfsdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfsdfdfsfsfsfsdfsdfsdfsfsdfsdfsdfsdfsdfsdfsfsdfsdfsdfsdfsfsfsdfsdfsdfsdfsdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfsdfdfsfsfsfsdfsdfsdfsfsdfsdfsdfsdfsdfsdfsfsdfsdfsdfsdfsfsfsdfsdfsdfsdfsdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfsdfdfsfsfsfsdfsdfsdfsfsdfsdfsdfsdfsdfsdfsfsdfsdfsdfsdfsfsfsdfsdfsdfsdfsdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfsdfdfsfsfsfsdfsdfsdfsfsdfsdfsdfsdfsdfsdfsfsdfsdfsdfsdfsfsfsdfsdfsdfsdfsdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfsdfdfsfsfsfsdfsdfsdfsfsdfsdfsdfsdfsdfsdfsfsdfsdfsdfsdfsfsfsdfsdfsdfsdfsdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfgdfsdfdfsfsf"
        //        , TextWrapping = TextWrapping.WrapWholeWords
        //    };

        //    stackPanel.Children.Add(content);

        //    customControl.Content = stackPanel;
        //}

        //private void ChangeContentSize()
        //{
        //    customControl.CanAnimate = true;

        //    var stackPanel = customControl.Content as StackPanel;
        //    stackPanel.Height += 100;
        //}
    }
}
