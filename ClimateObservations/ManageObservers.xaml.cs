using ClimateObservations.Models;
using ClimateObservations.Repositories;
using Npgsql;
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
            if (observer != null)
            {
                try
                {
                    ObserverRepository.DeleteObserver(observer.Id);
                    MessageBox.Show($"{observer} är nu borttagen");
                }
                catch (PostgresException ex)
                {
                    string message;
                    int sqlstate = int.Parse(ex.SqlState);
                    if (sqlstate == 23503)
                    {
                        message = $"{observer} kan inte tas bort från databasen då hen har registrerat en eller flera klimatobservationer. Vill du ta bort {observer} och hens observationer permanent?";
                        string title = $"Felkod {sqlstate}";
                        MessageBoxButton buttons = MessageBoxButton.YesNo;
                        var result = MessageBox.Show(message, title, buttons);
                        if (result == MessageBoxResult.Yes)
                        {
                            //Ta bort measurements
                        }
                        else
                        {
                            //Inget händer
                        }
                        
                    }
                    else
                    {
                        message = ex.MessageText;
                        MessageBox.Show(message);
                    }
                }
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Välj en observatör först");
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
    }
}
