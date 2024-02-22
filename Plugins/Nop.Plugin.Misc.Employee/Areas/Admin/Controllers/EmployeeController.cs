using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Misc.Employee.Areas.Admin.Factories;
using Nop.Plugin.Misc.Employee.Areas.Admin.Models;
using Nop.Plugin.Misc.Employee.Domain;
using Nop.Plugin.Misc.Employee.Services;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Misc.Employee.Areas.Admin.Controllers
{
    public class EmployeeController : BaseAdminController
    {
        #region Fields
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeModelFactory _employeeModelFactory;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IStoreContext _storeContext;

        #endregion

        #region Ctor

        public EmployeeController(IEmployeeModelFactory employeeModelFactory,
            IEmployeeService employeeService,
            IDateTimeHelper dateTimeHelper,
            ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IStoreContext storeContext)
        {
            _employeeService = employeeService;
            _employeeModelFactory = employeeModelFactory;
            _dateTimeHelper = dateTimeHelper;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _storeContext = storeContext;
        }

        #endregion

        #region Methods
        public async Task<IActionResult> List()
        {
            var model = await _employeeModelFactory.PrepareEmployeeSearchModelAsync(new EmployeeSearchModel());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> List(EmployeeSearchModel searchModel)
        {
            var model = await _employeeModelFactory.PrepareEmployeeListModelAsync(searchModel);
            return Json(model);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public async Task<IActionResult> Create(EmployeeDetailsModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var employee = await _employeeModelFactory.PrepareEmployeeDetailsAsync(model, new EmployeeDetails());
                await _employeeService.InsertEmployeeAsync(employee);
                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = employee.Id });
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                _notificationService.ErrorNotification("No employee found!");
                return RedirectToAction("List");
            }

            var model = await _employeeModelFactory.PrepareEmployeeDetailsModelAsync(new EmployeeDetailsModel(), employee);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Edit(EmployeeDetailsModel model, bool continueEditing)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(model.Id);
            if (employee == null)
            {
                _notificationService.ErrorNotification("No employee found!");
                return RedirectToAction("List");
            }

            if (ModelState.IsValid)
            {
                //prepare entity
                employee = await _employeeModelFactory.PrepareEmployeeDetailsAsync(model, new EmployeeDetails());
                await _employeeService.UpdateEmployeeAsync(employee);
                _notificationService.SuccessNotification("Employee updated.");
                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = employee.Id });
            }
            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(EmployeeDetailsModel model)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(model.Id);
            if (employee == null)
                return RedirectToAction("List");

            await _employeeService.DeleteEmployeeAsync(employee);

            _notificationService.ErrorNotification("Employee Deleted.");

            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteSelected(ICollection<int> selectedIds)
        {
            if (selectedIds == null || selectedIds.Count == 0)
                return NoContent();

            await _employeeService.DeleteEmployeesAsync((await _employeeService.GetEmployeesByIdsAsync(selectedIds.ToArray())).ToList());

            _notificationService.ErrorNotification("Employees Deleted.");

            return Json(new { Result = true });
        }
        #endregion
    }
}

