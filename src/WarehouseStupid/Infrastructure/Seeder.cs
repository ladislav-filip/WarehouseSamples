using Microsoft.Data.Sqlite;

namespace WarehouseStupid.Infrastructure;

public static class SqliteConnectionExtensions
{

    public static async Task CreateAndSeedAsync(this SqliteConnection connection)
    {
        if (File.Exists(connection.DataSource))
        {
            File.Delete(connection.DataSource);
            Console.WriteLine($"Drop database '{connection.DataSource}'");
        }
        
        Console.WriteLine("Create database and seed...");
        
        await connection.OpenAsync();
        var cmd = new SqliteCommand(@"CREATE TABLE products( 
            ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
            Code TEXT not null, 
            Name TEXT not null,
            Weight INTEGER not null
        )", connection);
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE warehouses(
            WarehouseId INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT not null
        )";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE locations(
            LocationId INTEGER PRIMARY KEY AUTOINCREMENT,
            WarehouseId INTEGER not null,
            Name TEXT not null,
            WeightCapacity INTEGER not null,
            FOREIGN KEY (WarehouseId) REFERENCES warehouses(WarehouseId)        
            ) ";
        cmd.ExecuteNonQuery();

        cmd.CommandText = @"CREATE TABLE productslocations(
            ProductLocationId INTEGER PRIMARY KEY AUTOINCREMENT,
            LocationId INTEGER not null, 
            ProductId INTEGER not null, 
            Amount INTEGER not null,
            FOREIGN KEY (LocationId) REFERENCES locations(LocationId),
            FOREIGN KEY (ProductId) REFERENCES products(ProductId)
        )";
        cmd.ExecuteNonQuery();
        
        Seed(connection);
        
        await connection.CloseAsync();
        
        Console.WriteLine("Database created.");
    }

    private static void Seed(SqliteConnection connection)
    {
        Console.WriteLine("Start seed data...");

        var cmd = new SqliteCommand();
        cmd.Connection = connection;

        #region Products
        cmd.CommandText = @"INSERT INTO products(Code, Name, Weight) VALUES('M01', 'Mleko', 25);
    INSERT INTO products(Code, Name, Weight) VALUES('45','PALETY EUR vrácené evidenční',1);
INSERT INTO products(Code, Name, Weight) VALUES('44','PALETY EUR dodané evidenční',1);
INSERT INTO products(Code, Name, Weight) VALUES('3000','Bio Eidam 30% bloček 200g',21);
INSERT INTO products(Code, Name, Weight) VALUES('3041',' Bio Gouda 48% plátky 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('3042',' Bio Eidam 30% plátky 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('3050',' Bio sýrové tyčky pařené 60g',7);
INSERT INTO products(Code, Name, Weight) VALUES('32233','Eidam cihla 30%',301);
INSERT INTO products(Code, Name, Weight) VALUES('41111','Máslo 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('41113','Bio máslo 150g, 16ks/kar',16);
INSERT INTO products(Code, Name, Weight) VALUES('4121','Bio Máslo bloky Varnsdorf 10kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('5000',' Smetanový jogurt bílý 145g',16);
INSERT INTO products(Code, Name, Weight) VALUES('5001',' Smetanový jogurt mix 145g',16);
INSERT INTO products(Code, Name, Weight) VALUES('5010','Bio  jogurt bílý 0% 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5020',' jogurt 0% bílý 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5021',' jogurt 0% jahoda 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5022',' jogurt 0% borůvka 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5023',' jogurt 0% čokoláda 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5025',' jogurt 0% meruňka,med + citrón,limetka 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5026',' jogurt 0% rybíz 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5027',' jogurt 0% karamel 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5029',' jogurt 0% MIX jahoda,borůvka,čokoláda 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5030',' jogurt 0% bílý 3x140g',43);
INSERT INTO products(Code, Name, Weight) VALUES('5031',' jogurt 0% jahoda 3x140g',43);
INSERT INTO products(Code, Name, Weight) VALUES('5032',' jogurt 0% borůvka 3x140g',43);
INSERT INTO products(Code, Name, Weight) VALUES('5033',' jogurt 0% čokoláda 3x140g',43);
INSERT INTO products(Code, Name, Weight) VALUES('5040',' jogurt 0% karamel 3x140g',43);
INSERT INTO products(Code, Name, Weight) VALUES('5050',' jogurt 5% bílý 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5051',' jogurt 5% mix (jahoda + borůvka) 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5053',' jogurt 5% mix (vanilka + čokoláda) 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5066',' jogurt 5% mix (vanilka+čokoláda) 2x140g',29);
INSERT INTO products(Code, Name, Weight) VALUES('5300','0% bílý 16ks/krt 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5301','0% bílý 8ks/krt 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5302','0% ostružina 16ks/krt 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5303','0% ostružina 8ks/krt 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5304','0% vanilka 16ks/krt 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5305','0% vanilka 8ks/krt 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5306','0% jahoda 16ks/krt 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5307','0% jahoda 8ks/krt 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5308','0% višeň 16ks/krt 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5309','0% višeň 8ks/krt 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5320','bílý 0% 4x130g',53);
INSERT INTO products(Code, Name, Weight) VALUES('5321','ostružina 0% 4x130g',53);
INSERT INTO products(Code, Name, Weight) VALUES('5322','vanilka 0% 4x130g',53);
INSERT INTO products(Code, Name, Weight) VALUES('63037',' dezert jahodový 140g, 16ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('63039',' dezert jahodový 140g, 8ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('63040',' dezert ananasový 140g, 8ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('63042',' dezert jahoda+ananas 2x2x140g',57);
INSERT INTO products(Code, Name, Weight) VALUES('63044',' dezert jahoda+višeň 2x2x140g',57);
INSERT INTO products(Code, Name, Weight) VALUES('63051',' dezert višňový 140g, 8ks/kar',15);
INSERT INTO products(Code, Name, Weight) VALUES('6401',' Tvh měkký 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64012',' Tvh zahradní směs 4x140g',57);
INSERT INTO products(Code, Name, Weight) VALUES('64013',' Tvh paprika 4x140g',57);
INSERT INTO products(Code, Name, Weight) VALUES('64016',' Tvh paprika 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('64017',' Tvh zahradní směs 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('64026','Měkky tvh folie 250g vakuový VRN',26);
INSERT INTO products(Code, Name, Weight) VALUES('6403',' Tvh měkký 4x250g',101);
INSERT INTO products(Code, Name, Weight) VALUES('6404',' Tvh měkký 250g, 12ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64041',' Tvh měkký 24x250g',601);
INSERT INTO products(Code, Name, Weight) VALUES('64046',' Tvh odtučněný 250g, 12ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('6405',' Tvh měkký 12x250g',301);
INSERT INTO products(Code, Name, Weight) VALUES('64050',' Tvh polotučný 24x250g',601);
INSERT INTO products(Code, Name, Weight) VALUES('64051',' Tvh tučný 24x250g',601);
INSERT INTO products(Code, Name, Weight) VALUES('64061',' Tvh polotučný 250g, 12ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64074',' Bio čerstvý sýr 120g',13);
INSERT INTO products(Code, Name, Weight) VALUES('64078','Bio Tvh měkký 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('6413',' smetanová čerstvý sýr kbelík 1 kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('6416',' jogurt 0% bílý kbelík 1kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('6417',' Tvh měkký kbelík 1kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('6419',' Tvh tučný kbelík 1kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('6426',' Tvh tučný kbelík 5kg',501);
INSERT INTO products(Code, Name, Weight) VALUES('6427','MC Tvh tučný kbelík 5kg',501);
INSERT INTO products(Code, Name, Weight) VALUES('6432',' Tvh měkký kbelík 5kg',501);
INSERT INTO products(Code, Name, Weight) VALUES('6433','MC Tvh měkký kbelík 5kg',501);
INSERT INTO products(Code, Name, Weight) VALUES('64332','MC Tvh měkký kbelík 3kg',301);
INSERT INTO products(Code, Name, Weight) VALUES('6434',' Tvh měkký kbelík 10kg',1001);
INSERT INTO products(Code, Name, Weight) VALUES('6441',' Tvh polotučný 250g, 12ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('6442',' Tvh polotučný 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('6443',' Tvh tučný 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('6445',' Tvh tučný 4x250g',101);
INSERT INTO products(Code, Name, Weight) VALUES('6446',' Tvh tučný 250g, 12ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('6447',' Tvh tučný 12x250g',301);
INSERT INTO products(Code, Name, Weight) VALUES('6448','Bio Zakysaná smetana 175g',19);
INSERT INTO products(Code, Name, Weight) VALUES('65000','Termix kakaový 90g',10);
INSERT INTO products(Code, Name, Weight) VALUES('65001','Termix kakaový 24x90g',217);
INSERT INTO products(Code, Name, Weight) VALUES('65003','Termix kakaový 8x90g',73);
INSERT INTO products(Code, Name, Weight) VALUES('65010','Termix jahodový 90g',10);
INSERT INTO products(Code, Name, Weight) VALUES('65011','Termix jahodový 24x90g',217);
INSERT INTO products(Code, Name, Weight) VALUES('65013','Termix jahodový 8x90g',73);
INSERT INTO products(Code, Name, Weight) VALUES('65020','Termix vanilkový 90g',10);
INSERT INTO products(Code, Name, Weight) VALUES('65021','Termix vanilkový 24x90g',217);
INSERT INTO products(Code, Name, Weight) VALUES('65023','Termix vanilkový 8x90g',73);
INSERT INTO products(Code, Name, Weight) VALUES('6525',' žervé s kozím tvhem 2x80g',17);
INSERT INTO products(Code, Name, Weight) VALUES('6527',' žervé krémové 2x80g',17);
INSERT INTO products(Code, Name, Weight) VALUES('6528',' žervé s pažitkou 2x80g',17);
INSERT INTO products(Code, Name, Weight) VALUES('6529',' žervé s paprikou 2x80g',17);
INSERT INTO products(Code, Name, Weight) VALUES('6530',' žervé krémové 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('6533',' žervé s pažitkou 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('6534',' žervé s paprikou 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('6540',' smetanová čerstvý sýr 120g',13);
INSERT INTO products(Code, Name, Weight) VALUES('6607',' Tvh jahoda/citron+limetka 130g, 12ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('6609',' Tvh borůvka/lesní plody 130g, 12ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('6635',' Tvh vanilkový+čokoládový 130g, 6+6ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('6642',' vanilka + čokoláda 2x130g',27);
INSERT INTO products(Code, Name, Weight) VALUES('66471',' Tvh borůvka/lesní plody 2x130g',27);
INSERT INTO products(Code, Name, Weight) VALUES('6650',' Bio dezert banánová 90 g',10);
INSERT INTO products(Code, Name, Weight) VALUES('6651',' Bio dezert malinová 90 g',10);
INSERT INTO products(Code, Name, Weight) VALUES('6652',' Bio dezert vanilková 90 g',10);
INSERT INTO products(Code, Name, Weight) VALUES('6653',' Bio dezert čokoládová 90 g',10);
INSERT INTO products(Code, Name, Weight) VALUES('6656',' Bio MIX banán,malina 90 g',10);
INSERT INTO products(Code, Name, Weight) VALUES('6657',' Bio MIX vanilka,čokoláda 90 g',10);
INSERT INTO products(Code, Name, Weight) VALUES('6700',' kakaový 90g, 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('6701',' kakaový 90g, 8ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('6702',' vanilkový 90g, 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('6703',' vanilkový 90g, 8ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('6704',' smetanový 90g, 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('6705',' smetanový 90g, 8ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('6707',' kakaový 3x90g',28);
INSERT INTO products(Code, Name, Weight) VALUES('6708',' vanilkový 3x90g',28);
INSERT INTO products(Code, Name, Weight) VALUES('6709',' smetanový 3x90g',28);
INSERT INTO products(Code, Name, Weight) VALUES('6720',' kakaový 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('6721',' vanilkový 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('6722',' smetanový 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('3020','Naše Bio  sýr bílý kostka',101);
INSERT INTO products(Code, Name, Weight) VALUES('3040','Bio Eidam 45% bloček 200g',21);
INSERT INTO products(Code, Name, Weight) VALUES('3051',' Bio sýrové tyčky pařené 80g (4x20g)',9);
INSERT INTO products(Code, Name, Weight) VALUES('3060','NB Eidam 30%  100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('3061','NB Gouda 48% 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('32207',' Bonbony pařený uzený sýr 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('32221','Nitě bílé pařený sýr 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('32242','Gouda 48% plátky 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('32246','Eidam 45% plátky 100 g',11);
INSERT INTO products(Code, Name, Weight) VALUES('32251','Kozí gouda plátky 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('32252','Kozí gouda bloček 200g',21);
INSERT INTO products(Code, Name, Weight) VALUES('32400','Copánek bílý 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('32402',' Copánek bílý 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('32403',' Copánek uzený 70g',8);
INSERT INTO products(Code, Name, Weight) VALUES('5042',' jogurt 0% MIX karamel, vanilka 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5048','BIO  jogurt MIX 0% a 5% 130g (6+6)',14);
INSERT INTO products(Code, Name, Weight) VALUES('5049','Bio  jogurt bílý 5% 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5068',' Bio  jogurt bílý 5% 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5069','  jogurt 5% bílý 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5070','  jogurt 0% bílý 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5071','  jogurt 0% MIX 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5316','0% mix jahoda, višeň 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5317','0% mix vanilka, ostružina 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('6236','Smetana ke šlehání 33% kbelík 5kg',501);
INSERT INTO products(Code, Name, Weight) VALUES('63038',' dezert ananasový 140g, 16ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('63041',' dezert jahoda+ananas 140g, 16ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('63050',' dezert višňový 140g, 16ks/kar',15);
INSERT INTO products(Code, Name, Weight) VALUES('64019',' Tvh MIX paprika, zahradní směs 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('64034',' tvh polotučný 500g',51);
INSERT INTO products(Code, Name, Weight) VALUES('64035',' tvh tučný 500g',51);
INSERT INTO products(Code, Name, Weight) VALUES('64036',' tvh měkký 500g',51);
INSERT INTO products(Code, Name, Weight) VALUES('64040','Globus Tvh měkký 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64043','TS tvh měkký 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64045','AQ Tvh odtučněný 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('64047',' Tvh měkký 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64048','CLEVER Tvh měkký 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64049','CLEVER Tvh tučný 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64052',' Tvh polotučný 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64053','CLEVER Tvh polotučný 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64059','TS tučný tvh 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('64065','TS polotučný tvh 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('64070','Globus Tvh tučný 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64071',' Tvh tučný 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64076','NP bio tvh odtučněný 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('64077','Naše Bio Tvh měkký 250g , 8ks/kar',26);
INSERT INTO products(Code, Name, Weight) VALUES('64110','Naše Bio Zakysaná smetana 175g, 8ks/krt',19);
INSERT INTO products(Code, Name, Weight) VALUES('6412','LUKA čerstvý sýr 1 kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('6414','Farmářský smetanový jogurt bílý kbelík 1kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('6415',' Žervé smetanové kbelík 1kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('6418',' Tvh polotučný kbelík 1kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('64331',' Tvh měkký kbelík 3kg',301);
INSERT INTO products(Code, Name, Weight) VALUES('6435',' Tvh měkký 18% 10kg',1001);
INSERT INTO products(Code, Name, Weight) VALUES('6438','Meliko Tvh polotučný 5 kg',501);
INSERT INTO products(Code, Name, Weight) VALUES('6449','NP bio smetana kysaná 175g',19);
INSERT INTO products(Code, Name, Weight) VALUES('65009','Termix kakaový,vanilkový XXL 150g',16);
INSERT INTO products(Code, Name, Weight) VALUES('6512','MELIKO Žervé 3kg',301);
INSERT INTO products(Code, Name, Weight) VALUES('6518',' žervé smetanové kbelík 3kg',301);
INSERT INTO products(Code, Name, Weight) VALUES('6519','Sýr na cheesecake 3kg',301);
INSERT INTO products(Code, Name, Weight) VALUES('6521',' žervé krémové 8ks/krt 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('6522',' žervé s pažitkou 8ks/krt 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('6523',' žervé s paprikou 8ks/krt 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('6524',' žervé s kozím tvhem 8ks/krt 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('6526',' žervé s kozím tvhem 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('6538',' Žervé krémové 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('6539','Žervé krémové XXL 140 g',15);
INSERT INTO products(Code, Name, Weight) VALUES('6600','BIO  MIX 90g',10);
INSERT INTO products(Code, Name, Weight) VALUES('6655',' Bio MIX B,M,V,Č 90 g',10);
INSERT INTO products(Code, Name, Weight) VALUES('6670',' XXL MIX vanilka,čokoláda 140 g',15);
INSERT INTO products(Code, Name, Weight) VALUES('6710',' MIX kakao,vanilka,smetana 90g, 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('6725',' MIX vanilka, kakao 130 g',14);
INSERT INTO products(Code, Name, Weight) VALUES('6726',' MIX vanilka, kakao, smetana 130 g',14);
INSERT INTO products(Code, Name, Weight) VALUES('77720','BIO tav.sýr 3v1 80g/12ks',9);
INSERT INTO products(Code, Name, Weight) VALUES('3019','Bio copánek uzený 70g',8);
INSERT INTO products(Code, Name, Weight) VALUES('32205','Bonbony pařený uzený sýr 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('32222','Nitě bílé pařený sýr 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('32206','Uzené bonbony a sýrové nitě mix 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('4118','Máslo bloky 10kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('4008',' Tvh měkký 250g/24 ks D.',26);
INSERT INTO products(Code, Name, Weight) VALUES('3996','BIO  jogurt 0% 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4053',' jogurt 0% bílý 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4054',' jogurt 0% jahoda 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4055',' jogurt 0% borůvka 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4056',' jogurt 0% čokoláda 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4058',' jogurt 0% m,m/c,l 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4065',' jogurt 0% rybíz 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4079',' jogurt 0% karamel 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('3998','BIO  jogurt 5% 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4063',' jogurt 5% bílý 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4064',' jogurt 5% mix (jahoda + borůvka) 140gD.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4066',' jogurt 5% mix (vanilka + čokoláda) 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4043','0% bílý 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4044','0% ostružina 130g  D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4045','0% vanilka 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4046','0% jahoda 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4047','0% višeň 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4030',' dezert jahodový 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4031',' dezert ananasový 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4026',' dezert višňový 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4032',' Tvh paprika 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4033',' Tvh zel. zahradní směs 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4001',' Tvh polotučný 250g/24 ks D.',26);
INSERT INTO products(Code, Name, Weight) VALUES('4005',' Tvh tučný 250g/24 ks D.',26);
INSERT INTO products(Code, Name, Weight) VALUES('4042','Bio Zakysaná smetana 175g D.',19);
INSERT INTO products(Code, Name, Weight) VALUES('4010','Termix kakaový 90g D.',10);
INSERT INTO products(Code, Name, Weight) VALUES('4011','Termix jahodový 90g D.',10);
INSERT INTO products(Code, Name, Weight) VALUES('4012','Termix vanilkový 90g D.',10);
INSERT INTO products(Code, Name, Weight) VALUES('4034',' žervé ochucené 80g D.',9);
INSERT INTO products(Code, Name, Weight) VALUES('4039',' smetanová čerstvý sýr 120g D.',13);
INSERT INTO products(Code, Name, Weight) VALUES('4014',' Tvh jahoda/citron+limetka 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4021',' Tvh borůvka/lesní plody 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4017',' Tvh vanilka, čokoláda 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4051',' Bio banán D.',10);
INSERT INTO products(Code, Name, Weight) VALUES('3999',' Bio malina D.',10);
INSERT INTO products(Code, Name, Weight) VALUES('4052',' Bio vanilka D.',10);
INSERT INTO products(Code, Name, Weight) VALUES('4000',' Bio čoko D.',10);
INSERT INTO products(Code, Name, Weight) VALUES('4048',' kakao 90g D.',10);
INSERT INTO products(Code, Name, Weight) VALUES('4049',' vanilka 90g D.',10);
INSERT INTO products(Code, Name, Weight) VALUES('4050',' smetana 90g D.',10);
INSERT INTO products(Code, Name, Weight) VALUES('4018',' kakao 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4019',' vanilka 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4020',' smetana 130g D.',14);
INSERT INTO products(Code, Name, Weight) VALUES('4076','Dip česnek s bylinkami 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4077','DIP Paprika s jalapeňos 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('4078',' jogurt DIP tzatziki 140g D.',15);
INSERT INTO products(Code, Name, Weight) VALUES('64014',' Tvh paprika 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('64015',' Tvh zahradní směs 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('6606',' Tvh jahoda/citron+limetka 130g, 24ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('6608',' Tvh borůvka/lesní plody 130g, 24ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('6627',' Tvh vanilkový+čokoládový 130g, 24ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('7000',' jogurt DIP limetka, chilli 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('7001',' jogurt DIP mango, curry 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('7002',' jogurt DIP tzatziki 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('3002','Bio Eidam 30%',101);
INSERT INTO products(Code, Name, Weight) VALUES('3010','Bio Gouda 48%',291);
INSERT INTO products(Code, Name, Weight) VALUES('41112','Bio máslo 200g, 12ks/kar',21);
INSERT INTO products(Code, Name, Weight) VALUES('4119','Bio Máslo bloky Varnsdorf 10kg',1001);
INSERT INTO products(Code, Name, Weight) VALUES('4122','Farmářské BIO máslo cca 1,6kg',161);
INSERT INTO products(Code, Name, Weight) VALUES('32190',' sýr bílý kostka cca 125g',101);
INSERT INTO products(Code, Name, Weight) VALUES('32191',' sýr paprika kostka cca 125g',101);
INSERT INTO products(Code, Name, Weight) VALUES('32192',' sýr gyros kostka cca 125g',101);
INSERT INTO products(Code, Name, Weight) VALUES('32195',' sýr uzený kostka cca 125g',101);
INSERT INTO products(Code, Name, Weight) VALUES('32204','Bonbony pařený uzený sýr 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('32213','Copánek pařený sýr bazalka cca 110g',101);
INSERT INTO products(Code, Name, Weight) VALUES('32214','Copánek pařený sýr gyros cca 110 g',101);
INSERT INTO products(Code, Name, Weight) VALUES('32217','Copánek pařený sýr cca 110g',101);
INSERT INTO products(Code, Name, Weight) VALUES('32218','Zopfkäse geräuchert - Lackman',101);
INSERT INTO products(Code, Name, Weight) VALUES('32295','Kozí  sýr uzený kostka',101);
INSERT INTO products(Code, Name, Weight) VALUES('32296','Kozí  bílý sýr 150g',16);
INSERT INTO products(Code, Name, Weight) VALUES('32401','Copánek uzený 70g',8);
INSERT INTO products(Code, Name, Weight) VALUES('5024',' jogurt 0% pečené jablko, skořice/hruška, skořice 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5028',' jogurt meruňka/med, citron s limetkou, karamel, vanilka 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5039',' jogurt 0% MIX karamel, čokoláda 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5052',' jogurt mix 4% (meruňka med + citrón limetka) 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5100','0% bílý 16ks/krt 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5101','0% ostružina 16ks/krt 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5102','0% malina 16ks/krt 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5103','0% jahoda 16ks/krt 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5104','0% višeň 16ks/krt 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5118','multipack 4x140g',57);
INSERT INTO products(Code, Name, Weight) VALUES('5120','SKYR Albert bílý 0 % 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5299','SKYR Albert bílý 0 % 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('63053',' dezert 10xjah, 6xviš, 16ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('6400',' Tvh měkký 500g, 12ks/krt',51);
INSERT INTO products(Code, Name, Weight) VALUES('64073',' Bio dezert vanilka+čoko 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('64080',' Bio dezert malina+banán 100 g',11);
INSERT INTO products(Code, Name, Weight) VALUES('64084','Bio tvh s jogurtem jahoda 140g, 8ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('64086','Bio tvh s jogurtem brusinka 140g, 8ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('64094',' tvhový dezert jahoda 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('64095',' tvhový dezert brusinka 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('64099',' Bio tvh s jogurtem MIX 4x100g multipack',41);
INSERT INTO products(Code, Name, Weight) VALUES('64060','AQ Tvh polotučný 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('64069','AQ Tvh tučný 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('64113',' dětský tvhový dezert vanilka 90g',10);
INSERT INTO products(Code, Name, Weight) VALUES('64114',' dětský tvhový dezert čokoláda 90g',10);
INSERT INTO products(Code, Name, Weight) VALUES('65004','Termix MIX 4x90g multipack',37);
INSERT INTO products(Code, Name, Weight) VALUES('65030','Termixík čokoládový 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('65031','Termixík cheesecake 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('65032','Termixík jahodový 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('6531',' kozí  120g',13);
INSERT INTO products(Code, Name, Weight) VALUES('6532',' žervé žampionové 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('65043','Termixík 3x100g klíčenka',31);
INSERT INTO products(Code, Name, Weight) VALUES('66003',' jahodovo-banánový 90g, 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66006',' smetanový 90g, 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66009',' kakaový 90g, 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66010',' vanilkový 90g, 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66025',' 4x90g multipack',37);
INSERT INTO products(Code, Name, Weight) VALUES('66063',' 4x100g multipack',41);
INSERT INTO products(Code, Name, Weight) VALUES('66070',' kakao 140g X',15);
INSERT INTO products(Code, Name, Weight) VALUES('66027',' malinový 90g, 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66040',' kakaový 100g, 24ks/krt',11);
INSERT INTO products(Code, Name, Weight) VALUES('66044',' vanilkový 100g, 24ks/krt',11);
INSERT INTO products(Code, Name, Weight) VALUES('66048',' smetanový 100g, 24ks/krt',11);
INSERT INTO products(Code, Name, Weight) VALUES('66052',' jahodovo-banánový 100g, 24ks/krt',11);
INSERT INTO products(Code, Name, Weight) VALUES('66071',' vanilka 140g X',15);
INSERT INTO products(Code, Name, Weight) VALUES('66072',' mix vanilka, kakao MAXI 140 g',15);
INSERT INTO products(Code, Name, Weight) VALUES('6614',' Tvh vanilkový 130g, 12ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('6615',' Tvh čokoládový 130g, 12ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('6616',' Tvh pečené jablko, skořice/hruška, skořice 130g, 12ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('6626',' Tvh zimní ovocný MIX 4x6 ks 130g, 24ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('94008',' klasic 200g',23);
INSERT INTO products(Code, Name, Weight) VALUES('94010',' švestka a skořice 200g',23);
INSERT INTO products(Code, Name, Weight) VALUES('94011',' mexico 200g',23);
INSERT INTO products(Code, Name, Weight) VALUES('5054',' jogurt 4% mix (jahoda+borůvka, meruňka/med+citron/limeta) 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5313','0% mix jahoda, vanilka 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('5314','0% mix višeň, ostružina 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('63025','Retro ovocný tvh JAHODA 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('63026','Retro ovocný tvh BORŮVKA 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('63043',' dezert jahoda+ananas+višeň 140g,16ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('64038','KLC tvh odtučněný 500g',51);
INSERT INTO products(Code, Name, Weight) VALUES('64039','KLC tvh odtučněný 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('64063','KLC tvh polotučný 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('64066','KLC tvh tučný 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('6423',' jogurt 5% bílý kbelík 1kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('6535',' žervé pažitka + paprika 80g',9);
INSERT INTO products(Code, Name, Weight) VALUES('66022','DĚTSKÝ TVH kakao 90g 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66023','DĚTSKÝ TVH vanilka 90g, 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('6617',' Tvh pečené jablko, skořice/hruška, skořice 130g, 24ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('3030','BIO Mozzarella třešničky 10 ks',11);
INSERT INTO products(Code, Name, Weight) VALUES('32227','Tolštejn 45% sýr s tvorbou ok, bloček cca 200g',21);
INSERT INTO products(Code, Name, Weight) VALUES('5034',' jogurt 0% vanilka 3x140g',43);
INSERT INTO products(Code, Name, Weight) VALUES('5106','Můj Skyr bílý 0% 4x140g',57);
INSERT INTO products(Code, Name, Weight) VALUES('64010',' Tvh zahradní směs 3x130g',40);
INSERT INTO products(Code, Name, Weight) VALUES('64011',' Tvh paprika 3x130g',40);
INSERT INTO products(Code, Name, Weight) VALUES('6428',' čerstvý smetanový sýr 3kg',301);
INSERT INTO products(Code, Name, Weight) VALUES('6640',' tvh pečené jablko +  tvh hruška se skořicí 4x130g',53);
INSERT INTO products(Code, Name, Weight) VALUES('6643',' Tvh vanilkový+čokoládový 4x130g',53);
INSERT INTO products(Code, Name, Weight) VALUES('6647',' Tvh borůvka/lesní plody 4x130g',53);
INSERT INTO products(Code, Name, Weight) VALUES('6648',' Tvh jahoda/citron+limetka 4x130g',53);
INSERT INTO products(Code, Name, Weight) VALUES('2200','Sušené mléko odstředěné 25kg',101);
INSERT INTO products(Code, Name, Weight) VALUES('32243','Gouda 48% bloček 200g',21);
INSERT INTO products(Code, Name, Weight) VALUES('32212','Sýrový balíček, 485g (gouda, bonbony, uz. , BIO 3v1)',41);
INSERT INTO products(Code, Name, Weight) VALUES('32294','Kozí  sýr kostka',101);
INSERT INTO products(Code, Name, Weight) VALUES('4120','Máslo bloky 10kg VRN',101);
INSERT INTO products(Code, Name, Weight) VALUES('64081','Bio tvh s jogurtem jahoda 140g, 16ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('64083','Bio tvh s jogurtem brusinka 140g, 16ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('6460',' Tvh polotučný 250g, 20ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('6464','My Price tvh odtučněný 250g 20ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('65042','Termixík MIX cheese+jahoda 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('6612',' Tvh vanilkový 130g, 24ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('3021','Naše Bio  sýr uzený kostka',101);
INSERT INTO products(Code, Name, Weight) VALUES('3033','Naše Bio mozzarella třešničky 10 ks',11);
INSERT INTO products(Code, Name, Weight) VALUES('32257','Mozzarella 2x130g',27);
INSERT INTO products(Code, Name, Weight) VALUES('32271','Balkánský sýr kbelík 500g',51);
INSERT INTO products(Code, Name, Weight) VALUES('32355','Eidam 30% plátky 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('32356','Eidam uzený 30% plátky 100g',11);
INSERT INTO products(Code, Name, Weight) VALUES('5105','0% bílý 8ks/krt 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5107','0% ostružina 8ks/krt 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5109','0% malina 8ks/krt 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5112','0% višeň 8ks/krt 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5114','0% jahoda 8ks/krt 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('62362','Smetana ke šlehání 33% 200g, 8 ks/krt',21);
INSERT INTO products(Code, Name, Weight) VALUES('64021','Mekky tv.folie 250g Varn',26);
INSERT INTO products(Code, Name, Weight) VALUES('64022','Tvaroh měkký folie 250 g ČE',26);
INSERT INTO products(Code, Name, Weight) VALUES('64093','Bio tvh s jogurtem MIX jahoda+brusinka 140g, 16ks/krt',15);
INSERT INTO products(Code, Name, Weight) VALUES('66000',' kakaový 90g, 8ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66007',' smetanový 90g, 16ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66008',' smetanový 90g, 8ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66011',' vanilkový 90g, 8ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66012',' vanilkový 90g, 16ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66013',' kakaový 90g, 16ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('6631',' Tvh ovocný MIX 4x6 ks 130g, 24ks/krt',14);
INSERT INTO products(Code, Name, Weight) VALUES('75001','Tvh folie 250g',26);
INSERT INTO products(Code, Name, Weight) VALUES('5067',' jogurt 5% mix (vanilka+čokoláda, jahoda+borůvka) 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5117','0% mix jahoda, višeň 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5119','0% mix malina, ostružina 16ks/krt 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5123','0% mix jah,viš,ostr 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('5315','0% mix jah, van, viš, ostr 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('64020',' Tvh MIX paprika, zahradní směs 130g',14);
INSERT INTO products(Code, Name, Weight) VALUES('64042',' Tvh měkký 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64064',' Tvh polotučný 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('64067',' Tvh tučný 250g, 24ks/krt',26);
INSERT INTO products(Code, Name, Weight) VALUES('66017',' MIX kakao+vanilka+smetana 90g, 24ks/krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('66060',' MIX kakao+vanilka+smetana 100g, 24ks/krt',11);
INSERT INTO products(Code, Name, Weight) VALUES('7010',' jogurt DIP mix 140g',15);
INSERT INTO products(Code, Name, Weight) VALUES('3052',' Sýrové tyčky 4*20g',9);
INSERT INTO products(Code, Name, Weight) VALUES('65008','Termix mix 12xK, 12xV, 90g/24 krt',10);
INSERT INTO products(Code, Name, Weight) VALUES('32241','Gouda 48%',291);
INSERT INTO products(Code, Name, Weight) VALUES('4126','Kozí máslo 150g, 16ks/kar',16);
";
        #endregion
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO warehouses(Name) VALUES('Hlavni sklad')";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO locations(WarehouseId, Name, WeightCapacity) VALUES(1, 'P-0001', 10)";
        cmd.ExecuteNonQuery();

        cmd.CommandText = "INSERT INTO productslocations(LocationId, ProductId, Amount) VALUES(1,1, 2)";
        cmd.ExecuteNonQuery();
    } 
}