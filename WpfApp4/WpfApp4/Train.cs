using System;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace WpfApp4
{
    public class Train : MetroObject<Status>
    {
        string line;
        int initialStation;
        string direction;
        int stopDurationFrom;
        int stopDurationTo;
        int travelDurationFrom;
        int travelDurationTo;
        Ellipse model;
        List<Station> stations;
        public string Line
        {
            get { return line; }
            set { line = value; }
        }

        public int InitialStation
        {
            get { return initialStation; }
            set { initialStation = value; }
        }

        public string Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public int StopDurationFrom
        {
            get { return stopDurationFrom; }
            set { stopDurationFrom = value; }
        }

        public int StopDurationTo
        {
            get { return stopDurationTo; }
            set { stopDurationTo = value; }
        }

        public int TravelDurationFrom
        {
            get { return travelDurationFrom; }
            set { travelDurationFrom = value; }
        }

        public int TravelDurationTo
        {
            get { return travelDurationTo; }
            set { travelDurationTo = value; }
        }

        public Ellipse Model
        {
            get { return model; }
            set { model = value; }
        }

        public List<Station> Stations
        {
            get { return stations; }
            set { stations = value; }
        }

        public override void ChangeStatus(Status t)
        {
            Status = t;
        }

        public override Status CheckStatus()
        {
            return Status;
        }

        public override string ToString()
        {
            return $"Line: {Line}, Initial Station: {InitialStation}\nDirection: {Direction}\nStop Duration: from {StopDurationFrom} s to {StopDurationTo} s\nTravel Duration: from {TravelDurationFrom} s to {TravelDurationTo} s";
        }
    }
}
