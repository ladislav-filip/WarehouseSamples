using Microsoft.Data.Sqlite;

namespace WarehouseStupid.Infrastructure;

public class DbConnectionFactory
{
    private readonly IWebHostEnvironment _environment;

    public DbConnectionFactory(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public SqliteConnection CreateConnection()
    {
        var connString = "Data Source=" + Path.Combine(_environment.ContentRootPath, "warehouse.sqlite");
        return new SqliteConnection(connString);
    }
}