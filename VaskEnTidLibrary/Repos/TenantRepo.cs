using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLibrary.Models;
using Microsoft.Data.SqlClient;
using VaskEnTidLibrary.Interfaces;

namespace VaskEnTidLibrary.Repos
{
    public class TenantRepo : ITenantRepo
    {
        private string _connectionString;
        public TenantRepo(string connectionString)
        {
            _connectionString = "Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;";
        }

        #region GetAllTenants
        // Method to get all tenants from the database
        public List<Models.Tenant> GetAllTenants()
        {
            var tenants = new List<Models.Tenant>();
            using (var connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionString))
            {
                var command = new Microsoft.Data.SqlClient.SqlCommand("SELECT TenantID, Phone, Email, Address FROM Tenant", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var tenant = new Models.Tenant
                        {
                            TenantID = (int)reader["TenantID"],
                            Phone = (string)reader["Phone"],
                            Email = (string)reader["Email"],
                            Address = (string)reader["Address"]
                        };
                        tenants.Add(tenant); // <-- Add to list
                    }
                }
                connection.Close();
            }
            //Debug output to verify data
            foreach (var tenant in tenants)
            {
                Console.WriteLine(
                    $"Id: {tenant.TenantID}, Phone: {tenant.Phone}, Email: {tenant.Email}, Address: {tenant.Address}"
                    );
            }
            return tenants;
        }
        #endregion
    }
}
