using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLibrary.Models;

namespace VaskEnTidLibrary.Interfaces
{
    public interface IBookingRepo
    {
        List<Booking> GetAllBookings();
        Booking GetBookingByID(int id);
        void CreateBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(int id);
    }
}
