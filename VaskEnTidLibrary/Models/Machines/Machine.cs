using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLibrary.Models.Machines
{
    public abstract class Machine
    {
        public int MachineID { get; set; }
        public string Type { get; set; }
        public Machine (int machineID, string type)
        {
            MachineID = machineID;
            Type = type;
        }
        public Machine () { }
    }

}
