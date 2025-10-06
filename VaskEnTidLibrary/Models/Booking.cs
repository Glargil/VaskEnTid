using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLibrary.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int TenantID { get; set; }
        public int MachineID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Booking(int bookingID, int tenantID, int machineID, DateTime startTime, DateTime endTime)
        {
            BookingID = bookingID;
            TenantID = tenantID;
            MachineID = machineID;
            StartTime = startTime;
            EndTime = endTime;
        }
        public Booking() { }
    }
}
