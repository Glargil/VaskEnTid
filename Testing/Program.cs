using VaskEnTidLibrary.Interfaces;
using VaskEnTidLibrary.Models.Machines;
using VaskEnTidLibrary.Repos;
namespace Testing
{
    public class Program
    {
        static void Main(string[] args)
        {
            IMachineRepo MachineRepo = new VaskEnTidLibrary.Repos.MachineRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");
            List<Machine> machines = MachineRepo.GetAllMachines();
            foreach (var machine in machines)
            {
                Console.WriteLine($"Id: {machine.MachineID}, Setup: {machine.Type}");
            }
            MachineRepo.GetMachineById(1);
            MachineRepo.UpdateMachine(new Machine(1, "Washer"));
        }
    }
}
