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
        Observer selectedobserver;
        public LoggedInView(Observer observer)
        {
            InitializeComponent();
            lblObserver.Content = null;
            lblObserver.Content = $"Välkommen, {observer.Firstname} {observer.Lastname}!";
            lbxObservations.ItemsSource = null;
            lbxObservations.ItemsSource = GetObservationsWithDetails(observer.Id);
            lblLastUpdated.Content = null;
            lblLastUpdated.Content = $"Senast uppdaterad {DateTime.Now}";
            selectedobserver = observer;
            FillCbx();
        }
        private void BtnAddObservation_Click(object sender, RoutedEventArgs e)
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



        private void BtnUpdateObservation_Click(object sender, RoutedEventArgs e)
        {

        }

        public void UpdateUI()
        {

        }

        private void BtnUpdateObservationsView_Click(object sender, RoutedEventArgs e)
        {
            lbxObservations.ItemsSource = null;
            lbxObservations.ItemsSource = GetObservationsWithDetails(selectedobserver.Id);
            lblLastUpdated.Content = null;
            lblLastUpdated.Content = $"Senast uppdaterad {DateTime.Now}";
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
    }
}
