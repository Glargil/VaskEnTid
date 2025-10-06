using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLibrary.Models
{
    public class Booking
    {
        public enum LaundrySlot
        {
            Slot06 = 6,
            Slot08 = 8,
            Slot10 = 10,
            Slot12 = 12,
            Slot14 = 14,
            Slot16 = 16,
            Slot18 = 18
        }

        public int BookingID { get; set; }
        public int TenantID { get; set; }
        public int MachineID { get; set; }
        public LaundrySlot StartSlot { get; set; }
        public DateTime BookingDate { get; set; } // Date only
        public DateTime StartTime => BookingDate.Date.AddHours((int)StartSlot);
        public DateTime EndTime => StartTime.AddHours(2);

        public Booking(int bookingID, int tenantID, int machineID, LaundrySlot startSlot, DateTime bookingDate)
        {
            BookingID = bookingID;
            TenantID = tenantID;
            MachineID = machineID;
            StartSlot = startSlot;
            BookingDate = bookingDate.Date;
        }

        public Booking() { }
    }
}
