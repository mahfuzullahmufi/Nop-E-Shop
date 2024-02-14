using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.PromoSlider.Models
{
    public record ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }
        
        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.Picture")]
        [UIHint("Picture")]
        public int Picture1Id { get; set; }
        public bool Picture1Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.Text")]
        public string Text1 { get; set; }
        public bool Text1_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.Link")]
        public string Link1 { get; set; }
        public bool Link1_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.AltText")]
        public string AltText1 { get; set; }
        public bool AltText1_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.Picture")]
        [UIHint("Picture")]
        public int Picture2Id { get; set; }
        public bool Picture2Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.Text")]
        public string Text2 { get; set; }
        public bool Text2_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.Link")]
        public string Link2 { get; set; }
        public bool Link2_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.AltText")]
        public string AltText2 { get; set; }
        public bool AltText2_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.Picture")]
        [UIHint("Picture")]
        public int Picture3Id { get; set; }
        public bool Picture3Id_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.Text")]
        public string Text3 { get; set; }
        public bool Text3_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.Link")]
        public string Link3 { get; set; }
        public bool Link3_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.PromoSlider.AltText")]
        public string AltText3 { get; set; }
        public bool AltText3_OverrideForStore { get; set; }
    }
}