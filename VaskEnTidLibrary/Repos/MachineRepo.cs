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
    public class MachineRepo : IMachineRepo
    {
        private string _connectionString;
        public MachineRepo(string connectionString)
        {
            _connectionString = "Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;";
        }
        #region GetAllMachines
        public List<Machine> GetAllMachines()
        {
            var machines = new List<Machine>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT MachineID, Type FROM Machine", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var machine = new Machine
                        {
                            MachineID = (int)reader["MachineID"],
                            Type = (string)reader["Type"]
                        };
                        machines.Add(machine); // <-- Add to list
                    }
                }
                connection.Close();
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
        public Machine GetMachineById(int id)
        {
            Machine machine = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT MachineID, Type FROM Machine WHERE MachineID = @MachineID", connection);
                command.Parameters.AddWithValue("@MachineID", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        machine = new Machine
                        {
                            MachineID = (int)reader["MachineID"],
                            Type = (string)reader["Type"]
                        };
                    }
                }
                if (machine == null)
                {
                    throw new Exception($"Machine with ID {id} not found.");
                }
                if (machine != null)
                {
                    Console.WriteLine($"Id: {machine.MachineID}, Setup: {machine.Type}");
                }
                connection.Close();
                return machine;
            }
        }
        #endregion
        #region AddMachine
        public void AddMachine(Machine machine)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Machine (Type) VALUES (@Type)", connection);
                command.Parameters.AddWithValue("@Type", machine.Type);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        #endregion
        #region UpdateMachine
        public void UpdateMachine(Machine machine)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Machine SET Type = @Type WHERE MachineID = @MachineID", connection);
                command.Parameters.AddWithValue("@Type", machine.Type);
                command.Parameters.AddWithValue("@MachineID", machine.MachineID);
                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception($"Machine with ID {machine.MachineID} not found.");
                }
                connection.Close();
            }
        }
        #endregion
        #region DeleteMachine
        public void DeleteMachine(int machineId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Machine WHERE MachineID = @MachineID", connection);
                command.Parameters.AddWithValue("@MachineID", machineId);
                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception($"Machine with ID {machineId} not found.");
                }
                connection.Close();
            }
        }
        #endregion
    }
}
