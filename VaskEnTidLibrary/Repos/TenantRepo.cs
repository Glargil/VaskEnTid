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
        #region AddTenant
        public void AddTenant(Tenant tenant)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Tenant (Name, Phone, Email, Address) VALUES (@Name, @Phone, @Email, @Address)", connection);
                command.Parameters.AddWithValue("@TenantID", tenant.TenantID);
                command.Parameters.AddWithValue("@Name", tenant.Name);
                command.Parameters.AddWithValue("@Phone", tenant.Phone);
                command.Parameters.AddWithValue("@Email", tenant.Email);
                command.Parameters.AddWithValue("@Address", tenant.Address);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
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
        #region UpdateTenant
        public void UpdateTenant(Tenant tenant)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Tenant SET Name = @Name, Phone = @Phone, Email = @Email, Address = Address WHERE TenantID @TenantID", connection);
                command.Parameters.AddWithValue("@TenantID", tenant.TenantID);
                command.Parameters.AddWithValue("@Name", tenant.Name);
                command.Parameters.AddWithValue("@Phone", tenant.Phone);
                command.Parameters.AddWithValue("@Email", tenant.Email);
                command.Parameters.AddWithValue("@Address", tenant.Address);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
        #region DeleteTenant
        public void DeleteTenant(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Tenant WHERE TenantID = @TenantID", connection);
                command.Parameters.AddWithValue("@TenantID", id);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
    }
}
