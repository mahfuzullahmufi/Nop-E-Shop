﻿using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.PromoSlider.Components;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.PromoSlider
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class PromoSliderPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly INopFileProvider _fileProvider;

        public PromoSliderPlugin(ILocalizationService localizationService,
            IPictureService pictureService,
            ISettingService settingService,
            IWebHelper webHelper,
            INopFileProvider fileProvider)
        {
            _localizationService = localizationService;
            _pictureService = pictureService;
            _settingService = settingService;
            _webHelper = webHelper;
            _fileProvider = fileProvider;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the widget zones
        /// </returns>
        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(new List<string> { PublicWidgetZones.ProductDetailsAfterBreadcrumb });
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsPromoSlider/Configure";
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public Type GetWidgetViewComponent(string widgetZone)
        {
            return typeof(WidgetsPromoSliderViewComponent);
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task InstallAsync()
        {
            //pictures
            var sampleImagesPath = _fileProvider.MapPath("~/Plugins/Widgets.PrmoSlider/Content/promoslider/sample-images/");

            //settings
            var settings = new PromoSliderSettings
            {
                Picture1Id = (await _pictureService.InsertPictureAsync(await _fileProvider.ReadAllBytesAsync(_fileProvider.Combine(sampleImagesPath, "banner_01.webp")), MimeTypes.ImageWebp, "banner_1")).Id,
                Text1 = "",
                Link1 = _webHelper.GetStoreLocation(),
                Picture2Id = (await _pictureService.InsertPictureAsync(await _fileProvider.ReadAllBytesAsync(_fileProvider.Combine(sampleImagesPath, "banner_02.webp")), MimeTypes.ImageWebp, "banner_2")).Id,
                Text2 = "",
                Link2 = _webHelper.GetStoreLocation()
                //Picture3Id = _pictureService.InsertPicture(File.ReadAllBytes(_fileProvider.Combine(sampleImagesPath,"banner3.jpg")), MimeTypes.ImagePJpeg, "banner_3").Id,
                //Text3 = "",
                //Link3 = _webHelper.GetStoreLocation(),
            };
            await _settingService.SaveSettingAsync(settings);

            await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Plugins.Widgets.PromoSlider.Picture1"] = "Picture 1",
                ["Plugins.Widgets.PromoSlider.Picture2"] = "Picture 2",
                ["Plugins.Widgets.PromoSlider.Picture3"] = "Picture 3",
                ["Plugins.Widgets.PromoSlider.Picture4"] = "Picture 4",
                ["Plugins.Widgets.PromoSlider.Picture5"] = "Picture 5",
                ["Plugins.Widgets.PromoSlider.Picture"] = "Picture",
                ["Plugins.Widgets.PromoSlider.Picture.Hint"] = "Upload picture.",
                ["Plugins.Widgets.PromoSlider.Text"] = "Comment",
                ["Plugins.Widgets.PromoSlider.Text.Hint"] = "Enter comment for picture. Leave empty if you don't want to display any text.",
                ["Plugins.Widgets.PromoSlider.Link"] = "URL",
                ["Plugins.Widgets.PromoSlider.Link.Hint"] = "Enter URL. Leave empty if you don't want this picture to be clickable.",
                ["Plugins.Widgets.PromoSlider.AltText"] = "Image alternate text",
                ["Plugins.Widgets.PromoSlider.AltText.Hint"] = "Enter alternate text that will be added to image."
            });

            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        /// <returns>A task that represents the asynchronous operation</returns>
        public override async Task UninstallAsync()
        {
            //settings
            await _settingService.DeleteSettingAsync<PromoSliderSettings>();

            //locales
            await _localizationService.DeleteLocaleResourcesAsync("Plugins.Widgets.PromoSlider");

            await base.UninstallAsync();
        }

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;
    }
}