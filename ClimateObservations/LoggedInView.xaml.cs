using ClimateObservations.Models;
using ClimateObservations.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static ClimateObservations.Repositories.ObservationRepository;
using static ClimateObservations.Repositories.CategoryRepository;

namespace ClimateObservations
{
    /// <summary>
    /// Interaction logic for LoggedInView.xaml
    /// </summary>
    public partial class LoggedInView : Window
    {
        public LoggedInView(Observer observer)
        {
            InitializeComponent();
            lblLastUpdated.Content = null;
            lblLastUpdated.Content = $"Senast uppdaterad {DateTime.Now}";
            lblObserver.Content = null;
            lblObserver.Content = $"Välkommen, {observer.Firstname} {observer.Lastname}!";

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void btnAddObservation_Click(object sender, RoutedEventArgs e)
        {

            UpdateUI();
        }

        private void btnUpdateObservation_Click(object sender, RoutedEventArgs e)
        {


            UpdateUI();
        }

        public void UpdateUI()
        {

        }

        private void btnUpdateObservationsView_Click(object sender, RoutedEventArgs e)
        {
            lbxObservations.ItemsSource = null;
            lbxObservations.ItemsSource = GetObservations();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
    }
}
