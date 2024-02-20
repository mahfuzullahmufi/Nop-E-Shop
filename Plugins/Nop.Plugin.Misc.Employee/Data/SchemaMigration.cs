using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Data.Extensions;
using Nop.Plugin.Misc.Employee.Domain;

namespace Nop.Plugin.Misc.Employee.Data
{
    [NopMigration("2024/02/19 03:41:55:1687543", "Misc.Employee base schema", MigrationProcessType.Installation)]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<EmployeeDetails>();
        }
    }
}

