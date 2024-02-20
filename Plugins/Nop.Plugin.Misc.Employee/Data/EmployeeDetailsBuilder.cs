using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.Employee.Domain;

namespace Nop.Plugin.Misc.Employee.Data
{
    public class EmployeeDetailsBuilder : NopEntityBuilder<EmployeeDetails>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(EmployeeDetails.Name)).AsString(200)
                .WithColumn(nameof(EmployeeDetails.EmployeeDesignationId)).AsInt32()
                .WithColumn(nameof(EmployeeDetails.Salary)).AsDouble()
                .WithColumn(nameof(EmployeeDetails.JoiningDate)).AsDateTime();
        }
    }
}

