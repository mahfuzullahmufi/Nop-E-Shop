using System;
using System.ComponentModel.DataAnnotations;
using Nop.Plugin.Misc.Employee.Domain.Enums;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.Employee.Areas.Admin.Models
{
    public record EmployeeDetailsModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Plugins.Misc.Employee.Fields.Name")]
        public string Name { get; set; }
        [NopResourceDisplayName("Plugins.Misc.Employee.Fields.EmployeeDesignation")]
        public DesignationType EmployeeDesignation { get; set; }
        public string? EmployeeDesignationTitle { get; set; }
        [NopResourceDisplayName("Plugins.Misc.Employee.Fields.Salary")]
        public double Salary { get; set; }
        [NopResourceDisplayName("Plugins.Misc.Employee.Fields.IsActive")]
        public bool IsActive { get; set; }
        [NopResourceDisplayName("Plugins.Misc.Employee.Fields.JoiningDate")]
        [UIHint("DateNullable")]
        public DateTime JoiningDate { get; set; }
    }
}

