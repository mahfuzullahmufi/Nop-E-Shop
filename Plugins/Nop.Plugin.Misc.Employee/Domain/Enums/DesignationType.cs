using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Misc.Employee.Domain.Enums
{
    public enum DesignationType
    {
        [Display(Name = "Select Designation")]
        SelectDesignation = 0,
        [Display(Name = "Associate Software Engineer")]
        AssociateSoftwareEngineer = 1,
        [Display(Name = "Software Engineer")]
        SoftwareEngineer = 2,
        [Display(Name = "Project Manager")]
        ProjectManager = 3,
    }
}
