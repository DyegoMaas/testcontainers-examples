using System.Data.Common;
using System.Data.Entity.Infrastructure;
using Npgsql;

namespace WebAapi.Database;

public class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public NpgsqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbConnection CreateConnection(string nameOrConnectionString)
    {
        return new NpgsqlConnection(_connectionString);
    }
}