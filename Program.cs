using defontana.Repositorio;
using defontana.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration =  new ConfigurationBuilder()
.SetBasePath(Environment.CurrentDirectory)
     .AddJsonFile("appsettings.json");

var config = configuration.Build();

var optionsBuilder = new DbContextOptionsBuilder<PruebaContext>()
            .UseSqlServer(config.GetConnectionString("DefontanaTest"));

var db = new PruebaContext(optionsBuilder.Options);
var d = new Repo(db);
Console.WriteLine("Cargando informacion de las ventas en los ultimos 30 dias...:\n");
var data = new Data(db, 30);

//Info ventas
Console.WriteLine("- " + data.DataVentas);
//--------------------------------------

//Mejor venta
Console.WriteLine("- " + data.BestDiaVenta);
/////////////////////////////

//Mejor Producto
Console.WriteLine("- " + data.BestProducto);
//////------------------------------

//Mejor Local
var bestLocal = data.BestLocal;
Console.WriteLine("- Mejor Local: {0} | Monto de Ventas: {1}",
        bestLocal.Item1.IdLocal + " - " + bestLocal.Item1.Nombre,
        bestLocal.Item2);

Console.Write("\nPresiona Enter para finalizar...");
Console.ReadLine();

