﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Dapper;

namespace WarehouseStupid.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly SqliteConnection _connection;

    public IEnumerable<WarehouseRec> Warehouses;

    public record WarehouseRec(string Name);

    public IndexModel(ILogger<IndexModel> logger, SqliteConnection connection)
    {
        _logger = logger;
        _connection = connection;
    }

    public void OnGet()
    {
        Warehouses = _connection.Query<WarehouseRec>("SELECT Name FROM warehouses");
    }
}