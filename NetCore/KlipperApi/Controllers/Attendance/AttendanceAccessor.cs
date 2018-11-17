using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Models.Core.HR.Attendance;
using Newtonsoft.Json;

namespace KlipperApi.Controllers.Attendance
{
    public class AttendanceAccessor : IAttendanceAccessor
    {
        static HttpClient _client = new HttpClient() { Timeout = TimeSpan.FromMilliseconds(10000) };
        static string _baseAddress = "http://localhost:5000/";

        public async Task<IEnumerable<AccessEvent>> GetAttendanceByEmployeeIdAsync(int employeeId)
        {
            _client.BaseAddress = new Uri(_baseAddress);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var str = "api/accessevents/byEmployeeId?employeeId=" + employeeId.ToString();
            HttpResponseMessage response = await _client.GetAsync(str);
            var jsonString = await response.Content.ReadAsStringAsync();
            var accessEvents = JsonConvert.DeserializeObject<List<AccessEvent>>(jsonString);

            return accessEvents;
        }
    }
}
