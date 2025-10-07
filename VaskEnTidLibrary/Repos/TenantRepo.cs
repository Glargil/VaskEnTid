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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="VaskEnTidLibrary.Interfaces.ITenantRepo" />
    public class TenantRepo : ITenantRepo
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private string _connectionString;
        /// <summary>
        /// Initializes a new instance of the <see cref="TenantRepo"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public TenantRepo(string connectionString)
        {
            _connectionString = "Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;";
        }

        #region AddTenant
        /// <summary>
        /// Adds the tenant.
        /// </summary>
        /// <param name="tenant">The tenant.</param>
        /// <exception cref="System.Exception">
        /// A database error occurred while adding a new tenant: " + ex.Message
        /// or
        /// An error occurred while adding a new tenant: " + ex.Message
        /// </exception>
        public void AddTenant(Tenant tenant)
        {
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            try
            {
                var command = new SqlCommand("INSERT INTO Tenant (Name, Phone, Email, Address) VALUES (@Name, @Phone, @Email, @Address)", connection);
                command.Parameters.AddWithValue("@Name", tenant.Name);
                command.Parameters.AddWithValue("@Phone", tenant.Phone);
                command.Parameters.AddWithValue("@Email", tenant.Email);
                command.Parameters.AddWithValue("@Address", tenant.Address);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while adding a new tenant: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new tenant: " + ex.Message, ex);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
        #endregion
        #region GetAllTenants
        // Method to get all tenants from the database
        /// <summary>
        /// Gets all tenants.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// A database error occurred while retrieving tenants: " + ex.Message
        /// or
        /// An error occurred while retrieving tenants: " + ex.Message
        /// </exception>
        public List<Models.Tenant> GetAllTenants()
        {
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            var tenants = new List<Models.Tenant>();
            try
            {
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
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while retrieving tenants: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving tenants: " + ex.Message, ex);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            //Debug output to verify data
            //foreach (var tenant in tenants)
            //{
            //    Console.WriteLine(
            //        $"Id: {tenant.TenantID}, Phone: {tenant.Phone}, Email: {tenant.Email}, Address: {tenant.Address}"
            //        );
            //}
            return tenants;
        }
        #endregion
        #region UpdateTenant
        /// <summary>
        /// Updates the tenant.
        /// </summary>
        /// <param name="tenant">The tenant.</param>
        /// <exception cref="System.Exception">
        /// A database error occurred while updating a tenant: " + ex.Message
        /// or
        /// An error occurred while updating a tenant: " + ex.Message
        /// </exception>
        public void UpdateTenant(Tenant tenant)
        {

            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            try
            {
                {
                    var command = new SqlCommand("UPDATE Tenant SET Name = @Name, Phone = @Phone, Email = @Email, Address = Address WHERE TenantID @TenantID", connection);
                    command.Parameters.AddWithValue("@TenantID", tenant.TenantID);
                    command.Parameters.AddWithValue("@Name", tenant.Name);
                    command.Parameters.AddWithValue("@Phone", tenant.Phone);
                    command.Parameters.AddWithValue("@Email", tenant.Email);
                    command.Parameters.AddWithValue("@Address", tenant.Address);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while updating a tenant: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating a tenant: " + ex.Message, ex);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
        #endregion
        #region DeleteTenant
        /// <summary>
        /// Deletes the tenant.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.Exception">
        /// A database error occurred while deleting a tenant: " + ex.Message
        /// or
        /// An error occurred while deleting a tenant: " + ex.Message
        /// </exception>
        public void DeleteTenant(int id)
        {
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            try
            {
                {
                    var command = new SqlCommand("DELETE FROM Tenant WHERE TenantID = @TenantID", connection);
                    command.Parameters.AddWithValue("@TenantID", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while deleting a tenant: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting a tenant: " + ex.Message, ex);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
        #endregion
    }
}
