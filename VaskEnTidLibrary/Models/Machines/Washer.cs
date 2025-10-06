using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLibrary.Models.Machines
{
    public class Washer : Machine
    {
        public Washer(int machineID, string type) : base(machineID, type)
        {
            MachineID = machineID;
            Type = type;
        }
        public Washer() { }
    }
}
