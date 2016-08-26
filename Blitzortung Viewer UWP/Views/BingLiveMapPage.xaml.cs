using Blitzortung_Viewer_UWP.Models;
using Blitzortung_Viewer_UWP.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Blitzortung_Viewer_UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BingLiveMapPage : Page
    {
        private MessageWebSocket messageWebSocket;
        private DataWriter messageWriter;
        public string MapServiceToken => Keys.MapServiceToken;

        public List<HistoryBlitz> listOfHistoryBlitz = new List<HistoryBlitz>();

        private string ws_server = Keys.ws_server;
        public BingLiveMapPage()
        {
            this.InitializeComponent();
        }
    }
}
