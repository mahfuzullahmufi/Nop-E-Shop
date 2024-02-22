﻿using Nop.Core;
using Nop.Plugin.Misc.Employee.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core.Domain.Media;

namespace Nop.Plugin.Misc.Employee.Services
{
    public interface IEmployeeService
    {
        #region Employees

        Task<IPagedList<EmployeeDetails>> GetAllEmployeesAsync(string? name, int? designationId,
            int? isActiveId, DateTime? joiningDate, int pageIndex = 0, int pageSize = 10);
        Task<EmployeeDetails> GetEmployeeByIdAsync(int id);
        Task<IList<EmployeeDetails>> GetEmployeesByIdsAsync(int[] employeeIds);
        Task InsertEmployeeAsync(EmployeeDetails employee);
        Task UpdateEmployeeAsync(EmployeeDetails employee);
        Task DeleteEmployeesAsync(IList<EmployeeDetails> employees);
        Task DeleteEmployeeAsync(EmployeeDetails employee);
        Task<Picture> GetEmployeePictureAsync(int id);
        #endregion
    }
}
