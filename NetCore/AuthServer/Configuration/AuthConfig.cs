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
                new ApiResource("AttendaceDBAccessApi", "Attendace Database Access Api"),
                new ApiResource("KlipperApi", "Klingelnberg India Platform for Personnel Enterprise Resources Api"),
                new ApiResource("LeaveManagementApi", "Leave Management Database Access Api"),
                new ApiResource("HRMDBAccessAPi", "Human Resource Management Database Access Api"),
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
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
                        "AttendaceDBAccessApi"
                    }
                },
                new Client
                {
                    ClientId = "LeaveManagementApi",
                    ClientName = "Leave Management Database Access Api",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("LmdaApi@12345".Sha256()) },
                    AllowedScopes =
                    {
                        "AttendaceDBAccessApi"
                    }
                },
                new Client
                {
                    ClientId = "KlipperApi",
                    ClientName = "Klingelnberg India Platform for Personnel Enterprise Resources Api",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("KlipperApi@12345".Sha256()) },
                    AllowedScopes =
                    {
                        "AttendaceDBAccessApi",
                        "AuthDBAccessApi",
                        "LeaveManagementApi",
                        "HRMDBAccessAPi"
                    }
                },
                new Client
                {
                    ClientId = "KlipperApplication",
                    ClientName = "Klingelnberg India Platform for Personnel Enterprise Resources Application",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("KlipperApp@12345".Sha256()) },
                    AllowedScopes =
                    {
                        "KlipperApi"
                    }
                },
            };
        }
    }
}
