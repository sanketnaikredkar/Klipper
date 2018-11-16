using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Models.Core.Employment;

namespace KlipperApi.DataAccess
{
    public class EmployeeAccessor : IEmployeeAccessor
    {
        static HttpClient _client = new HttpClient() { Timeout = TimeSpan.FromMilliseconds(10000) };
        static string _baseAddress = "https://localhost:6001/";

        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            _client.BaseAddress = new Uri(_baseAddress);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var str = "api/employees/" + employeeId.ToString();
            HttpResponseMessage response = await _client.GetAsync(str);
            return await response.Content.ReadAsAsync<Employee>();
        }
    }
}
