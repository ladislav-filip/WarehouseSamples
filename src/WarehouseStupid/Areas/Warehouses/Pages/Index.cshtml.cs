using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;

namespace WarehouseStupid.Areas.Warehouses.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly SqliteConnection _connection;

    public IEnumerable<WarehouseRec> Warehouses;

    public record WarehouseRec(long Id, string Name);

    public IndexModel(ILogger<IndexModel> logger, SqliteConnection connection)
    {
        _logger = logger;
        _connection = connection;
    }

    public void OnGet()
    {
        Warehouses = _connection.Query<WarehouseRec>("SELECT WarehouseId as Id, Name FROM warehouses");
    }
}