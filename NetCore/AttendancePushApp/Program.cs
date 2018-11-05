using ERPCore.Models.Employment;
using ERPCore.Models.HR.Attendance;
using ERPCore.Models.Operationals;
using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AttendancePushApp
{
    class Program
    {
        #region Fields

        static HttpClient _client = new HttpClient();
        static List<Employee> _employees = new List<Employee>();
        static List<AccessEvent> _accessEvents = new List<AccessEvent>();
        static List<AccessPoint> _accessPoints = new List<AccessPoint>();

        #endregion

        static void Main(string[] args)
        {
            //_client.BaseAddress = new Uri("https://localhost:5001/");

            LoadJsonData();

            var token = GetTokenAsync().Result; //Get and set Authentication bearer token
            _client.SetBearerToken(token);

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                foreach (var o in _employees)
                {
                    var res = Add(o);
                }
                foreach (var o in _accessPoints)
                {
                    var res = Add(o);
                }
                foreach (var o in _accessEvents)
                {
                    var res = Add(o);
                }
            }
            catch(Exception exp)
            {
                Console.Write(exp.Message);
            }
        }

        #region Authentication

        private static async Task<string> GetTokenAsync()
        {
            var discoveryResponse = await DiscoveryClient.GetAsync("https://localhost:5001/");
            if(discoveryResponse.IsError)
            {
                Console.WriteLine("Error pushing attendance data to AttendaceApi: {0}", discoveryResponse.Error);
                return null;
            }

            var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, "AttendancePushApp", "Adpa@98765");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync();
            if(tokenResponse.IsError)
            {
                Console.WriteLine("Error pushing attendance data to AttendaceApi -> Token end point error: {0}", tokenResponse.Error);
                return null;
            }
            return tokenResponse.AccessToken;
        }

        #endregion

        #region Loading and pushing attendance data

        private static void LoadJsonData()
        {
            var employeesStr = System.IO.File.ReadAllText(@"C:\Temp\Attendance\Employees.txt");
            _employees = JsonConvert.DeserializeObject<List<Employee>>(employeesStr);

            var accessEventsStr = System.IO.File.ReadAllText(@"C:\Temp\Attendance\AccessEvents.txt");
            _accessEvents = JsonConvert.DeserializeObject<List<AccessEvent>>(accessEventsStr);

            var accessPointsStr = System.IO.File.ReadAllText(@"C:\Temp\Attendance\AccessPoints.txt");
            _accessPoints = JsonConvert.DeserializeObject<List<AccessPoint>>(accessPointsStr);
        }

        static async Task<Uri> Add(Employee employee)
        {
            var json = JsonConvert.SerializeObject(employee, Formatting.Indented);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("api/Employees", httpContent);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location; // return URI of the created resource.
        }

        static async Task<Uri> Add(AccessEvent obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("api/AccessEvents", httpContent);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location; // return URI of the created resource.
        }

        static async Task<Uri> Add(AccessPoint obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("api/AccessPoints", httpContent);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location; // return URI of the created resource.
        }

        static async Task<Uri> Add(Department obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("api/Departments", httpContent);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location; // return URI of the created resource.
        }

        #endregion
    }
}
