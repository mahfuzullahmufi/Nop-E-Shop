using Microsoft.AspNetCore.Authentication;
using Nop.Core;

namespace Nop.Plugin.Misc.Employee.Domain
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public double Salary { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

