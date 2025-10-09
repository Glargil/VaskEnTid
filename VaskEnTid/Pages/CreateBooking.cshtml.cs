using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VaskEnTidLibrary.Interfaces;
using VaskEnTidLibrary.Models;
using VaskEnTidLibrary.Repos;

namespace VaskEnTid.Pages
{
    public class CreateBookingModel : PageModel
    {
        [BindProperty]
        public DateTime SelectedDate { get; set; } = DateTime.Today;

        [BindProperty]
        public int SelectedMachineId { get; set; }

        [BindProperty]
        public int SelectedTenantId { get; set; }

        [BindProperty]
        public int SelectedAvailableSlot { get; set; }

        public List<SelectListItem> Machines { get; set; } = new();
        public List<SelectListItem> Tenants { get; set; } = new();
        public List<SelectListItem> AllSlots { get; set; } = new();
        public List<SelectListItem> AvailableSlots { get; set; } = new();
        public List<Booking> Bookings { get; private set; } = new();

        private readonly IBookingRepo _bookingRepo = new BookingRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");
        private readonly IMachineRepo _machineRepo = new MachineRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");
        private readonly ITenantRepo _tenantRepo = new TenantRepo("Server=mssql.mkhansen.dk,1436;Database=Laundromat;User Id=sa;Password=Laundromat25;Encrypt=true;TrustServerCertificate=True;");

        public void OnGet()
        {
            LoadDropdowns();
        }

        public IActionResult OnPost()
        {
            try
            {
                var booking = new Booking
                {
                    MachineID = SelectedMachineId,
                    TenantID = SelectedTenantId,
                    BookingDate = SelectedDate.Date,
                    StartSlot = (Booking.LaundrySlot)SelectedAvailableSlot
                };
                _bookingRepo.CreateBooking(booking);
                TempData["BookingSuccess"] = "Din booking er oprettet!";
            }
            catch (Exception ex)
            {
                TempData["BookingError"] = "Kunne ikke oprette booking: " + ex.Message;
            }

            LoadDropdowns();
            return Page();
        }

        private void LoadDropdowns()
        {
            Machines = _machineRepo.GetAllMachines()
                .Select(m => new SelectListItem { Value = m.MachineID.ToString(), Text = $"{m.MachineID} - {m.Type}" })
                .ToList();

            Tenants = _tenantRepo.GetAllTenants()
                .Select(t => new SelectListItem { Value = t.TenantID.ToString(), Text = t.Name })
                .ToList();

            AllSlots = Enum.GetValues(typeof(Booking.LaundrySlot))
                .Cast<Booking.LaundrySlot>()
                .Select(slot => new SelectListItem
                {
                    Value = ((int)slot).ToString(),
                    Text = $"{(int)slot:00}:00 - {((int)slot + 2):00}:00"
                }).ToList();

            Bookings = _bookingRepo.GetAllBookings();

            AvailableSlots = AllSlots.Where(slot =>
                !Bookings.Any(b =>
                    b.MachineID == SelectedMachineId &&
                    b.BookingDate == SelectedDate.Date &&
                    (int)b.StartSlot == int.Parse(slot.Value)
                )
            ).ToList();
        }
    }
}