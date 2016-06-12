using Blitzortung_Viewer.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Blitzortung_Viewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage1 : Page
    {
        string tag;
        string version = "u12062016";
        public MainPage1()
        {
            this.InitializeComponent();
            ContentFrame.Navigate(typeof(MainPage));

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (!localSettings.Values.ContainsKey(version))
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                var messageHeader = loader.GetString("UpdateHeader");
                var messageText = loader.GetString("UpdateText");
                MessageDialog message = new MessageDialog(messageText, messageHeader);
                await message.ShowAsync();
                SettingsService.Instance.SaveSetting(version, "true");
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.GoBack();
            ContentFrame.Navigate(typeof(MapPage), tag);
        }

        private void HomeButton_CLick(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = false;
            RefreshButton.Visibility = Visibility.Collapsed;
            if (MySplitView.Content != null)
                ContentFrame.Navigate(typeof(MainPage));
        }
        private void MapPageButton_Click(object sender, RoutedEventArgs e)
        {
            tag = ((Button)sender).Tag.ToString();
            RefreshButton.Visibility = Visibility.Visible;
            MySplitView.IsPaneOpen = false;
            if (MySplitView.Content != null)
                ContentFrame.Navigate(typeof(MapPage), tag);
        }

        private void LiveMapButton_Click(object sender, RoutedEventArgs e)
        {
            tag = ((Button)sender).Tag.ToString();
            if (tag.Equals("false"))
            {
                RefreshButton.Visibility = Visibility.Collapsed;
            }
            MySplitView.IsPaneOpen = false;
            if (MySplitView.Content != null)
            {
                ContentFrame.Navigate(typeof(LiveMapPage));
            }
        }
    }
}
