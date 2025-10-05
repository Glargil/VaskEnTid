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
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        //constructor to initialize a tenant object
        public Tenant(int tenantID, string phone, string email, string address)
        {
            TenantID = tenantID;
            Phone = phone;
            Email = email;
            Address = address;
        }
        //default constructor
        public Tenant() { }
    }
}
