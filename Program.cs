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
var data = new Data(db, 30);
Console.WriteLine("Informacion de las ventas en los ultimos 30 dias:");
Console.WriteLine("...");
//Info ventas
var infoVentas = data.DataVentas;
Console.WriteLine("- " + infoVentas);
//--------------------------------------

//Mejor venta
var bestVenta = data.BestVenta;
Console.WriteLine("- Mejor Dia en Ventas: Fecha:{0} | Valor: {1}", bestVenta.Item1, bestVenta.Item2);
/////////////////////////////

//Mejor Producto
var bestProduct = data.BestProducto;
Console.WriteLine("- Mejor Producto: {0} | Monto de Ventas: {1}", 
          bestProduct.Item1.IdProducto + " - " + bestProduct.Item1.Nombre,
          bestProduct.Item2);
//////------------------------------

//Mejor Local
var bestLocal = data.BestLocal;
Console.WriteLine("- Mejor Local: {0} | Monto de Ventas: {1}",
        bestLocal.Item1.IdLocal + " - " + bestLocal.Item1.Nombre,
        bestLocal.Item2);

