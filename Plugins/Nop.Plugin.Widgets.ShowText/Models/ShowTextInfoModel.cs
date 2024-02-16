using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.ShowText.Models
{
    public record ShowTextInfoModel : BaseNopModel
    {
        public string Text { get; set; }
    }
}