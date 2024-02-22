using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Misc.Employee.Domain.Enums;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Misc.Employee.Areas.Admin.Models
{
    public partial record EmployeeSearchModel : BaseSearchModel
    {
        #region Ctor

        public EmployeeSearchModel()
        {
            AvailableActiveOpdions = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Plugins.Misc.Employee.List.SearchEmployeeName")]
        public string? SearchEmployeeName { get; set; }
        [NopResourceDisplayName("Plugins.Misc.Employee.List.SearchDesignation")]
        public DesignationType? SearchDesignation { get; set; }
        [NopResourceDisplayName("Plugins.Misc.Employee.List.SearchIsActive")]
        public int? SearchIsActiveId { get; set; }
        public IList<SelectListItem> AvailableActiveOpdions { get; set; }
        [NopResourceDisplayName("Plugins.Misc.Employee.List.SearchJoiningDate")]
        [UIHint("DateNullable")]
        public DateTime? SearchJoiningDate { get; set; }

        #endregion
    }
}

