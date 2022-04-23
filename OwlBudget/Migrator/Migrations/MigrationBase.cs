using System;
using FluentMigrator;

namespace Migrator.Migrations;

public abstract class MigrationBase : Migration
{
    public override void Down()
    {
        throw new NotSupportedException();
    }
}