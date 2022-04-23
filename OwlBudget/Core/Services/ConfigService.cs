using Core.ServiceInterfaces;

namespace Core.Services;

public class ConfigService : IConfigService
{
    public ConfigService(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public string ConnectionString { get; }
}