using System;
using Blitzortung_Viewer_UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;


namespace Blitzortung_Viewer_UWP.Views
{
    public sealed partial class LiveMapPage : Page
    {

        string PleaseWaitString = new Windows.ApplicationModel.Resources.ResourceLoader().GetString("PleaseWaitString");
        public LiveMapPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            Busy.SetBusy(true, PleaseWaitString);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            
            web.Navigate(new Uri("http://www.lightningmaps.org/#m=osm"));
        }

        private void webContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            Busy.SetBusy(false);
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            Busy.SetBusy(true, PleaseWaitString);
            web.Refresh();
        }
    }
}
