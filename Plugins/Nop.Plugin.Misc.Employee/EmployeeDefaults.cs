using Nop.Core.Caching;

namespace Nop.Plugin.Misc.Employee
{
    public static class EmployeeDefaults
    {
        public static CacheKey EmployeeCacheKey => new CacheKey("Plugins.Misc.Employee", EmployeePluginPrefix);

        public static string EmployeePluginPrefix => "Nop.employeedetails.";
    }
}

