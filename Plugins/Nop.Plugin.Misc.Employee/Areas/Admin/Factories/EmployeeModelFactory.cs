using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.Employee.Areas.Admin.Models;
using Nop.Plugin.Misc.Employee.Domain;
using Nop.Plugin.Misc.Employee.Domain.Enums;
using Nop.Plugin.Misc.Employee.Helper;
using Nop.Plugin.Misc.Employee.Services;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Misc.Employee.Areas.Admin.Factories
{
    public class EmployeeModelFactory : IEmployeeModelFactory
    {
        #region Fields
        private readonly IEmployeeService _employeeService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        #endregion

        #region Ctor
        public EmployeeModelFactory(IEmployeeService employeeService, ILocalizationService localizationService, IPictureService pictureService)
        {
            _employeeService = employeeService;
            _localizationService = localizationService;
            _pictureService = pictureService;
        }

        public async Task<EmployeeDetails> PrepareEmployeeDetailsAsync(EmployeeDetailsModel model, EmployeeDetails entity)
        {
            if (model != null)
            {
                //entity = model.ToEntity<EmployeeDetails>();
                entity.Id = model.Id;
                entity.Name = model.Name;
                entity.PictureId = model.PictureId;
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
            if (entity != null)
            {
                //model = entity.ToModel<EmployeeDetailsModel>();
                model.Id = entity.Id;
                model.Name = entity.Name;
                model.PictureId = entity.PictureId;
                model.EmployeeDesignation = (DesignationType)entity.EmployeeDesignationId;
                model.EmployeeDesignationTitle = model.EmployeeDesignation.GetDisplayName();
                model.Salary = entity.Salary;
                model.IsActive = entity.IsActive;
                model.JoiningDate = entity.JoiningDate;
            }
            return model;
        }

        public async Task<EmployeeListModel> PrepareEmployeeListModelAsync(EmployeeSearchModel searchModel)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(searchModel.SearchEmployeeName, (int)searchModel.SearchDesignation, searchModel.SearchIsActiveId, searchModel.SearchJoiningDate, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            var model = await new EmployeeListModel().PrepareToGridAsync(searchModel, employees, () =>
            {
                return employees.SelectAwait(async employee =>
                {
                    var employeeModel = await PrepareEmployeeDetailsModelAsync(new EmployeeDetailsModel(), employee);
                    var pictureUrl = await _pictureService.GetPictureUrlAsync(employeeModel.PictureId, 75, false);
                    if (!string.IsNullOrEmpty(pictureUrl))
                        employeeModel.PictureUrl = pictureUrl;

                    return employeeModel;
                });
            });

            return model;
        }

        public async Task<EmployeeSearchModel> PrepareEmployeeSearchModelAsync(EmployeeSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare "published" filter (0 - all; 1 - active only; 2 - inactive only)
            searchModel.AvailableActiveOpdions.Add(new SelectListItem
            {
                Value = "0",
                Text = await _localizationService.GetResourceAsync("Plugins.Misc.Employee.AvailableActiveOpdion.All")
            });
            searchModel.AvailableActiveOpdions.Add(new SelectListItem
            {
                Value = "1",
                Text = await _localizationService.GetResourceAsync("Plugins.Misc.Employee.AvailableActiveOpdion.ActiveEmployee")
            });
            searchModel.AvailableActiveOpdions.Add(new SelectListItem
            {
                Value = "2",
                Text = await _localizationService.GetResourceAsync("Plugins.Misc.Employee.AvailableActiveOpdion.InActiveEmployee")
            });

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;

        }
        #endregion
    }
}

