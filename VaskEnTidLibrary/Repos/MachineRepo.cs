using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLibrary.Models.Machines;
using Microsoft.Data.SqlClient;
using VaskEnTidLibrary.Interfaces;

namespace VaskEnTidLibrary.Repos
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="VaskEnTidLibrary.Interfaces.IMachineRepo" />
    public class MachineRepo : IMachineRepo
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private string _connectionString;
        /// <summary>
        /// Initializes a new instance of the <see cref="MachineRepo"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MachineRepo(string connectionString)
        {
            _connectionString = "Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;";
        }
        #region GetAllMachines
        /// <summary>
        /// Gets all machines.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Unknown machine type: {typeString}
        /// or
        /// A database error occurred while retrieving all machines: " + ex.Message
        /// or
        /// An error occurred while retrieving all machines: " + ex.Message
        /// </exception>
        public List<Machine> GetAllMachines()
        {
            var machines = new List<Machine>();
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            try
            {

                {
                    var command = new SqlCommand("SELECT MachineID, Type FROM Machine", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Machine machine;
                            var typeString = reader["Type"].ToString();
                            var machineType = Enum.Parse<Machine.MachineType>(typeString);

                            switch (machineType)
                            {
                                case Machine.MachineType.Dryer:
                                    machine = new Dryer
                                    {
                                        MachineID = (int)reader["MachineID"],
                                        Type = machineType
                                    };
                                    break;
                                case Machine.MachineType.Roller:
                                    machine = new Roller
                                    {
                                        MachineID = (int)reader["MachineID"],
                                        Type = machineType
                                    };
                                    break;
                                case Machine.MachineType.Washer:
                                    machine = new Washer
                                    {
                                        MachineID = (int)reader["MachineID"],
                                        Type = machineType
                                    };
                                    break;
                                default:
                                    throw new Exception($"Unknown machine type: {typeString}");
                            }
                            machines.Add(machine); // <-- Add to list
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while retrieving all machines: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all machines: " + ex.Message, ex);
            }
            finally
            {
                if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            // Debug output to verify data
            //foreach (var machine in machines)
            //{
            //    Console.WriteLine($"Id: {machine.MachineID}, Setup: {machine.Type}");
            //}

            return machines;
        }
        #endregion

        #region GetMachineById
        /// <summary>
        /// Gets the machine by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">
        /// Unknown machine type: {typeString}
        /// or
        /// Machine with ID {id} not found.
        /// or
        /// A database error occurred while retrieving machine with ID {id}: {ex.Message}
        /// or
        /// An error occurred while retrieving machine with ID {id}: {ex.Message}
        /// </exception>
        public Machine GetMachineById(int id)
        {
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            Machine machine = null;
            try
            {
                {
                    var command = new SqlCommand("SELECT MachineID, Type FROM Machine WHERE MachineID = @MachineID", connection);
                    command.Parameters.AddWithValue("@MachineID", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var typeString = reader["Type"].ToString();
                            var machineType = Enum.Parse<Machine.MachineType>(typeString);
                            switch (machineType)
                            {
                                case Machine.MachineType.Dryer:
                                    machine = new Dryer
                                    {
                                        MachineID = (int)reader["MachineID"],
                                        Type = machineType
                                    };
                                    break;
                                case Machine.MachineType.Roller:
                                    machine = new Roller
                                    {
                                        MachineID = (int)reader["MachineID"],
                                        Type = machineType
                                    };
                                    break;
                                case Machine.MachineType.Washer:
                                    machine = new Washer
                                    {
                                        MachineID = (int)reader["MachineID"],
                                        Type = machineType
                                    };
                                    break;
                                default:
                                    throw new Exception($"Unknown machine type: {typeString}");
                            }
                        }
                    }
                    if (machine == null)
                    {
                        throw new Exception($"Machine with ID {id} not found.");
                    }
                    return machine;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"A database error occurred while retrieving machine with ID {id}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving machine with ID {id}: {ex.Message}", ex);
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
        #region AddMachine
        /// <summary>
        /// Adds the machine.
        /// </summary>
        /// <param name="machine">The machine.</param>
        /// <exception cref="System.Exception">
        /// A database error occurred while adding a new machine: " + ex.Message
        /// or
        /// An error occurred while adding a new machine: " + ex.Message
        /// </exception>
        public void AddMachine(Machine machine)
        {
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            try
            {
                {
                    var command = new SqlCommand("INSERT INTO Machine (Type) VALUES (@Type)", connection);
                    command.Parameters.AddWithValue("@Type", machine.Type.ToString());
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while adding a new machine: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a new machine: " + ex.Message, ex);
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
        #region UpdateMachine
        /// <summary>
        /// Updates the machine.
        /// </summary>
        /// <param name="machine">The machine.</param>
        /// <exception cref="System.Exception">
        /// Machine with ID {machine.MachineID} not found.
        /// or
        /// A database error occurred while updating the machine: " + ex.Message
        /// or
        /// An error occurred while updating the machine: " + ex.Message
        /// </exception>
        public void UpdateMachine(Machine machine)
        {
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            try
            {
                {
                    var command = new SqlCommand("UPDATE Machine SET Type = @Type WHERE MachineID = @MachineID", connection);
                    command.Parameters.AddWithValue("@Type", machine.Type.ToString());
                    command.Parameters.AddWithValue("@MachineID", machine.MachineID);
                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception($"Machine with ID {machine.MachineID} not found.");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("A database error occurred while updating the machine: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the machine: " + ex.Message, ex);
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
        #region DeleteMachine
        /// <summary>
        /// Deletes the machine.
        /// </summary>
        /// <param name="machineId">The machine identifier.</param>
        /// <exception cref="System.Exception">
        /// Machine with ID {machineId} not found.
        /// or
        /// A database error occurred while deleting machine with ID {machineId}: {ex.Message}
        /// or
        /// An error occurred while deleting machine with ID {machineId}: {ex.Message}
        /// </exception>
        public void DeleteMachine(int machineId)
        {
            SqlConnection connection = null;
            connection = new SqlConnection(_connectionString);
            try
            {
                {
                    var command = new SqlCommand("DELETE FROM Machine WHERE MachineID = @MachineID", connection);
                    command.Parameters.AddWithValue("@MachineID", machineId);
                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception($"Machine with ID {machineId} not found.");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"A database error occurred while deleting machine with ID {machineId}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting machine with ID {machineId}: {ex.Message}", ex);
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
