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
            IBookingRepo BookingRepo = new VaskEnTidLibrary.Repos.BookingRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");
            #endregion
            #region TestMachines
            List<Machine> machines = MachineRepo.GetAllMachines();
            foreach (var machine in machines)
            {
                Console.WriteLine($"Id: {machine.MachineID}, Setup: {machine.Type}");
            }
            MachineRepo.GetMachineById(1);
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

            //BookingRepo.CreateBooking(new Booking
            //{

            //    TenantID = 1,
            //    MachineID = 1,
            //    BookingDate = DateTime.Now,
            //    StartSlot = Booking.LaundrySlot.Slot06
            //});
            //    Console.WriteLine($"Id: {machine.MachineID}, Setup: {machine.Type}");
            //}
            //MachineRepo.GetMachineById(1);
            //MachineRepo.UpdateMachine(new Machine(1, Machine.MachineType.Washer));
            //List<Tenant> tenants = TenantRepo.GetAllTenants();
            //foreach (var tenant in tenants)
            //{
            //    Console.WriteLine(
            //        $"Id: {tenant.TenantID}, Phone: {tenant.Phone}, Email: {tenant.Email}, Address: {tenant.Address}"
            //        );
            //}
            //TenantRepo.DeleteTenant(13);
            //TenantRepo.DeleteTenant(14);
            //TenantRepo.DeleteTenant(15);
            //foreach (var tenant in tenants)
            //{
            //    Console.WriteLine(
            //        $"Id: {tenant.TenantID}, Phone: {tenant.Phone}, Email: {tenant.Email}, Address: {tenant.Address}"
            //        );
            //}

        }
    }
}
