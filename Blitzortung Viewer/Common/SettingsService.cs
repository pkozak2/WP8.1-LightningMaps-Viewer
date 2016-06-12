using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blitzortung_Viewer.Common
{
    class SettingsService
    {

        public static SettingsService Instance { get; }

        static SettingsService()
        {
            // implement singleton pattern
            Instance = Instance ?? new SettingsService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Nazwa"></param>
        /// <returns></returns>
        public object ReadSetting(string Nazwa)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            object setting = localSettings.Values[Nazwa];
            return setting;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Nazwa"></param>
        /// <param name="Wartosc"></param>
        public void SaveSetting(string Nazwa, string Wartosc)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values[Nazwa] = Wartosc;
        }
    }
}
