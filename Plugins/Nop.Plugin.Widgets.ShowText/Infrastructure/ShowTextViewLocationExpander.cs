using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Nop.Plugin.Widgets.ShowText.Infrastructure
{
    public class ShowTextViewLocationExpander : IViewLocationExpander
    {
        private const string THEME_KEY = "nop.themename";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            context.Values.TryGetValue(THEME_KEY, out string theme);

            if (theme == "Mytheme")
            {
                viewLocations = new[] {
                        $"/Plugins/Widgets.ShowText/Themes/{theme}/Views/Shared/{{0}}.cshtml",
                        $"/Plugins/Widgets.ShowText/Themes/{theme}/Views/{{1}}/{{0}}.cshtml"
                    }.Concat(viewLocations);
            }
            else
            {
                viewLocations = new[] {
                    $"/Plugins/Widgets.ShowText/Views/Shared/{{0}}.cshtml",
                    $"/Plugins/Widgets.ShowText/Views/{{1}}/{{0}}.cshtml",
                }.Concat(viewLocations);
            }

            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}

