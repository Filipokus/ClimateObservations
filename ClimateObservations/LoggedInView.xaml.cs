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
using static ClimateObservations.Repositories.UnitRepository;

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

            FillCbx();
        }
        private void btnAddObservation_Click(object sender, RoutedEventArgs e)
        {

        }

        public void FillCbx()
        {
            cbxNewCategory.ItemsSource = null;
            cbxNewCategory.ItemsSource = GetCategories();
            cbxUnits.ItemsSource = null;
            cbxUnits.ItemsSource = GetUnits();
            cbxUnitsTwo.ItemsSource = null;
            cbxUnitsTwo.ItemsSource = GetUnits();
        }



        private void btnUpdateObservation_Click(object sender, RoutedEventArgs e)
        {

        }

        public void UpdateUI()
        {

        }

        private void btnUpdateObservationsView_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
    }
}
