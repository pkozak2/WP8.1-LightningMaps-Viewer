using Blitzortung_Viewer_UWP.Models;
using Blitzortung_Viewer_UWP.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Blitzortung_Viewer_UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BingLiveMapPage : Page
    {
        string PleaseWaitString = new Windows.ApplicationModel.Resources.ResourceLoader().GetString("PleaseWaitString");

        private MessageWebSocket messageWebSocket;
        private DataWriter messageWriter;
        public string MapServiceToken => Keys.MapServiceToken;

        public List<HistoryBlitz> listOfHistoryBlitz = new List<HistoryBlitz>();

        private string ws_server = Keys.ws_server;

        private string message = Keys.message;

        public BingLiveMapPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Busy.SetBusy(true, PleaseWaitString);
            Map.Style = MapStyle.AerialWithRoads;
            await FillHistoryBlitz();
            await ConnectToWebSocket();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            CloseSocket();
        }

        private void AddPin(double lat, double lon, bool isLive, Color color)
        {
            double borderAndGridStartingHeightWidth = 70;
            double pointHeightWidth = 10;
            double pointBorder = 1;
            double animationTime = 2;

            var pin = new Grid()
            {
                Width = borderAndGridStartingHeightWidth,
                Height = borderAndGridStartingHeightWidth,
                Margin = new Windows.UI.Xaml.Thickness(-35)
            };

            MapControl.SetLocation(pin, new Geopoint(new BasicGeoposition()
            {
                Latitude = lat,
                Longitude = lon
            }));

            if (isLive)
            {
                Ellipse borderEllipse = new Ellipse()
                {
                    Fill = new SolidColorBrush(Colors.White),
                    Stroke = new SolidColorBrush(Colors.Black),
                    Opacity = 0.3,
                    Width = borderAndGridStartingHeightWidth,
                    Height = borderAndGridStartingHeightWidth,
                };
                Duration duration = new Duration(TimeSpan.FromSeconds(animationTime));

                DoubleAnimation myDoubleAnimation1 = new DoubleAnimation();
                DoubleAnimation myDoubleAnimation2 = new DoubleAnimation();

                myDoubleAnimation1.Duration = duration;
                myDoubleAnimation2.Duration = duration;

                Storyboard sb = new Storyboard();
                sb.Duration = duration;

                sb.Children.Add(myDoubleAnimation1);
                sb.Children.Add(myDoubleAnimation2);

                Storyboard.SetTarget(myDoubleAnimation1, borderEllipse);
                Storyboard.SetTarget(myDoubleAnimation2, borderEllipse);

                Storyboard.SetTargetProperty(myDoubleAnimation1, "Height");
                Storyboard.SetTargetProperty(myDoubleAnimation2, "Width");

                myDoubleAnimation1.To = pointHeightWidth;
                myDoubleAnimation2.To = pointHeightWidth;
                myDoubleAnimation1.EnableDependentAnimation = true;
                myDoubleAnimation2.EnableDependentAnimation = true;

                pin.Children.Add(borderEllipse);
                sb.Begin();
            }

            pin.Children.Add(new Ellipse()
            {
                Fill = new SolidColorBrush(color),
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = pointBorder,
                Width = pointHeightWidth,
                Height = pointHeightWidth
            });

            Map.Children.Add(pin);

        }

        private async Task FillHistoryBlitz()
        {
            listOfHistoryBlitz.AddRange(await new JsonDownload().Download());

            foreach (HistoryBlitz blitz in listOfHistoryBlitz)
            {
                AddPin(blitz.lat, blitz.lon, false, Colors.Red);
            }
        }

        private void CloseSocket()
        {
            if (messageWriter != null)
            {
                messageWriter.DetachStream();
                messageWriter.Dispose();
                messageWriter = null;
            }

            if (messageWebSocket != null)
            {
                try
                {
                    messageWebSocket.Close(1000, "Closed due to user request.");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                messageWebSocket = null;
            }
        }

        private async Task ConnectToWebSocket()
        {
            var rnd = new Random().Next(40) + 50;
            string server = "ws://" + ws_server + ":80" + rnd;

            Uri serverConnect = new Uri(server);
            if (serverConnect == null)
            {
                return;
            }

            messageWebSocket = new MessageWebSocket();
            messageWebSocket.Control.MessageType = SocketMessageType.Utf8;
            messageWebSocket.MessageReceived += MessageReceived;
            messageWebSocket.Closed += OnClosed;

            try
            {
                await messageWebSocket.ConnectAsync(serverConnect);
            }
            catch (Exception ex) // For debugging
            {
                // Error happened during connect operation.
                messageWebSocket.Dispose();
                messageWebSocket = null;
                Debug.WriteLine(ex);
                return;

            }
            messageWriter = new DataWriter(messageWebSocket.OutputStream);
            await SendAsync();
        }

        private async void OnClosed(IWebSocket sender, WebSocketClosedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (messageWebSocket == sender)
                {
                    CloseSocket();
                }
            });
        }

        async Task SendAsync()
        {
            messageWriter.WriteString(message);
            Busy.SetBusy(false);
            try
            {
                await messageWriter.StoreAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }
        }

        private void MessageReceived(MessageWebSocket sender, MessageWebSocketMessageReceivedEventArgs args)
        {
            var ignore = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                using (DataReader reader = args.GetDataReader())
                {
                    reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                    try
                    {
                        string read = reader.ReadString(reader.UnconsumedBufferLength);

                        LiveBlitz live = JsonConvert.DeserializeObject<LiveBlitz>(read);

                        AddPin(live.lat, live.lon, true, Colors.Gold);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }
            });
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
