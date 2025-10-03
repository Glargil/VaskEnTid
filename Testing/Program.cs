using VaskEnTidLibrary.Interfaces;
using VaskEnTidLibrary.Models.Machines;
using VaskEnTidLibrary.Repos;
namespace Testing
{
    public class Program
    {
        static void Main(string[] args)
        {
            IMachineRepo SQLHandlerRepo = new VaskEnTidLibrary.Repos.SQLHandlerRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");
            List<Machine> machines = SQLHandlerRepo.GetAllMachines();
            foreach (var machine in machines)
            {
                Console.WriteLine($"Id: {machine.MachineID}, Setup: {machine.Type}");
            }
        }
    }
}
