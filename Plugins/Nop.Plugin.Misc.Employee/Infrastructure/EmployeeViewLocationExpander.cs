using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Nop.Plugin.Misc.Employee.Infrastructure
{
    public class EmployeeViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            viewLocations = new[] {
                    $"/Plugins/Misc.Employee/Views/{{1}}/{{0}}.cshtml",
                    $"/Plugins/Misc.Employee/Areas/Admin/Views/{{1}}/{{0}}.cshtml",
                }.Concat(viewLocations);

            return viewLocations;
        }



        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}

