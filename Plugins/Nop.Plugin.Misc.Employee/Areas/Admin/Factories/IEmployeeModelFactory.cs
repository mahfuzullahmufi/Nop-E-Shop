using System.Threading.Tasks;
using Nop.Plugin.Misc.Employee.Areas.Admin.Models;
using Nop.Plugin.Misc.Employee.Domain;

namespace Nop.Plugin.Misc.Employee.Areas.Admin.Factories
{
    public interface IEmployeeModelFactory
    {
        Task<EmployeeSearchModel> PrepareEmployeeSearchModelAsync(EmployeeSearchModel searchModel);

        Task<EmployeeListModel> PrepareEmployeeListModelAsync(EmployeeSearchModel searchModel);

        Task<EmployeeDetailsModel> PrepareEmployeeDetailsModelAsync(EmployeeDetailsModel model, EmployeeDetails entity);
        Task<EmployeeDetails> PrepareEmployeeDetailsAsync(EmployeeDetailsModel model, EmployeeDetails entity);
    }
}
