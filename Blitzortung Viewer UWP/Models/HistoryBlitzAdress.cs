using Blitzortung_Viewer_UWP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blitzortung_Viewer_UWP.Models
{
    class HistoryBlitzAdress
    {
        public List<String> adresy = new List<string>();

        public HistoryBlitzAdress()
        {
            adresy.Add(Keys.historyRegion1);
            adresy.Add(Keys.historyRegion2);
            adresy.Add(Keys.historyRegion3);
            adresy.Add(Keys.historyRegion4);
            adresy.Add(Keys.historyRegion5);
            adresy.Add(Keys.historyRegion6);
        }
    }
}
