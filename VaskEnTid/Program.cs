using VaskEnTidLibrary.Interfaces;
using VaskEnTidLibrary.Repos;

namespace VaskEnTid
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IBookingRepo>(provider =>
    new BookingRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;"));

            builder.Services.AddScoped<IMachineRepo>(provider =>
    new MachineRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;"));

            builder.Services.AddScoped<ITenantRepo>(provider =>
    new TenantRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;"));
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
