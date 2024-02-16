using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.ShowText.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.ShowText.Components
{
    public class WidgetsShowTextViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;

        public WidgetsShowTextViewComponent(IStoreContext storeContext, 
            ISettingService settingService)
        {
            _storeContext = storeContext;
            _settingService = settingService;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var store = await _storeContext.GetCurrentStoreAsync();
            var showTextSettings = await _settingService.LoadSettingAsync<ShowTextSettings>(store.Id);

            var model = new ShowTextInfoModel
            {
                Text = showTextSettings.Text
            };

            //return View("~/Plugins/Widgets.ShowText/Views/ShowTextInfo.cshtml", model);
            return View(model);
        }
    }
}
