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
            lblLastUpdated.Content = $"Senast uppdaterad {DateTime.Now}";
        }
    }
}
