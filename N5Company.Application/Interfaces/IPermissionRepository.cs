using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N5Company.Domain.Entities;
namespace N5Company.Application.Interfaces
{
   

    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<IEnumerable<Permission>> GetWithTypeAsync();
    }
}
