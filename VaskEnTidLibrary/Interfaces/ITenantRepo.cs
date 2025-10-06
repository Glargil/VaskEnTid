using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaskEnTidLibrary.Models;

namespace VaskEnTidLibrary.Interfaces
{
    public interface ITenantRepo
    {
        List<Tenant> GetAllTenants();
        void AddTenant(Tenant tenant);
        void DeleteTenant(int tenantId);
        void UpdateTenant(Tenant tenant);
    }
}
