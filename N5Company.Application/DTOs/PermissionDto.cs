using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Application.DTOs
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLastName { get; set; }
        public DateTime Date { get; set; }

        public PermissionTypeDto PermissionType { get; set; }
    }
}
