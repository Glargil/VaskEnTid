using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLibrary.Models
{
    public class Booking
    {
        int BookingID { get; set; }
        int TenantID { get; set; }
        int MachineID { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        string Status { get; set; }
        public Booking(int bookingID, int tenantID, int machineID, DateTime startTime, DateTime endTime, string status)
        {
            BookingID = bookingID;
            TenantID = tenantID;
            MachineID = machineID;
            StartTime = startTime;
            EndTime = endTime;
            Status = status;
        }
        public Booking() { }
    }
}
