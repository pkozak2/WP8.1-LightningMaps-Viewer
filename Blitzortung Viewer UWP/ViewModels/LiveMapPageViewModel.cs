using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace Blitzortung_Viewer_UWP.ViewModels
{
    public class LiveMapPageViewModel : ViewModelBase
    {
        public LiveMapPageViewModel()
        {

        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }


    }
}

