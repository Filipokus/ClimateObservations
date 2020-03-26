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
using static ClimateObservations.Repositories.MeasurementRepository;
using static ClimateObservations.Repositories.CategoryRepository;
using static ClimateObservations.Repositories.UnitRepository;
using static ClimateObservations.Repositories.GeolocationRepository;
using static ClimateObservations.Repositories.AreaRepository;

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

            lblCurrValue.Visibility = Visibility.Hidden;
            lblCurrValueTxt.Visibility = Visibility.Hidden;
            lblNewValueTxt.Visibility = Visibility.Hidden;
            lblNewValue.Visibility = Visibility.Hidden;
            lblMsrmtTxt.Visibility = Visibility.Hidden;
            lblDateTxt.Visibility = Visibility.Hidden;
            lblAreaTxt.Visibility = Visibility.Hidden;
            cbxMeasurements.Visibility = Visibility.Hidden;
            btnShowMsrmt.Visibility = Visibility.Hidden;

            ChangeObservation();
            FillCbx();
            UpdateUI();
        }
        private void BtnAddObservation_Click(object sender, RoutedEventArgs e)
        {
            if (cbxSubCategory.SelectedItem != null)
            {
                Category selectedcategory = (Category)cbxNewCategory.SelectedItem;
                int category_id = selectedcategory.Id;
                Area area = (Area)cbxLocation.SelectedItem;
                int geolocation_id = AddGeolocation(area.Id);
                int value;
                if (int.TryParse(txtNewMeasurement.Text, out value))
                {
                    AddMeasurement(selectedobserver.Id, geolocation_id, value, category_id);
                }
                else
                {
                    MessageBox.Show("Inmatningen ska vara ett heltal.");
                }
            }
            else
            {
                Category selectedcategory = (Category)cbxSubCategory.SelectedItem;
                int category_id = selectedcategory.Id;
                Area area = (Area)cbxLocation.SelectedItem;
                int geolocation_id = AddGeolocation(area.Id);
                int value;
                if (int.TryParse(txtNewMeasurement.Text, out value))
                {
                    AddMeasurement(selectedobserver.Id, geolocation_id, value, category_id);
                }
                else
                {
                    MessageBox.Show("Inmatningen ska vara ett heltal.");
                }
            }
        }
        public void FillCbx()
        {
            cbxNewCategory.ItemsSource = null;
            cbxNewCategory.ItemsSource = GetParentCategories();
            cbxLocation.ItemsSource = null;
            cbxLocation.ItemsSource = GetAreas();
        }
        public void ChangeObservation()
        {

        }
        public void UpdateUI()
        {
            cbxNewCategory.SelectedIndex = 0;
            cbxSubCategory.SelectedIndex = 0;
            cbxLocation.SelectedIndex = 0;
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
        private void cbxNewCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category selectedcategory = (Category)cbxNewCategory.SelectedItem;
            lblUnit.Content = null;
            lblUnit.Content = GetRelevantUnit(selectedcategory.Id);
            cbxSubCategory.ItemsSource = null;
            cbxSubCategory.ItemsSource = GetChildCategories(selectedcategory.Id);
            cbxSubCategory.SelectedIndex = 0;
        }
        private void BtnShowMsrmt_Click(object sender, RoutedEventArgs e)
        {
            if (cbxMeasurements.SelectedIndex != -1)
            {
                lblCurrValue.Visibility = Visibility.Visible;
                lblCurrValueTxt.Visibility = Visibility.Visible;
                lblNewValueTxt.Visibility = Visibility.Visible;
                lblNewValue.Visibility = Visibility.Visible;

                Observation observation = (Observation)lbxObservations.Items[lbxObservations.SelectedIndex];
                lblCurrValue.Content = observation.Measurements[0].Value;
            }
        }
        private void BtnUpdateObservation_Click(object sender, RoutedEventArgs e)
        {
            if (lbxObservations.SelectedIndex != -1)
            {
                lblMsrmtTxt.Visibility = Visibility.Visible;
                lblDateTxt.Visibility = Visibility.Visible;
                lblAreaTxt.Visibility = Visibility.Visible;
                cbxMeasurements.Visibility = Visibility.Visible;
                btnShowMsrmt.Visibility = Visibility.Visible;

                Observation observation = (Observation)lbxObservations.Items[lbxObservations.SelectedIndex];
                lblDate.Content = observation.Date;
                lblArea.Content = observation.Areas[0].Name;
                cbxMeasurements.ItemsSource = observation.Categories;
            }
            else
            {
                MessageBox.Show("Du måste välja en observation!");
            }
        }
        private void BtnSaveObservationUpdate_Click(object sender, RoutedEventArgs e)
        {
            string textBoxValue = lblNewValue.Text;
        }

    }
}
