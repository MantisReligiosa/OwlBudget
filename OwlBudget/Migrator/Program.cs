using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Migrator.Migrations;

namespace Migrator;

internal class Program
{
    private static string _connStr;

    private static void Main(string[] args)
    {
        if (args.Length != 1) throw new ArgumentException("Wrong connection string");

        _connStr = args[0];

        var serviceProvider = CreateServices();

        using var scope = serviceProvider.CreateScope();
        UpdateDatabase(scope.ServiceProvider);
    }

    /// <summary>
    ///     Configure the dependency injection services
    /// </summary>
    private static IServiceProvider CreateServices()
    {
        return new ServiceCollection()
            // Add common FluentMigrator services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                // Add SQLite support to FluentMigrator
                .AddSQLite()
                // Set the connection string
                .WithGlobalConnectionString(_connStr)
                // Define the assembly containing the migrations
                .ScanIn(typeof(MigrationBase).Assembly).For.All())
            // Enable logging to console in the FluentMigrator way
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            // Build the service provider
            .BuildServiceProvider(false);
    }

    /// <summary>
    ///     Update the database
    /// </summary>
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        // Instantiate the runner
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        // Execute the migrations
        runner.MigrateUp();
    }
}