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
        Observation selectedobservation;
        Measurement selectedmeasurement;
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
            UpdateUI();
        }
        private void BtnAddObservation_Click(object sender, RoutedEventArgs e)
        {
            if (cbxSubCategory.SelectedItem != null)
            {
                Category selectedcategory = (Category)cbxSubCategory.SelectedItem;
                int category_id = selectedcategory.Id;
                Area area = (Area)cbxLocation.SelectedItem;
                int geolocation_id = AddGeolocation(area.Id);
                double value;
                if (double.TryParse(txtNewMeasurement.Text, out value))
                {
                    AddMeasurement(selectedobserver.Id, geolocation_id, value, category_id);
                    UpdateUI();
                }
                else
                {
                    MessageBox.Show("Inmatningen ska vara ett heltal eller decimaltal. Tänk på att decimaltal skrivs med kommatecken (,)");
                }
            }
            else
            {
                Category selectedcategory = (Category)cbxNewCategory.SelectedItem;
                int category_id = selectedcategory.Id;
                Area area = (Area)cbxLocation.SelectedItem;
                int geolocation_id = AddGeolocation(area.Id);
                double value;
                if (double.TryParse(txtNewMeasurement.Text, out value))
                {
                    AddMeasurement(selectedobserver.Id, geolocation_id, value, category_id);
                    UpdateUI();
                }
                else
                {
                    MessageBox.Show("Inmatningen ska vara ett heltal eller decimaltal. Tänk på att decimaltal skrivs med kommatecken (,)");
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
        public void UpdateUI()
        {
            lblCurrValue.Visibility = Visibility.Hidden;
            lblCurrValueTxt.Visibility = Visibility.Hidden;
            lblNewValueTxt.Visibility = Visibility.Hidden;
            txtNewValue.Visibility = Visibility.Hidden;
            lblMsrmtTxt.Visibility = Visibility.Hidden;
            lblDateTxt.Visibility = Visibility.Hidden;
            lblAreaTxt.Visibility = Visibility.Hidden;
            cbxMeasurements.Visibility = Visibility.Hidden;
            btnShowMsrmt.Visibility = Visibility.Hidden;
            cbxChildCategory.ItemsSource = null;
            cbxChildCategory.Visibility = Visibility.Hidden;
            lblChildCategory.Visibility = Visibility.Hidden;
            lblCurrChildCategory.Visibility = Visibility.Hidden;
            lblCurrChildCategory.Content = null;
            txtNewValue.Text = "";
            cbxNewCategory.SelectedIndex = 0;
            cbxSubCategory.SelectedIndex = 0;
            cbxLocation.SelectedIndex = 0;
            lbxObservations.ItemsSource = null;
            lbxObservations.ItemsSource = GetObservationsWithDetails(selectedobserver.Id);
            lblLastUpdated.Content = null;
            lblLastUpdated.Content = $"Senast uppdaterad {DateTime.Now}";
        }
        private void BtnUpdateObservationsView_Click(object sender, RoutedEventArgs e)
        {
        UpdateUI();
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
            if (cbxMeasurements.SelectedIndex != -1 && selectedobservation != null)
            {
                lblCurrValue.Visibility = Visibility.Visible;
                lblCurrValueTxt.Visibility = Visibility.Visible;
                lblNewValueTxt.Visibility = Visibility.Visible;
                txtNewValue.Visibility = Visibility.Visible;
                int i = cbxMeasurements.SelectedIndex;
                selectedmeasurement = selectedobservation.Measurements[i];
                lblCurrValue.Content = selectedmeasurement.Value;
                if (cbxMeasurements.SelectedItem.ToString() == "Fjällripa")
                {
                    cbxChildCategory.ItemsSource = null;
                    cbxChildCategory.ItemsSource = GetChildCategories(5);
                    lblChildCategory.Visibility = Visibility.Visible;
                    cbxChildCategory.Visibility = Visibility.Visible;
                    lblCurrChildCategory.Visibility = Visibility.Visible;
                }
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
                selectedobservation = (Observation)lbxObservations.Items[lbxObservations.SelectedIndex];
                lblDate.Content = selectedobservation.Date;
                lblArea.Content = selectedobservation.Areas[0].Name;
                List<Category> categories = selectedobservation.Categories;
                foreach (var c in categories)
                {
                    if (c.BaseCategoryId==5)
                    {
                        string currentchildcategory = c.ToString();
                        lblCurrChildCategory.Content = currentchildcategory;
                        c.Name = "Fjällripa";
                    }
                }
                cbxMeasurements.ItemsSource = categories;
            }
            else
            {
                MessageBox.Show("Du måste välja en observation!");
            }
        }
        private void BtnSaveObservationUpdate_Click(object sender, RoutedEventArgs e)
        {
            string measurementValue = txtNewValue.Text;
            double value;
            if (double.TryParse(measurementValue, out value))
            {
                if (double.TryParse(measurementValue, out value) && cbxChildCategory.Visibility == Visibility.Visible)
                {
                    int id = selectedmeasurement.Id;
                    Category childcategory = (Category)cbxChildCategory.SelectedItem;
                    int category_id = childcategory.Id;
                    UpdateObservation(id, value, category_id);
                    MessageBox.Show($"Mätpunken lyckades ändras till {value} och {childcategory.Name}!");
                    UpdateUI();
                }
                else
                {
                    int id = selectedmeasurement.Id;
                    UpdateObservation(id, value);
                    MessageBox.Show($"Mätpunken lyckades ändras till {value}!");
                    UpdateUI();
                }
            }
            else
            {
                if (cbxChildCategory.Visibility == Visibility.Visible)
                {
                    int id = selectedmeasurement.Id;
                    Category childcategory = (Category)cbxChildCategory.SelectedItem;
                    int category_id = childcategory.Id;
                    UpdateObservation(id, category_id);
                    MessageBox.Show($"Mätpunken lyckades ändras till {childcategory.Name}!");
                    UpdateUI();
                }
                else
                {
                    MessageBox.Show("Fyll i ett heltal eller ett decimaltal. Tänk på att decimaltal skrivs med ett kommatecken (,)");
                }
            }
        }
    }
}
