using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
using Microsoft.Win32;

namespace VT24A6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // AddressInfo record 
        public record AddressInfo
        {
            public string Name { get; init; }
            public string Street { get; init; }
            public string ZipCode { get; init; }
            public string City { get; init; }
            public string Country { get; init; }
            public string PhoneNumber { get; init; }
            public string HomePageURL { get; init; }

            public AddressInfo(string name, string street, string zipCode, string city, string country, string phoneNumber, string homePageURL)
            {
                Name = name;
                Street = street;
                ZipCode = zipCode;
                City = city;
                Country = country;
                PhoneNumber = phoneNumber;
                HomePageURL = homePageURL;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DiscountTxtBox.TextChanged += DiscountTxtBox_TextChanged;

        }

        private void DiscountTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateTotalPrice();
        }

        private void UpdateInvoiceDate_Click(object sender, RoutedEventArgs e)
        {
            if (InvoiceListBox.SelectedItem != null)
            {
                var selectedItem = (KeyValuePair<string, string>)InvoiceListBox.SelectedItem;

                if (selectedItem.Key == "Invoice Date:")
                {
                    selectedItem = new KeyValuePair<string, string>("Invoice Date:", InvoiceDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd"));
                }
                else if (selectedItem.Key == "Due Date:")
                {
                    selectedItem = new KeyValuePair<string, string>("Due Date:", DueDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd"));
                }

                InvoiceListBox.Items[InvoiceListBox.SelectedIndex] = selectedItem;
            }
        }


        private void InvoiceDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InvoiceListBox.Items.Count > 0)
            {
                var selectedItem = (KeyValuePair<string, string>)InvoiceListBox.Items[1]; 
                selectedItem = new KeyValuePair<string, string>("Invoice Date:", InvoiceDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd"));
                InvoiceListBox.Items[1] = selectedItem;
            }
        }

        private void DueDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InvoiceListBox.Items.Count > 0)
            {
                var selectedItem = (KeyValuePair<string, string>)InvoiceListBox.Items[2]; 
                selectedItem = new KeyValuePair<string, string>("Due Date:", DueDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd"));
                InvoiceListBox.Items[2] = selectedItem;
            }
        }

        private void CalculateTotalPrice()
        {
            double totalPrice = 0;

            for (int i = 1; i < AdditionalDetailsListBox.Items.Count; i += 4)
            {
                string quantityStr = ((KeyValuePair<string, string>)AdditionalDetailsListBox.Items[i]).Value.Replace("Quantity:", "").Trim();
                string unitPriceStr = ((KeyValuePair<string, string>)AdditionalDetailsListBox.Items[i + 1]).Value.Replace("Unit price:", "").Replace("$", "").Trim();
                string taxPercentageStr = ((KeyValuePair<string, string>)AdditionalDetailsListBox.Items[i + 2]).Value.Replace("Tax in %:", "").Trim();

                if (double.TryParse(quantityStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double quantity) &&
                    double.TryParse(unitPriceStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double unitPrice) &&
                    double.TryParse(taxPercentageStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double taxPercentage))
                {
                    double itemTotal = quantity * unitPrice * (1 + taxPercentage / 100);
                    totalPrice += itemTotal;
                }
                else
                {
                    Debug.WriteLine("Error parsing values for item " + (i / 4 + 1));
                }
            }

            if (double.TryParse(DiscountTxtBox.Text, out double discountPercentage))
            {
                double discountAmount = totalPrice * (discountPercentage / 100);

                totalPrice -= discountAmount;
            }

            TotalLbl.Content = "Total: " + totalPrice.ToString("C2");
        }


        private void OpenInvoice_Click(object sender, RoutedEventArgs e)
        {

            InvoiceListBox.Items.Clear();


            InvoiceListBox.Visibility = Visibility.Visible;
            CompanyListBox.Visibility = Visibility.Visible;
            AdditionalDetailsListBox.Visibility = Visibility.Visible;
            SenderListBox.Visibility = Visibility.Visible;
            DiscountTxtBox.Visibility = Visibility.Visible;
            TotalLbl.Visibility = Visibility.Visible;
            InvoiceDatePicker.Visibility = Visibility.Visible;
            DueDatePicker.Visibility = Visibility.Visible;
            InvoiceDatelbl.Visibility = Visibility.Visible;
            DueDatelbl.Visibility = Visibility.Visible;
            InvoiceDatePicker.Visibility = Visibility.Visible;
            DueDatePicker.Visibility = Visibility.Visible;
            TotalLbl.Visibility = Visibility.Visible;
            DiscountLbl.Visibility = Visibility.Visible;

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Select an invoice file";

                // Show OpenFileDialog
                if (openFileDialog.ShowDialog() == true)
                {
                    string[] invoiceDetails = File.ReadAllLines(openFileDialog.FileName);

                    List<KeyValuePair<string, string>> invoiceData = new List<KeyValuePair<string, string>>();

                    InvoiceDatePicker.SelectedDate = DateTime.ParseExact(invoiceDetails[1], "yyyy-MM-dd", null);
                    DueDatePicker.SelectedDate = DateTime.ParseExact(invoiceDetails[2], "yyyy-MM-dd", null);

                    invoiceData.Add(new KeyValuePair<string, string>("Invoice Number:", invoiceDetails[0]));

                    InvoiceListBox.ItemsSource = invoiceData;

                    List<KeyValuePair<string, string>> companyData = new List<KeyValuePair<string, string>>();

                    companyData.Add(new KeyValuePair<string, string>("Company:", invoiceDetails[3]));
                    companyData.Add(new KeyValuePair<string, string>("Street:", invoiceDetails[5]));
                    companyData.Add(new KeyValuePair<string, string>("Zip code:", invoiceDetails[6]));
                    companyData.Add(new KeyValuePair<string, string>("City:", invoiceDetails[7]));
                    companyData.Add(new KeyValuePair<string, string>("Country:", invoiceDetails[8]));

                    CompanyListBox.ItemsSource = companyData;

                    List<AddressInfo> senderData = new List<AddressInfo>();

                    AddressInfo senderInfo = new AddressInfo(
                        name: invoiceDetails[18],
                        street: invoiceDetails[19],
                        zipCode: invoiceDetails[20],
                        city: invoiceDetails[21],
                        country: invoiceDetails[22],
                        phoneNumber: invoiceDetails[23],
                        homePageURL: invoiceDetails[24]
                    );
                    senderData.Add(senderInfo);

                    SenderListBox.ItemsSource = senderData;

                    List<KeyValuePair<string, string>> additionalData = new List<KeyValuePair<string, string>>();

                    additionalData.Add(new KeyValuePair<string, string>("Description:", invoiceDetails[10]));
                    additionalData.Add(new KeyValuePair<string, string>("Quantity:", invoiceDetails[11]));
                    additionalData.Add(new KeyValuePair<string, string>("Unit price:", invoiceDetails[12]));
                    additionalData.Add(new KeyValuePair<string, string>("Tax in %:", invoiceDetails[13]));

                    additionalData.Add(new KeyValuePair<string, string>("Description:", invoiceDetails[14]));
                    additionalData.Add(new KeyValuePair<string, string>("Quantity:", invoiceDetails[15]));
                    additionalData.Add(new KeyValuePair<string, string>("Unit price:", invoiceDetails[16]));
                    additionalData.Add(new KeyValuePair<string, string>("Tax in %:", invoiceDetails[17]));


                    AdditionalDetailsListBox.ItemsSource = additionalData;

                    CalculateTotalPrice();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice: " + ex.Message);
            }
        }
    }
}