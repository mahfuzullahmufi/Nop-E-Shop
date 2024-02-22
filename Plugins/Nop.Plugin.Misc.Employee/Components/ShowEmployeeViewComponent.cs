using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.Employee.Areas.Admin.Factories;
using Nop.Plugin.Misc.Employee.Areas.Admin.Models;
using Nop.Plugin.Misc.Employee.Services;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.ShowText.Components
{
    public class ShowEmployeeViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeModelFactory _employeeModelFactory;

        public ShowEmployeeViewComponent(IStoreContext storeContext, 
            ISettingService settingService,
            IEmployeeService employeeService,
            IEmployeeModelFactory employeeModelFactory)
        {
            _storeContext = storeContext;
            _settingService = settingService;
            _employeeService = employeeService;
            _employeeModelFactory = employeeModelFactory;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var model = await _employeeModelFactory.PrepareEmployeeListModelAsync(new EmployeeSearchModel());
            //return View("~/Plugins/Widgets.ShowText/Views/ShowTextInfo.cshtml", model);
            return View(model);
        }
    }
}
