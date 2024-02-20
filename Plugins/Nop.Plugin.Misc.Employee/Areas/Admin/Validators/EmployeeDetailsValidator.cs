using FluentValidation;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Misc.Employee.Areas.Admin.Models;
using Nop.Plugin.Misc.Employee.Domain.Enums;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Misc.Employee.Areas.Admin.Validators
{
    public class EmployeeDetailsValidator : BaseNopValidator<EmployeeDetailsModel>
    {
        public EmployeeDetailsValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name)
                 .NotEmpty()
                 .WithMessage(localizationService.GetResourceAsync("Plugins.Misc.Employee.Fields.Name.Required").Result);
            RuleFor(x => x.EmployeeDesignation)
                 .NotEmpty()
                 .WithMessage(localizationService.GetResourceAsync("Plugins.Misc.Employee.Fields.EmployeeDesignation.Required").Result)
                 .NotEqual(DesignationType.SelectDesignation)
                 .WithMessage("Please select Designation.");
            RuleFor(x => x.Salary)
                 .NotEmpty()
                 .WithMessage(localizationService.GetResourceAsync("Plugins.Misc.Employee.Fields.Salary.Required").Result);
            RuleFor(x => x.JoiningDate)
                 .NotEmpty()
                 .WithMessage(localizationService.GetResourceAsync("Plugins.Misc.Employee.Fields.JoiningDate.Required").Result);

        }
    }
}

