using ClimateObservations.Models;
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
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }
    }
}
