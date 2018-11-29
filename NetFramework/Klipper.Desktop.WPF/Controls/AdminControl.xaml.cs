using Models.Core.Employment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Klipper.Desktop.WPF.Controls
{
    /// <summary>
    /// Interaction logic for AdminControl.xaml
    /// </summary>
    public partial class AdminControl : UserControl
    {
        HttpClient client = new HttpClient();
        
        public AdminControl()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://localhost:6001/");
            IDTextBox.Text = GetMaxEmployeeId();
        }

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Save_EmployeeData(object sender, RoutedEventArgs e)
        {
            Employee employeeData = new Employee()
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                ID = Convert.ToInt32(IDTextBox.Text)
            };

            string jsonData = JsonConvert.SerializeObject(employeeData, Formatting.Indented);
            StringContent httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = client.PostAsync("/api/Employees/", httpContent).Result;
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Employee Information Saved Successfully !","Saved",MessageBoxButton.OK, MessageBoxImage.Information);
                IDTextBox.Text = GetMaxEmployeeId();
            }
            else
            {
                MessageBox.Show($"Error while saving:{response.RequestMessage}", "Error", MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private string GetMaxEmployeeId()
        {
            var response = client.GetAsync("api/Employees/GetMaxEmployeeId").Result;
            return "";
        }
    }
}
