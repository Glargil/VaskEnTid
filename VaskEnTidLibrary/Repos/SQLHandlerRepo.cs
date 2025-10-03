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
    public class SQLHandlerRepo : IMachineRepo
    {
        private string _connectionString;
        public SQLHandlerRepo(string connectionString)
        {
            _connectionString = "Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;";
        }


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
            }

            foreach (var machine in machines)
            {
                Console.WriteLine($"Id: {machine.MachineID}, Setup: {machine.Type}");
            }

            return machines;
        }
    }
}
