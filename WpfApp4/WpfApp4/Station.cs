using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
     public class Station : MetroObject<bool>
    {
        public static List<Station> redFwdStations;
        public static List<Station> redBwdStations;
        public static List<Station> greenFwdStations;
        public static List<Station> greenBwdStations;
        int x, y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public Station(int x, int y)
        {
            this.x = x;
            this.y = y;
            status = true;
        }

        public override bool CheckStatus()
        {
            return Status;
        }

        public override void ChangeStatus(bool t)
        {
            Status = t;
        }
    }
}
