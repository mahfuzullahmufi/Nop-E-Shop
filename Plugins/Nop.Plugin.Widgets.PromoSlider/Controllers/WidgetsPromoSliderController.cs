using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.PromoSlider;
using Nop.Plugin.Widgets.PromoSlider.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.NivoSlider.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class WidgetsPromoSliderController : BasePluginController
    {
        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;

        public WidgetsPromoSliderController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService, 
            IPictureService pictureService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var promoSliderSettings = await _settingService.LoadSettingAsync<PromoSliderSettings>(storeScope);
            var model = new ConfigurationModel
            {
                Picture1Id = promoSliderSettings.Picture1Id,
                Text1 = promoSliderSettings.Text1,
                Link1 = promoSliderSettings.Link1,
                AltText1 = promoSliderSettings.AltText1,
                Picture2Id = promoSliderSettings.Picture2Id,
                Text2 = promoSliderSettings.Text2,
                Link2 = promoSliderSettings.Link2,
                AltText2 = promoSliderSettings.AltText2,
                Picture3Id = promoSliderSettings.Picture3Id,
                Text3 = promoSliderSettings.Text3,
                Link3 = promoSliderSettings.Link3,
                AltText3 = promoSliderSettings.AltText3,
                Picture4Id = promoSliderSettings.Picture4Id,
                Text4 = promoSliderSettings.Text4,
                Link4 = promoSliderSettings.Link4,
                AltText4 = promoSliderSettings.AltText4,
                Picture5Id = promoSliderSettings.Picture5Id,
                Text5 = promoSliderSettings.Text5,
                Link5 = promoSliderSettings.Link5,
                AltText5 = promoSliderSettings.AltText5,
                ActiveStoreScopeConfiguration = storeScope
            };

            if (storeScope > 0)
            {
                model.Picture1Id_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Picture1Id, storeScope);
                model.Text1_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Text1, storeScope);
                model.Link1_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Link1, storeScope);
                model.AltText1_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.AltText1, storeScope);
                model.Picture2Id_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Picture2Id, storeScope);
                model.Text2_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Text2, storeScope);
                model.Link2_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Link2, storeScope);
                model.AltText2_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.AltText2, storeScope);
                model.Picture3Id_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Picture3Id, storeScope);
                model.Text3_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Text3, storeScope);
                model.Link3_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Link3, storeScope);
                model.AltText3_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.AltText3, storeScope);
                model.Picture4Id_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Picture4Id, storeScope);
                model.Text4_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Text4, storeScope);
                model.Link4_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Link4, storeScope);
                model.AltText4_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.AltText4, storeScope);
                model.Picture5Id_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Picture5Id, storeScope);
                model.Text5_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Text5, storeScope);
                model.Link5_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.Link5, storeScope);
                model.AltText5_OverrideForStore = await _settingService.SettingExistsAsync(promoSliderSettings, x => x.AltText5, storeScope);
            }

            return View("~/Plugins/Widgets.PromoSlider/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var promoSliderSettings = await _settingService.LoadSettingAsync<PromoSliderSettings>(storeScope);

            //get previous picture identifiers
            var previousPictureIds = new[] 
            {
                promoSliderSettings.Picture1Id,
                promoSliderSettings.Picture2Id,
                promoSliderSettings.Picture3Id,
                promoSliderSettings.Picture4Id,
                promoSliderSettings.Picture5Id
            };

            promoSliderSettings.Picture1Id = model.Picture1Id;
            promoSliderSettings.Text1 = model.Text1;
            promoSliderSettings.Link1 = model.Link1;
            promoSliderSettings.AltText1 = model.AltText1;
            promoSliderSettings.Picture2Id = model.Picture2Id;
            promoSliderSettings.Text2 = model.Text2;
            promoSliderSettings.Link2 = model.Link2;
            promoSliderSettings.AltText2 = model.AltText2;
            promoSliderSettings.Picture3Id = model.Picture3Id;
            promoSliderSettings.Text3 = model.Text3;
            promoSliderSettings.Link3 = model.Link3;
            promoSliderSettings.AltText3 = model.AltText3;
            promoSliderSettings.Picture4Id = model.Picture4Id;
            promoSliderSettings.Text4 = model.Text4;
            promoSliderSettings.Link4 = model.Link4;
            promoSliderSettings.AltText4 = model.AltText4;
            promoSliderSettings.Picture5Id = model.Picture5Id;
            promoSliderSettings.Text5 = model.Text5;
            promoSliderSettings.Link5 = model.Link5;
            promoSliderSettings.AltText5 = model.AltText5;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Picture1Id, model.Picture1Id_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Text1, model.Text1_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Link1, model.Link1_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.AltText1, model.AltText1_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Picture2Id, model.Picture2Id_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Text2, model.Text2_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Link2, model.Link2_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.AltText2, model.AltText2_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Picture3Id, model.Picture3Id_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Text3, model.Text3_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Link3, model.Link3_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.AltText3, model.AltText3_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Picture4Id, model.Picture4Id_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Text4, model.Text4_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Link4, model.Link4_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.AltText4, model.AltText4_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Picture5Id, model.Picture5Id_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Text5, model.Text5_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.Link5, model.Link5_OverrideForStore, storeScope, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(promoSliderSettings, x => x.AltText5, model.AltText5_OverrideForStore, storeScope, false);

            //now clear settings cache
            await _settingService.ClearCacheAsync();
            
            //get current picture identifiers
            var currentPictureIds = new[]
            {
                promoSliderSettings.Picture1Id,
                promoSliderSettings.Picture2Id,
                promoSliderSettings.Picture3Id,
                promoSliderSettings.Picture4Id,
                promoSliderSettings.Picture5Id
            };

            //delete an old picture (if deleted or updated)
            foreach (var pictureId in previousPictureIds.Except(currentPictureIds))
            { 
                var previousPicture = await _pictureService.GetPictureByIdAsync(pictureId);
                if (previousPicture != null)
                    await _pictureService.DeletePictureAsync(previousPicture);
            }

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
            
            return await Configure();
        }
    }
}