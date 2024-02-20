using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.Employee.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nop.Plugin.Misc.Employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Fields

        private readonly IRepository<EmployeeDetails> _employeeRepository;

        #endregion

        #region Ctor

        public EmployeeService(IRepository<EmployeeDetails> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion

        #region Methods

        public async Task<IPagedList<EmployeeDetails>> GetAllEmployeesAsync(string? name, int? designationId, bool? isActive, DateTime? joiningDate, int pageIndex = 0, int pageSize = 10)
        {
            var query = _employeeRepository.Table;

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));

            if(designationId > 0)
                query = query.Where(x => x.EmployeeDesignationId == designationId);

            if (isActive != null)
                query = query.Where(x => x.IsActive == isActive);

            if (joiningDate != null)
                query = query.Where(x => x.JoiningDate == joiningDate);

            query = query.OrderByDescending(x => x.JoiningDate);

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        public async Task<EmployeeDetails> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id, cache => default);
        }

        public async Task InsertEmployeeAsync(EmployeeDetails employee)
        {
            await _employeeRepository.InsertAsync(employee);
        }

        public async Task UpdateEmployeeAsync(EmployeeDetails employee)
        {
            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteEmployeesAsync(IList<EmployeeDetails> employees)
        {
            await _employeeRepository.DeleteAsync(employees);
        }

        public async Task<IList<EmployeeDetails>> GetEmployeesByIdsAsync(int[] employeeIds)
        {
            if (employeeIds == null || !employeeIds.Any())
                return new List<EmployeeDetails>();

            return await _employeeRepository.GetByIdsAsync(employeeIds, cache => default);
        }

        public async Task DeleteEmployeeAsync(EmployeeDetails employee)
        {
            await _employeeRepository.DeleteAsync(employee);
        }

        #endregion
    }
}

