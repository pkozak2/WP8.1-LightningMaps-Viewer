using Windows.UI.Xaml;
using System.Threading.Tasks;
using Blitzortung_Viewer_UWP.Services.SettingsServices;
using Windows.ApplicationModel.Activation;
using Template10.Controls;
using Template10.Common;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;

namespace Blitzortung_Viewer_UWP
{
    /// Documentation on APIs used in this page:
    /// https://github.com/Windows-XAML/Template10/wiki

    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Views.Splash(e);

            #region App settings

            var _settings = SettingsService.Instance;
            RequestedTheme = _settings.AppTheme;
            CacheMaxDuration = _settings.CacheMaxDuration;
            ShowShellBackButton = _settings.UseShellBackButton;

            #endregion

            
        }

        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {

                var statusBar = StatusBar.GetForCurrentView();
                //if (statusBar != null)
                //{
                //    statusBar.BackgroundOpacity = 1;
                //    statusBar.BackgroundColor = Colors.DarkBlue;
                //    statusBar.ForegroundColor = Colors.White;
                await statusBar.HideAsync();
                //}
            }

            if (Window.Current.Content as ModalDialog == null)
            {
                // create a new frame 
                var nav = NavigationServiceFactory(BackButton.Attach, ExistingContent.Include);

                // create modal root
                Window.Current.Content = new ModalDialog
                {
                    DisableBackButtonWhenModal = true,
                    Content = new Views.Shell(nav),
                    ModalContent = new Views.Busy(),
                };
            }
            await Task.CompletedTask;
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // long-running startup tasks go here
            await Task.Delay(1000);

            NavigationService.Navigate(typeof(Views.MainPage));
            await Task.CompletedTask;
        }
    }
}

