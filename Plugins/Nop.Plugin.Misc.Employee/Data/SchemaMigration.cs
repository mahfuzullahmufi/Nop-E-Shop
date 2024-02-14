using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Data.Extensions;

namespace Nop.Plugin.Misc.Employee.Data
{
    [NopMigration("2024/02/13 12:41:55:1687543", "Misc.Employee base schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<Nop.Plugin.Misc.Employee.Domain.Employee>();
        }
    }
}

