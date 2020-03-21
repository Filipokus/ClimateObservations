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

namespace ClimateObservations
{
    /// <summary>
    /// Interaction logic for ManageObservers.xaml
    /// </summary>
    public partial class ManageObservers : Window
    {
        public ManageObservers()
        {
            InitializeComponent();
            cbxObservers.ItemsSource = null;
            cbxObservers.ItemsSource = ObserverRepository.GetObservers();
            lbxObservers.ItemsSource = null;
            lbxObservers.ItemsSource = ObserverRepository.GetObservers();
            lblLastUpdated.Content = $"Senast uppdaterad {DateTime.Now}";
        }
        public void UpdateUI() 
        {
            cbxObservers.ItemsSource = null;
            cbxObservers.ItemsSource = ObserverRepository.GetObservers();
            lbxObservers.ItemsSource = null;
            lbxObservers.ItemsSource = ObserverRepository.GetObservers();
            lblLastUpdated.Content = $"Senast uppdaterad {DateTime.Now}";
        }
        private void BtnAddObserver_Click(object sender, RoutedEventArgs e)
        {
            if (txtFirstname.Text.Length>0 && txtLastname.Text.Length>0)
            {
                string firstname = txtFirstname.Text;
                string lastname = txtLastname.Text;
                int observerId = ObserverRepository.AddObserver(firstname, lastname);
                MessageBox.Show($"{ObserverRepository.GetObserver(observerId)} har lagts till i databasen (med ID {observerId}).");
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Alla observatörer måste ha ett förnamn och ett efternamn");
            }

        }

        private void BtnUpdateObservers_Click(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }

        private void BtnDeleteObserver_Click(object sender, RoutedEventArgs e)
        {
            Observer observer = (Observer)cbxObservers.SelectedItem;
            ObserverRepository.DeleteObserver(observer.Id);
            MessageBox.Show($"{observer} är nu borttagen");
            UpdateUI();
        }
    }
}
