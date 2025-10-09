using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaskEnTidLibrary.Models;
using VaskEnTidLibrary.Interfaces;
using VaskEnTidLibrary.Repos;
using VaskEnTidLibrary.Models.Machines;

namespace VaskEnTid.Pages
{
    public class BookingOverviewModel : PageModel
    {
        private readonly ILogger<BookingOverviewModel> _logger;
        private static IBookingRepo _bookingRepo = new BookingRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");
        private static ITenantRepo _tenantRepo = new TenantRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");

        // Filters
        [BindProperty(SupportsGet = true)]
        public string TenantNameFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookingDateFilter { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookingIDFilter { get; set; }

        [BindProperty]
        public List<Booking> Bookings { get; private set; }
        [BindProperty]
        public List<Tenant> Tenants { get; private set; }
        public BookingOverviewModel(ILogger<BookingOverviewModel> logger, IBookingRepo IBookingRepo, ITenantRepo ITenantRepo)
        {
            _logger = logger;
            _bookingRepo = IBookingRepo;
            _tenantRepo = ITenantRepo;
        }
        public void OnGet()
        {
            Tenants = _tenantRepo.GetAllTenants();
            var allBookings = _bookingRepo.GetAllBookings();
            var allTenants = _tenantRepo.GetAllTenants();

            // Join bookings with tenants for filtering by name
            var filtered = allBookings
                .Join(allTenants, b => b.TenantID, t => t.TenantID, (b, t) => new { Booking = b, Tenant = t });

            if (!string.IsNullOrWhiteSpace(TenantNameFilter))
            {
                filtered = filtered.Where(x => x.Tenant.Name.Contains(TenantNameFilter, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrWhiteSpace(BookingDateFilter))
            {
                filtered = filtered.Where(x => x.Booking.BookingDate.ToString("yyyy-MM-dd").Contains(BookingDateFilter));
            }
            if (!string.IsNullOrWhiteSpace(BookingIDFilter))
            {
                filtered = filtered.Where(x => x.Booking.BookingID.ToString().Contains(BookingIDFilter));
            }

            Bookings = filtered.Select(x => x.Booking).ToList();
        }

    }
}
