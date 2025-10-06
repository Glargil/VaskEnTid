using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLibrary.Models.Machines
{
    public class Roller : Machine
    {
        public Roller(int machineID) : base(machineID, MachineType.Roller)
        {
            // No need to set Type again, base constructor handles it
        }
        public Roller() : base()
        {
            Type = MachineType.Roller;
        }
    }
}
