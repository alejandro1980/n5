using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }

        public int PermissionTypeId { get; set; }
        public PermissionType PermissionType { get; set; }

        public DateTime Date { get; set; }
    }
}
