using IdentityServer4.Models;
using System.Collections.Generic;

namespace AuthServer.Configuration
{
    public class AuthConfig
    {
        public static IEnumerable<ApiResource> GetApiResources() //Scopes
        {
            return new List<ApiResource>
            {
                new ApiResource("AttendanceApi", "Attendace Api"),
                new ApiResource("EmployeeApi", "Employee Data Management Api"),
                new ApiResource("KlipperApi", "Klingelnberg India Platform for Personnel Enterprise Resources Api"),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "AttendancePushApp",
                    ClientName = "Attendance Data Pusher Application",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("Adpa@98765".Sha256()) },
                    AllowedScopes =
                    {
                        "AttendanceApi"
                    }
                },
                new Client
                {
                    ClientId = "KlipperApi",
                    ClientName = "Klingelnberg India Platform for Personnel Enterprise Resources Api",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("Klipperapi@12345".Sha256()) },
                    AllowedScopes =
                    {
                        "AttendanceApi",
                        "EmployeeApi"
                    }
                },
                new Client
                {
                    ClientId = "KlipperApplication",
                    ClientName = "Klingelnberg India Platform for Personnel Enterprise Resources Application",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("Klipperapp@12345".Sha256()) },
                    AllowedScopes =
                    {
                        "KlipperApi"
                    }
                },
                new Client
                {
                    ClientId = "Klipper.Desktop.CommandLine",
                    ClientName = "Klingelnberg India Platform for Personnel Enterprise Resources Application",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("Klipperapp@12345".Sha256()) },
                    AllowedScopes =
                    {
                        "KlipperApi",
                        "AttendanceApi",
                        "EmployeeApi"
                    }
                }

            };
        }
    }
}
