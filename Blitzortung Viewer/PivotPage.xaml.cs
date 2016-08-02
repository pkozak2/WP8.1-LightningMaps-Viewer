using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Documents;
using System;

// The Pivot Application template is documented at http://go.microsoft.com/fwlink/?LinkID=391641

namespace Blitzortung_Viewer
{
    public sealed partial class PivotPage : Page
    {

        public PivotPage()
        {
            this.InitializeComponent();

            //webView.NavigationStarting += webView_NavigationStarting;
            //webView.ContentLoading += webView_ContentLoading;
            //webView.DOMContentLoaded += webView_DOMContentLoaded;
            //webView.UnviewableContentIdentified += webView_UnviewableContentIdentified;
            //webView.NavigationCompleted += webView_NavigationCompleted;

        }



        /// <summary>
        /// Invoked when this xaml page is about to be displayed in a Frame.
        /// Note: This event is not related to the webview navigation.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //address.Text = "http://m.lightningmaps.org/realtime";
            //NavigateWebview("http://www.microsoft.com");
            String adres = "http://m.lightningmaps.org/realtime";
           // String adres = "http://google.pl";
           
            NavigateWebview(adres);
        }

        /// <summary>
        /// This is the click handler for the 'Navigation' button.  You would replace this with your own handler
        /// if you have a button or buttons on this page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            if (!pageIsLoading)
            {
                //NavigateWebview(address.Text);
            }
            else
            {
                webView.Stop();
                pageIsLoading = false;
            }
        }

        /// <summary>
        /// This handles the enter key in the url address box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void address_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                //NavigateWebview(address.Text);
            }
        }

        /// <summary>
        /// Helper to perform the navigation in webview
        /// </summary>
        /// <param name="url"></param>
        private void NavigateWebview(string url)
        {
           
            try
            {
                Uri targetUri = new Uri(url);
                webView.Navigate(targetUri);
            }
            catch (FormatException myE)
            {
                // Bad address
                webView.NavigateToString(String.Format("<h1>Address is invalid, try again.</h1>" + myE.ToString()));
            }
        }

        /// <summary>
        /// Property to control the "Go" button text, forward/backward buttons and progress ring
        /// </summary>
        private bool _pageIsLoading;
        bool pageIsLoading
        {
            get { return _pageIsLoading; }
            set
            {
                _pageIsLoading = value;
               // goButton.Content = (value ? "Stop" : "Go");
                progressRing1.Visibility = (value ? Visibility.Visible : Visibility.Collapsed);

                if (!value)
                {

                }
            }
        }

        /// <summary>
        /// Event to indicate webview is starting a navigation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void webView1_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            string url = "";
            try { url = args.Uri.ToString(); }
            finally
            {
               // address.Text = url;
                
                pageIsLoading = true;
            }
        }

        /// <summary>
        /// Event is fired by webview when the content is not a webpage, such as a file download
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void webView1_UnviewableContentIdentified(WebView sender, WebViewUnviewableContentIdentifiedEventArgs args)
        {
            
            // We turn around and hand the Uri to the system launcher to launch the default handler for it
            await Windows.System.Launcher.LaunchUriAsync(args.Uri);
            pageIsLoading = false;
        }

        /// <summary>
        /// Event to indicate webview has resolved the uri, and that it is loading html content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void webView1_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        {
            string url = (args.Uri != null) ? args.Uri.ToString() : "<null>";
        }

        /// <summary>
        /// Event to indicate that the content is fully loaded in the webview. If you need to invoke script, it is best to wait for this event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void webView1_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            string url = (args.Uri != null) ? args.Uri.ToString() : "<null>";
           
        }

        /// <summary>
        /// Event to indicate webview has completed the navigation, either with success or failure.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void webView1_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            pageIsLoading = false;
            if (args.IsSuccess)
            {
                string url = (args.Uri != null) ? args.Uri.ToString() : "<null>";
               
            }
            else
            {
                string url = "";
                try { url = args.Uri.ToString(); }
                finally
                {
                    //address.Text = url;
                    
                }
            }
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            
            webView.Refresh();
        }


        private void WebView_LoadCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            
        }

    }
}
