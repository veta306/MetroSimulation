using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        public static ObservableCollection<MetroObject<Status>> trains;
        bool isSimulationRunning;
        Station transit;
        int forward = 1;
        int backward = 4;
        Random random;
        TextBlock trainStatus;
        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            InitializeListBox();
            InitializeTextBox();
        }

        private void InitializeData()
        {
            random = new Random();
            trains = new ObservableCollection<MetroObject<Status>>();
            transit = new Station(370, 220);
            Station.redFwdStations = new List<Station>
            {
                new Station(215,110),
                new Station(60,110),
                new Station(60,320),
                new Station(215,320),
                transit
            };
            Station.redBwdStations = new List<Station>
            {
                new Station(215, 120),
                new Station(70,120),
                new Station(70,310),
                new Station(215,310),
                new Station(355,220)
            };
            Station.greenFwdStations = new List<Station>
            {
                new Station(515,110),
                new Station(670,110),
                new Station(670,320),
                new Station(515,320),
                transit
            };
            Station.greenBwdStations = new List<Station>
            {
                new Station(515,120),
                new Station(660,120),
                new Station(660,310),
                new Station(515,310),
                new Station(385,220) 
            };
        }

        private void InitializeListBox()
        {
            lstTrains.ItemsSource = trains;
        }
        private void InitializeTextBox()
        {
            trainStatus = new TextBlock()
            {
                FontSize = 14,
                Foreground = Brushes.Black,
                Margin = new Thickness(5),
                Visibility = Visibility.Collapsed
            };
            canvas.Children.Add(trainStatus);
        }

        private void btnAddTrain_Click(object sender, RoutedEventArgs e)
        {
            TrainFormWindow trainForm = new TrainFormWindow();
            if (trainForm.ShowDialog() == true)
            {
                MetroObject<Status> newTrain = trainForm.train;
                trains.Add(newTrain);
            }
        }

        private void btnEditTrain_Click(object sender, RoutedEventArgs e)
        {
            if (lstTrains.SelectedItem != null)
            {
                TrainFormWindow trainForm = new TrainFormWindow((Train)lstTrains.SelectedItem);
                if (trainForm.ShowDialog() == true)
                {
                    int selectedIndex = lstTrains.SelectedIndex;
                    trains[selectedIndex] = trainForm.train;
                }
            }
            else
            {
                MessageBox.Show("Please select a train to edit.");
            }
        }

        private void btnDeleteTrain_Click(object sender, RoutedEventArgs e)
        {
            if (lstTrains.SelectedItem != null)
            {
                trains.Remove((Train)lstTrains.SelectedItem);
            }
            else
            {
                MessageBox.Show("Please select a train to delete.");
            }
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (lstTrains.Items.Count > 0)
            {
                if (!isSimulationRunning)
                {
                    isSimulationRunning = true;
                    btnEditTrain.IsEnabled = false;
                    btnDeleteTrain.IsEnabled = false;
                    btnAddTrain.IsEnabled = false;
                    btnStart.IsEnabled = false;
                    btnStop.IsEnabled = true;
                    ClearStations();
                    OccupyStations();
                    await SimulateMetro();
                }
            }
            else
            {
                MessageBox.Show("Please add a train to start.");
            }
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            isSimulationRunning = false;
        }

        private async Task SimulateMetro()
        {
            List<Task> tasks = new List<Task>();

            foreach (Train train in trains)
            {
                if (!isSimulationRunning) break;

                if (train.Direction == "Forward") tasks.Add(MoveTrain(train, forward));
                else tasks.Add(MoveTrain(train, backward));
            }

            await Task.WhenAll(tasks);

            btnEditTrain.IsEnabled = true;
            btnDeleteTrain.IsEnabled = true;
            btnAddTrain.IsEnabled = true;
            btnStart.IsEnabled = true;
            btnStop.IsEnabled = false;
        }


        private async Task MoveTrain(Train train, int direction)
        {
            train.Model = new Ellipse() { Width = 9, Height = 9, Fill = Brushes.Black };
            Canvas.SetLeft(train.Model, train.Stations[train.InitialStation - 1].X);
            Canvas.SetTop(train.Model, train.Stations[train.InitialStation - 1].Y);
            train.Model.MouseEnter += Train_MouseEnter;
            train.Model.MouseLeave += Train_MouseLeave;
            canvas.Children.Add(train.Model);

            int currentStationIndex = train.InitialStation - 1;

            while (isSimulationRunning)
            {
                int nextStationIndex = (currentStationIndex + direction) % 5;

                while (!train.Stations[nextStationIndex].CheckStatus() && isSimulationRunning)
                {
                    train.ChangeStatus(Status.Waiting);
                    await Task.Delay(100);
                }

                train.Stations[currentStationIndex].ChangeStatus(true);
                train.Stations[nextStationIndex].ChangeStatus(false);


                int stopDuration = random.Next(train.StopDurationFrom, train.StopDurationTo + 1);
                int travelDuration = random.Next(train.TravelDurationFrom, train.TravelDurationTo + 1);

                DoubleAnimation animationX = new DoubleAnimation();
                animationX.From = Canvas.GetLeft(train.Model);
                animationX.To = train.Stations[nextStationIndex].X;
                animationX.Duration = new Duration(TimeSpan.FromSeconds(travelDuration));
                animationX.FillBehavior = FillBehavior.HoldEnd;

                DoubleAnimation animationY = new DoubleAnimation();
                animationY.From = Canvas.GetTop(train.Model);
                animationY.To = train.Stations[nextStationIndex].Y;
                animationY.Duration = new Duration(TimeSpan.FromSeconds(travelDuration));
                animationY.FillBehavior = FillBehavior.HoldEnd;

                Storyboard.SetTarget(animationX, train.Model);
                Storyboard.SetTargetProperty(animationX, new PropertyPath(Canvas.LeftProperty));
                Storyboard.SetTarget(animationY, train.Model);
                Storyboard.SetTargetProperty(animationY, new PropertyPath(Canvas.TopProperty));

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(animationX);
                storyboard.Children.Add(animationY);

                storyboard.Begin();

                train.ChangeStatus( Status.Moving);
                await Task.Delay(travelDuration * 1000);

                train.ChangeStatus(Status.Stoped);
                await Task.Delay(stopDuration * 1000);

                currentStationIndex = nextStationIndex;
            }

            canvas.Children.Remove(train.Model);
        }
        private void Train_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Ellipse trainEllipse)
            {
                Train train = GetTrainByEllipse(trainEllipse);

                if (train != null)
                {
                    Canvas.SetLeft(trainStatus, Canvas.GetLeft(trainEllipse) + 15);
                    Canvas.SetTop(trainStatus, Canvas.GetTop(trainEllipse) - 15);
                    trainStatus.Text = "Status: " + train.CheckStatus();
                    trainStatus.Visibility = Visibility.Visible;
                }
            }
        }

        private void Train_MouseLeave(object sender, MouseEventArgs e)
        {
            trainStatus.Visibility = Visibility.Collapsed;
        }
        private Train GetTrainByEllipse(Ellipse ellipse)
        {
            foreach (Train train in trains)
            {
                if (train.Model == ellipse)
                {
                    return train;
                }
            }
            return null;
        }
        private void ClearStations()
        {
            foreach (Station station in Station.redFwdStations) station.ChangeStatus(true);
            foreach (Station station in Station.redBwdStations) station.ChangeStatus(true);
            foreach (Station station in Station.greenFwdStations) station.ChangeStatus(true);
            foreach (Station station in Station.greenBwdStations) station.ChangeStatus(true);
        }
        private void OccupyStations()
        {
            foreach (Train train in trains)
            {
                train.Stations[train.InitialStation - 1].ChangeStatus(false);
            }
        }
    }
}
