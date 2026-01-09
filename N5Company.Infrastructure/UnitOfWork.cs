using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N5Company.Application.Interfaces;
using N5Company.Infrastructure.Persistence;
using N5Company.Infrastructure.Repositories;

namespace N5Company.Infrastructure
{
   
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PermissionsDbContext _context;

        public IPermissionRepository Permissions { get; }

        public UnitOfWork(PermissionsDbContext context)
        {
            _context = context;
            Permissions = new PermissionRepository(context);
        }

        public async Task<int> CompleteAsync()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();
    }

}
