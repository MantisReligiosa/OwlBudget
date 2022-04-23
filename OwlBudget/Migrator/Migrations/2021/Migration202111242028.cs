using FluentMigrator;

namespace Migrator.Migrations;

[Migration(202111242028, "Init")]
public class Migration202111242028 : MigrationBase
{
    public override void Up()
    {
        Execute.EmbeddedScript("Migration202111242028.sql");
    }
}