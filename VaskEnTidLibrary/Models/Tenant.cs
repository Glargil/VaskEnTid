using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaskEnTidLibrary.Models
{
    //class to represent a tenant in the system
    public class Tenant
    {
        //properties of the tenant
        public int TenantID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        //constructor to initialize a tenant object
        public Tenant(string name, string phone, string email, string address)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }
        //default constructor
        public Tenant() { }
    }
}
