using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace Blitzortung_Viewer_UWP.ViewModels
{
    public class ImageMapPageViewModel : ViewModelBase
    {
        public ImageMapPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        public string _Value = "image_b_earth";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public string _Url = "image_b_earth";
        public string Url { get { return _Url; } set { Set(ref _Url, value); } }


        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Value = (suspensionState.ContainsKey(nameof(Value))) ? suspensionState[nameof(Value)]?.ToString() : parameter?.ToString();
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                Url = "http://images.blitzortung.org/Images/" + Value + ".png";
            }
            else
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                var str = loader.GetString("CheckInternet");
                MessageDialog message = new MessageDialog(str);
                await message.ShowAsync();
            }
            
            
            await Task.CompletedTask;

        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }
    }
}

