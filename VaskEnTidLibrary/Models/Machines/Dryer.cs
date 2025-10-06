using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLibrary.Models.Machines
{
    public class Dryer : Machine
    {
        public Dryer(int machineID) : base(machineID, MachineType.Dryer)
        {
            // No need to set Type again, base constructor handles it
        }
        public Dryer() : base()
        {
            Type = MachineType.Dryer;
        }
    }
}
