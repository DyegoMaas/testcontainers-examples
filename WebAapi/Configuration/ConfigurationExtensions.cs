namespace WebAapi.Configuration;

public static class ConfigurationExtensions
{
    public static string PostgreSqlHost(this IConfiguration configuration) =>
        GetConfiguration("POSTGRESQL_HOST", configuration) ?? "127.0.0.1";

    public static int PostgreSqlPort(this IConfiguration configuration) =>
        int.Parse(GetConfiguration("POSTGRESQL_PORT", configuration) ?? "5555");

    public static string PostgreSqlDatabase(this IConfiguration configuration) =>
        GetConfiguration("POSTGRESQL_DATABASE", configuration)
        ?? "database";

    public static string PostgreSqlUsername(this IConfiguration configuration) =>
        GetConfiguration("POSTGRESQL_USERNAME", configuration) ?? "postgres";

    public static string PostgreSqlPassword(this IConfiguration configuration) =>
        GetConfiguration("POSTGRESQL_PASSWORD", configuration) ?? "postgres";

    public static int PostgreSqlCommandTimeout(this IConfiguration configuration)
    {
        var timeout =
            GetConfiguration("POSTGRESQL_COMMAND_TIMEOUT", configuration)
            ?? "60";
        return int.Parse(timeout);
    }

    public static string PostgreSqlSslMode(this IConfiguration configuration)
    {
        var sslMode =
            GetConfiguration("POSTGRESQL_SSL_MODE", configuration)
            ?? "Disable";
        return sslMode;
    }

    public static int PostgreSqlMinPoolSize(this IConfiguration configuration)
    {
        var minPoolSize =
            GetConfiguration("POSTGRESQL_MIN_POOL_SIZE", configuration)
            ?? "10";
        return int.Parse(minPoolSize);
    }

    public static int PostgreSqlMaxPoolSize(this IConfiguration configuration)
    {
        var maxPoolSize =
            GetConfiguration("POSTGRESQL_MAX_POOL_SIZE", configuration)
            ?? "20";
        return int.Parse(maxPoolSize);
    }

    private static string? GetConfiguration(string variableName, IConfiguration configuration)
    {
        var servicePrefix = Environment.GetEnvironmentVariable("SERVICE_PREFIX") ?? "";
        var withPrefix = servicePrefix + variableName;
        var withoutPrefix = variableName;

        var configuredValue = Environment.GetEnvironmentVariable(withoutPrefix)
                              ?? Environment.GetEnvironmentVariable(withPrefix)
                              ?? configuration?[withoutPrefix]
                              ?? configuration?[withPrefix];

        return configuredValue?.Trim();
    }
}