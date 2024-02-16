using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.ShowText.Models
{
    public record ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }
        public bool Text_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Widgets.ShowText.Text")]
        public string Text { get; set; }
    }
}