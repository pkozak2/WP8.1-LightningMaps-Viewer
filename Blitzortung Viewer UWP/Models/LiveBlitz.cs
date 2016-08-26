using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blitzortung_Viewer_UWP.Models
{
    class LiveBlitz
    {
        public object time { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public int alt { get; set; }
        public int pol { get; set; }
        public int mds { get; set; }
        public int mcg { get; set; }
        public List<Sig> sig { get; set; }
        public double delay { get; set; }
    }
}
