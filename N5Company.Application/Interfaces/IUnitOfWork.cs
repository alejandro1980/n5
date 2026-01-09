using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPermissionRepository Permissions { get; }
        Task<int> CompleteAsync();
    }
}
