using System.Windows;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for TrainFormWindow.xaml
    /// </summary>
    public partial class TrainFormWindow : Window
    {
        public Train train;
        public TrainFormWindow()
        {
            InitializeComponent();
            train = new Train();
        }

        public TrainFormWindow(Train train)
        {
            InitializeComponent();
            this.train = train;

            cmbLine.SelectedItem = train.Line == "Green" ? cmbLine.Items[1] : cmbLine.Items[0];
            cmbInitialStation.SelectedItem = cmbInitialStation.Items[train.InitialStation - 1];
            cmbDirection.SelectedItem = train.Direction == "Reverse" ? cmbDirection.Items[1] : cmbDirection.Items[0];
            txtStopDurationFrom.Text = train.StopDurationFrom.ToString();
            txtStopDurationTo.Text = train.StopDurationTo.ToString();
            txtTravelDurationFrom.Text = train.TravelDurationFrom.ToString();
            txtTravelDurationTo.Text = train.TravelDurationTo.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbLine.SelectedItem == null || cmbInitialStation.SelectedItem == null || cmbDirection.SelectedItem == null ||
            string.IsNullOrEmpty(txtStopDurationFrom.Text) || string.IsNullOrEmpty(txtStopDurationTo.Text) ||
            string.IsNullOrEmpty(txtTravelDurationFrom.Text) || string.IsNullOrEmpty(txtTravelDurationTo.Text))
            {
                MessageBox.Show("Please fill in all the required fields.", "Incomplete Form", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int stopDurationFrom, stopDurationTo, travelDurationFrom, travelDurationTo;

            if (!int.TryParse(txtStopDurationFrom.Text, out stopDurationFrom) || !int.TryParse(txtStopDurationTo.Text, out stopDurationTo) || !int.TryParse(txtTravelDurationFrom.Text, out travelDurationFrom) || !int.TryParse(txtTravelDurationTo.Text, out travelDurationTo))
            {
                MessageBox.Show("Invalid value. Please enter a valid integer.", "Invalid Value", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (stopDurationFrom > stopDurationTo || travelDurationFrom > travelDurationTo || travelDurationFrom < 1 || stopDurationFrom < 0)
            {
                MessageBox.Show("Invalid time range. Please make sure the 'From' value is less than or equal to the 'To' value.", "Invalid Time Range", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            Train newTrain = new Train
            {
                Line = cmbLine.SelectedValue.ToString(),
                InitialStation = int.Parse(cmbInitialStation.SelectedValue.ToString()),
                Direction = cmbDirection.SelectedValue.ToString(),
                StopDurationFrom = int.Parse(txtStopDurationFrom.Text),
                StopDurationTo = int.Parse(txtStopDurationTo.Text),
                TravelDurationFrom = int.Parse(txtTravelDurationFrom.Text),
                TravelDurationTo = int.Parse(txtTravelDurationTo.Text)
            };

            foreach (Train existingTrain in MainWindow.trains)
            {
                if (existingTrain == train)
                    continue;
                if ((existingTrain.Line == newTrain.Line && existingTrain.InitialStation == newTrain.InitialStation && existingTrain.Direction == newTrain.Direction) 
                    || (existingTrain.Direction == "Forward" && newTrain.Direction == "Forward" && existingTrain.InitialStation == 5 && newTrain.InitialStation == 5))
                {
                    MessageBox.Show("Train with similar parameters exist", "Also exist", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }

            train = newTrain;

            if (train.Direction == "Forward" && train.Line == "Red") train.Stations = Station.redFwdStations;
            else if (train.Line == "Red") train.Stations = Station.redBwdStations;
            else if (train.Direction == "Forward") train.Stations = Station.greenFwdStations;
            else train.Stations = Station.greenBwdStations;

            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
