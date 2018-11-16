using Models.Core.HR.Attendance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for AttendanceControl.xaml
    /// </summary>
    public partial class AttendanceControl : UserControl
    {
        public AttendanceControl()
        {
            InitializeComponent();
        }

        private void GetAttendanceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var startDate = StartDatePicker.SelectedDate;
                var endDate = EndDatePicker.SelectedDate;
                int employeeId = int.Parse(EmployeeIdTextbox.Text);
                var accessEvents = GetAccessEvents(startDate, endDate, employeeId).Result;
                if(accessEvents.Count > 0)
                {
                    var json = JsonConvert.SerializeObject(accessEvents, Formatting.Indented);
                    var filename = "C:/Temp/accessEvents.json";
                    File.WriteAllText(filename, json);
                    Process.Start("notepad.exe", filename);
                }
                else
                {
                    MessageBox.Show("No records found!");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private async Task<List<AccessEvent>> GetAccessEvents(DateTime? startDate, DateTime? endDate, int employeeId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:7000/");
            client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", Auth.SessionToken);

            var x = new
            {
                id = employeeId,
                startdate = startDate,
                enddate = endDate
            };

            var json = JsonConvert.SerializeObject(x, Formatting.Indented);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("api/attendance/", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var accessEvents = JsonConvert.DeserializeObject<List<AccessEvent>>(jsonString);
                return accessEvents;
            }
            else
            {
                MessageBox.Show(response.Content.ReadAsStringAsync().Result);
                return new List<AccessEvent>();
            }
        }
    }
}
