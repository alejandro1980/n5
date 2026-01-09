using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using N5Company.Application.Interfaces;
using N5Company.Domain.Entities;
using N5Company.Infrastructure.Persistence;

namespace N5Company.Infrastructure.Repositories
{
   

    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(PermissionsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Permission>> GetWithTypeAsync()
        {
            return await _context.Permissions
                .Include(p => p.PermissionType)
                .ToListAsync();
        }
    }
}
