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
        [BindProperty]
        public List<Booking> Bookings { get; private set; }
        public BookingOverviewModel(ILogger<BookingOverviewModel> logger, IBookingRepo IBookingRepo)
        {
            _logger = logger;
            _bookingRepo = IBookingRepo;
            
        }
        public void OnGet()
        {
            Bookings = _bookingRepo.GetAllBookings();
        }
    }
}
