using ClimateObservations.Models;
using ClimateObservations.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClimateObservations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbxObservers.ItemsSource = null;
            cbxObservers.ItemsSource = ObserverRepository.GetObservers();
        }
        private void BtnManageObservers_Click(object sender, RoutedEventArgs e) 
        {
            ManageObservers objManageObservers = new ManageObservers();
            this.Visibility = Visibility.Hidden;
            objManageObservers.Show();
        }

        private void BtnLogUserIn_Click(object sender, RoutedEventArgs e)
        {
            if (cbxObservers.SelectedItem!=null)
            {
                Observer observer = new Observer();
                observer = (Observer)cbxObservers.SelectedItem;
                LoggedInView objLoggedInView = new LoggedInView(observer);
                this.Visibility = Visibility.Hidden;
                objLoggedInView.Show();
            }
            else
            {
                MessageBox.Show("Välj vilken observatör du vill logga in på först!");
            }
        }
    }
}
