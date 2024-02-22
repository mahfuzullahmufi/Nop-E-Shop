using System;
using Nop.Core;

namespace Nop.Plugin.Misc.Employee.Domain
{
    public class EmployeeDetails : BaseEntity
    {
        public string Name { get; set; }
        public int EmployeeDesignationId { get; set; }
        public int PictureId { get; set; }
        public double Salary { get; set; }
        public bool IsActive { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}

