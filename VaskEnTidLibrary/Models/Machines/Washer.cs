using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLibrary.Models.Machines
{
    public class Washer : Machine
    {
        public Washer(int machineID) : base(machineID, MachineType.Washer)
        {
            // No need to set Type again, base constructor handles it
        }
        public Washer() : base()
        {
            Type = MachineType.Washer;
        }
    }
}
