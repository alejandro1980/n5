using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Domain.Entities
{
    public class PermissionType
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
