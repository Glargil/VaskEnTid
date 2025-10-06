using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLibrary.Models.Machines
{
    public abstract class Machine
    {
        public enum MachineType { Dryer, Roller, Washer }
        public int MachineID { get; set; }
        public MachineType Type { get; set; }
        public Machine(int machineID, MachineType type)
        {
            MachineID = machineID;
            Type = type;
        }
        public Machine () { }
    }

}
