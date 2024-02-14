using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.PromoSlider.Infrastructure.Cache;
using Nop.Plugin.Widgets.PromoSlider.Models;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.PromoSlider.Components
{
    public class WidgetsPromoSliderViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        private readonly IWebHelper _webHelper;

        public WidgetsPromoSliderViewComponent(IStoreContext storeContext, 
            IStaticCacheManager staticCacheManager, 
            ISettingService settingService, 
            IPictureService pictureService,
            IWebHelper webHelper)
        {
            _storeContext = storeContext;
            _staticCacheManager = staticCacheManager;
            _settingService = settingService;
            _pictureService = pictureService;
            _webHelper = webHelper;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var store = await _storeContext.GetCurrentStoreAsync();
            var promoSliderSettings = await _settingService.LoadSettingAsync<PromoSliderSettings>(store.Id);

            var model = new PublicInfoModel
            {
                Picture1Url = await GetPictureUrlAsync(promoSliderSettings.Picture1Id),
                Text1 = promoSliderSettings.Text1,
                Link1 = promoSliderSettings.Link1,
                AltText1 = promoSliderSettings.AltText1,

                Picture2Url = await GetPictureUrlAsync(promoSliderSettings.Picture2Id),
                Text2 = promoSliderSettings.Text2,
                Link2 = promoSliderSettings.Link2,
                AltText2 = promoSliderSettings.AltText2,

                Picture3Url = await GetPictureUrlAsync(promoSliderSettings.Picture3Id),
                Text3 = promoSliderSettings.Text3,
                Link3 = promoSliderSettings.Link3,
                AltText3 = promoSliderSettings.AltText3,
            };

            if (string.IsNullOrEmpty(model.Picture1Url) && string.IsNullOrEmpty(model.Picture2Url) &&
                string.IsNullOrEmpty(model.Picture3Url))
                //no pictures uploaded
                return Content("");

            return View("~/Plugins/Widgets.PromoSlider/Views/PublicInfo.cshtml", model);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected async Task<string> GetPictureUrlAsync(int pictureId)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY, 
                pictureId, _webHelper.IsCurrentConnectionSecured() ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);

            return await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                //little hack here. nulls aren't cacheable so set it to ""
                var url = await _pictureService.GetPictureUrlAsync(pictureId, showDefaultPicture: false) ?? "";
                return url;
            });
        }
    }
}
