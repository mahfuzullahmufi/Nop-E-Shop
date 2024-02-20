using System;
using System.Linq;
using System.Threading.Tasks;
using Nop.Plugin.Misc.Employee.Areas.Admin.Models;
using Nop.Plugin.Misc.Employee.Domain;
using Nop.Plugin.Misc.Employee.Domain.Enums;
using Nop.Plugin.Misc.Employee.Helper;
using Nop.Plugin.Misc.Employee.Services;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.Employee.Areas.Admin.Factories
{
    public class EmployeeModelFactory : IEmployeeModelFactory
    {
        #region Fields
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Ctor
        public EmployeeModelFactory(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<EmployeeDetails> PrepareEmployeeDetailsAsync(EmployeeDetailsModel model, EmployeeDetails entity)
        {
            if (model != null)
            {
                //entity = model.ToEntity<EmployeeDetails>();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.EmployeeDesignationId = (int)model.EmployeeDesignation;
                entity.Salary = model.Salary;
                entity.IsActive = model.IsActive;
                entity.JoiningDate = model.JoiningDate;
            }
            return entity;
        }
        #endregion

        #region Methods
        public async Task<EmployeeDetailsModel> PrepareEmployeeDetailsModelAsync(EmployeeDetailsModel model, EmployeeDetails entity)
        {
            if(entity != null)
            {
                //model = entity.ToModel<EmployeeDetailsModel>();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.EmployeeDesignation = (DesignationType)entity.EmployeeDesignationId;
                //model.EmployeeDesignationTitle = (Enum.GetName(typeof(DesignationType), entity.EmployeeDesignationId)).GetDisplayName();
                model.EmployeeDesignationTitle = model.EmployeeDesignation.GetDisplayName();
                model.Salary = entity.Salary;
                model.IsActive = entity.IsActive;
                model.JoiningDate = entity.JoiningDate;
            }
            return model;
        }

        public async Task<EmployeeListModel> PrepareEmployeeListModelAsync(EmployeeSearchModel searchModel)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(searchModel.SearchEmployeeName, (int)searchModel.SearchDesignation, searchModel.SearchIsActive, searchModel.SearchJoiningDate, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new EmployeeListModel().PrepareToGridAsync(searchModel, employees, () =>
            {
                return employees.SelectAwait(async employee =>
                {
                    var employeeModel = await PrepareEmployeeDetailsModelAsync(new EmployeeDetailsModel(), employee);

                    return employeeModel;
                });
            });

            return model;
        }

        public async Task<EmployeeSearchModel> PrepareEmployeeSearchModelAsync(EmployeeSearchModel searchModel)
        {
            //if (searchModel == null)
            //    throw new ArgumentNullException(nameof(searchModel));

            searchModel.SearchIsActive = null;
            searchModel.SearchJoiningDate = null;

            searchModel.SetGridPageSize();

            return searchModel;

        }
        #endregion
    }
}

