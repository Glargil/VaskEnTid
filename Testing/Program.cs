using VaskEnTidLibrary.Interfaces;
using VaskEnTidLibrary.Models.Machines;
using VaskEnTidLibrary.Repos;
using VaskEnTidLibrary.Models;
namespace Testing
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region InstantiateRepos
            //instantiate repos with connection string
            ITenantRepo TenantRepo = new VaskEnTidLibrary.Repos.TenantRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");
            IMachineRepo MachineRepo = new VaskEnTidLibrary.Repos.MachineRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");
            #endregion
            #region TestMachines
            List<Machine> machines = MachineRepo.GetAllMachines();
            foreach (var machine in machines)
            {
                Console.WriteLine($"Id: {machine.MachineID}, Setup: {machine.Type}");
            }
            MachineRepo.GetMachineById(1);
            MachineRepo.UpdateMachine(new Machine(1, Machine.MachineType.Washer));
            #endregion
            #region TestTenants
            List<Tenant> tenants = TenantRepo.GetAllTenants();
            foreach (var tenant in tenants)
            {
                Console.WriteLine(
                    $"Id: {tenant.TenantID}, Phone: {tenant.Phone}, Email: {tenant.Email}, Address: {tenant.Address}"
                    );
            }
            #endregion
        }
    }
}
