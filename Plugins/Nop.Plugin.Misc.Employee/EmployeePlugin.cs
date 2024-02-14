using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Plugins;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Misc.Employee
{
    public class EmployeePlugin : BasePlugin, IAdminMenuPlugin, IMiscPlugin
    {
        #region Fields

        private readonly IPermissionService _permissionService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public EmployeePlugin(IPermissionService permissionService,
            IWebHelper webHelper)
        {
            _permissionService = permissionService;
            _webHelper = webHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/WebApiFrontend/Configure";
        }

        /// <summary>
        /// Install the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            await base.InstallAsync();
        }

        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            //var menuItem = new SiteMapNode()
            //{
            //    Title = "Plugins",
            //    Visible = true,
            //    IconClass = "far fa-dot-circle",
            //};
            var listItem = new SiteMapNode()
            {
                Title = "Employee",
                Url = "~/Admin/Employee/List",
                Visible = true,
                IconClass = "far fa-circle",
                SystemName = "Misc.Employee"
            };
            //menuItem.ChildNodes.Add(listItem);


            rootNode.ChildNodes.Add(listItem);

        }

        /// <summary>
        /// Uninstall the plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task UninstallAsync()
        {
            await base.UninstallAsync();
        }

        #endregion
    }
}

