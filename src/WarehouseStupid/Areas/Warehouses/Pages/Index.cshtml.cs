using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace WarehouseStupid.Areas.Warehouses.Pages;

public class WarehouseIndexModel : PageModel
{
    private readonly ILogger<WarehouseIndexModel> _logger;
    private readonly SqliteConnection _connection;

    public IEnumerable<WarehouseRec> Warehouses;

    public record WarehouseRec(long Id, string Name);

    public WarehouseIndexModel(ILogger<WarehouseIndexModel> logger, SqliteConnection connection)
    {
        _logger = logger;
        _connection = connection;
    }

    public void OnGet()
    {
        Warehouses = _connection.Query<WarehouseRec>("SELECT WarehouseId as Id, Name FROM warehouses");
    }
}