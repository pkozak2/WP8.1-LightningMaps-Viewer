using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blitzortung_Viewer_UWP.Models
{
    public class HistoryBlitz
    {
        public object time { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }

        public HistoryBlitz(object time, double lat, double lon)
        {
            this.time = time;
            this.lat = lat;
            this.lon = lon;
        }
    }
}
