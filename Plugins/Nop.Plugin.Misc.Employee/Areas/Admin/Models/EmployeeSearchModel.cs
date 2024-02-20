using System;
using System.ComponentModel.DataAnnotations;
using Nop.Plugin.Misc.Employee.Domain.Enums;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.Employee.Areas.Admin.Models
{
    public partial record EmployeeSearchModel : BaseSearchModel
    {
        #region Properties

        [NopResourceDisplayName("Plugins.Misc.Employee.List.SearchEmployeeName")]
        public string? SearchEmployeeName { get; set; }
        [NopResourceDisplayName("Plugins.Misc.Employee.List.SearchDesignation")]
        public DesignationType? SearchDesignation { get; set; }
        [NopResourceDisplayName("Plugins.Misc.Employee.List.SearchIsActive")]
        public bool? SearchIsActive { get; set; }
        [NopResourceDisplayName("Plugins.Misc.Employee.List.SearchJoiningDate")]
        [UIHint("DateNullable")]
        public DateTime? SearchJoiningDate { get; set; }

        #endregion
    }
}

