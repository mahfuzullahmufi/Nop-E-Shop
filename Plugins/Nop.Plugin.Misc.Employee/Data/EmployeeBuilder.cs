using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;

namespace Nop.Plugin.Misc.Employee.Data
{
    public class EmployeeBuilder : NopEntityBuilder<Nop.Plugin.Misc.Employee.Domain.Employee>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(Domain.Employee.Name)).AsString(200)
                .WithColumn(nameof(Domain.Employee.Salary)).AsInt32()
                .WithColumn(nameof(Domain.Employee.CreatedDate)).AsDateTime();
        }
    }
}

