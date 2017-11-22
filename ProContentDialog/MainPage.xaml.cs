using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ProContentDialog.UI.Controls;

namespace ProContentDialog
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            proContentDialog.FullSizeDesired = !proContentDialog.FullSizeDesired;

            if (!proContentDialog.FullSizeDesired)
            {
                proContentDialog.VerticalContentAlignment = VerticalAlignment.Top;
                proContentDialog.HorizontalContentAlignment = HorizontalAlignment.Center;
                proContentDialog.ContentMargin = new Thickness(0, 100, 0, -100);
            }
            else
            {
                proContentDialog.VerticalContentAlignment = VerticalAlignment.Stretch;
                proContentDialog.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                proContentDialog.ContentMargin = new Thickness(0, 0, 0, 0);
            }
        }
    }
}
