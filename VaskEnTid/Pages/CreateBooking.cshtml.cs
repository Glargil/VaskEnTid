using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VaskEnTidLibrary.Interfaces;
using VaskEnTidLibrary.Models;
using VaskEnTidLibrary.Repos;

namespace VaskEnTid.Pages
{
    public class CreateBookingModel : PageModel
    {
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        [BindProperty]
        public DateTime _sDT { get; set; }
        [BindProperty]
        public DateTime _eDT { get; set; }
        [BindProperty]
        public required string _machineType { get; set; }
        public List<Booking> Bookings { get; private set; }

        private readonly ILogger<BookingOverviewModel> _logger;
        private static IBookingRepo _bookingRepo = new BookingRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");
        public void OnGet()
        {
        }

        public void CreateBooking(Booking booking)
        {
            _bookingRepo.CreateBooking(booking);
            _logger.LogInformation("CreateBooking!");
        }
        public IActionResult OnPostDelete(int bookingId)
        {
            _bookingRepo.DeleteBooking(bookingId);
            return RedirectToPage();
        }
    }
}
