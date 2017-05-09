using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Zaragoza_Bici
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Colors.CadetBlue;
            //Navigate to portada
            ContentFrame.Navigate(typeof(pagina1));
            NavigateToFrontPage.IsChecked = true;
        }
        private void HamburguerButtton_Click(object o, RoutedEventArgs e)
        {
            Split.IsPaneOpen = !Split.IsPaneOpen;
            HamburguerButton.IsChecked = false;
        }
        private void pagina1_Click(Object o, RoutedEventArgs e)
        {
            //Navigate to page 1
            ContentFrame.Navigate(typeof(pagina1));
            NavigateToPage1.IsChecked = true;
        }
          private void pagina3_Click(Object o, RoutedEventArgs e)
           {
               //Navigate to page 2
               ContentFrame.Navigate(typeof(pagina3));
               NavigateToPage3.IsChecked = true;
           }
        private void pagina2_Click(Object o, RoutedEventArgs e)
        {
            //Navigate to portada
            ContentFrame.Navigate(typeof(pagina2));
            NavigateToFrontPage.IsChecked = true;
        }
        private void GoBack_Click(Object o, RoutedEventArgs e)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
            GoBack.IsChecked = false;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
