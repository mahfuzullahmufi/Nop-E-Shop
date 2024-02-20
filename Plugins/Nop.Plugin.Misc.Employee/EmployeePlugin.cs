using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Misc.Employee
{
    public class EmployeePlugin : BasePlugin, IAdminMenuPlugin, IMiscPlugin
    {
        #region Fields
        private readonly ILocalizationService _localizationService;
        private string prefixName = "Plugins.Misc.Employee";
        #endregion

        #region Ctor

        public EmployeePlugin(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return "";
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                [$"{prefixName}.Fields.Name"] = "Name",
                [$"{prefixName}.Fields.EmployeeDesignation"] = "Employee Designation",
                [$"{prefixName}.Fields.Salary"] = "Salary",
                [$"{prefixName}.Fields.IsActive"] = "Is Active",
                [$"{prefixName}.Fields.JoiningDate"] = "Joining Date",

                ["Plugins.Misc.Employee.Fields.Name.Required"] = "Name is required",
                ["Plugins.Misc.Employee.Fields.EmployeeDesignation.Required"] = "Employee Designation is required",
                ["Plugins.Misc.Employee.Fields.Salary.Required"] = "Salary is required",
                ["Plugins.Misc.Employee.Fields.JoiningDate.Required"] = "Joining Date is required",

                ["Plugins.Misc.Employee.List.SearchEmployeeName"] = "Employee Name",
                ["Plugins.Misc.Employee.List.SearchDesignation"] = "Employee Designation",
                ["Plugins.Misc.Employee.List.SearchIsActive"] = "Active Employee",
                ["Plugins.Misc.Employee.List.SearchJoiningDate"] = "Joining Date",

                ["Plugins.Misc.Employee.List"] = "Employee List",
                ["Plugins.Misc.Employee.AddNew"] = "Add Employee",
                ["Plugins.Misc.Employee.Edit"] = "Edit Employee",
                ["Plugins.Misc.Employee.BackToList"] = "Back To List",
            });

            await base.InstallAsync();
        }

        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            var listItem = new SiteMapNode()
            {
                Title = "Employee",
                Url = "~/Admin/Employee/List",
                Visible = true,
                IconClass = "far fa-circle",
                SystemName = "Misc.Employee"
            };

            rootNode.ChildNodes.Add(listItem);

        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task UninstallAsync()
        {
            //locales
            await _localizationService.DeleteLocaleResourcesAsync(prefixName);

            await base.UninstallAsync();
        }

        #endregion
    }
}

