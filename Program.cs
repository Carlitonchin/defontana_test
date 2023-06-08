using defontana.Repositorio;
using Microsoft.Extensions.Logging;
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

var repo = new Repositorio(db, 30);

