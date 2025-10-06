using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLibrary.Models.Machines
{
    public class Roller : Machine
    {
        public Roller(int machineID, string type):base(machineID, type)
        {
            MachineID = machineID;
            Type = type;
        }
        public Roller() { }
    }
}
