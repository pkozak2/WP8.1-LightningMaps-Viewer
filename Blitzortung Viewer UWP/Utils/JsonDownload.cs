using Blitzortung_Viewer_UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blitzortung_Viewer_UWP.Utils
{
    class JsonDownload
    {
        public async Task<List<HistoryBlitz>> Download()
        {
            List<HistoryBlitz> downloadedBlitz = new List<HistoryBlitz>();
            List<string> adresy = new HistoryBlitzAdress().adresy;
            foreach (string adres in adresy)
            {
                var uri = new Uri(adres);
                var client = new HttpClient();
                var json = await client.GetStringAsync(uri);
                downloadedBlitz.AddRange(JsonConvert.DeserializeObject<List<HistoryBlitz>>(json));
            }

            return downloadedBlitz;
        }
    }
}
