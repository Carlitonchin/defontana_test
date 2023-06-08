using defontana.Models;

namespace Repositorio;
internal class Repo{
    private PruebaContext db;

    internal Repo(PruebaContext db){
        this.db = db;
    }
    internal IEnumerable<VentaDetalle> getLastDaysVentaDetalles(int days){
        DateTime today = DateTime.Now;
        today = new DateTime(today.Year, today.Month, today.Day,0,0,0,0,0);
        DateTime pastDays = today.Subtract(TimeSpan.FromDays(days));
        Console.WriteLine(pastDays);
        var resp = from ventaDetalle in db.VentaDetalles 
                    join venta in db.Venta on ventaDetalle.IdVenta equals venta.IdVenta
                    join producto in db.Productos on ventaDetalle.IdProducto equals producto.IdProducto
                    join local in db.Locals on venta.IdLocal equals local.IdLocal
                    join marca in db.Marcas on producto.IdMarca equals marca.IdMarca
                    where venta.Fecha >= pastDays

                    select new VentaDetalle{
                        Cantidad=ventaDetalle.Cantidad,
                        IdProducto=ventaDetalle.IdProducto,
                        IdVenta=ventaDetalle.IdVenta,
                        IdVentaDetalle=ventaDetalle.IdVentaDetalle,
                        PrecioUnitario=ventaDetalle.PrecioUnitario,
                        TotalLinea=ventaDetalle.TotalLinea,
                        IdProductoNavigation=new Producto{
                            Codigo=producto.Codigo,
                            CostoUnitario=producto.CostoUnitario,
                            IdMarca=producto.IdMarca,
                            IdProducto=producto.IdProducto,
                            Modelo=producto.Modelo,
                            Nombre=producto.Nombre,
                            IdMarcaNavigation=new Marca{
                                IdMarca=producto.IdMarca,
                                Nombre=marca.Nombre,
                            }
                        },
                        IdVentaNavigation=new Ventum{
                            Fecha=venta.Fecha,
                            IdLocal=venta.IdLocal,
                            IdVenta=ventaDetalle.IdVentaDetalle,
                            Total=venta.Total,
                            IdLocalNavigation=new Local{
                                Direccion=local.Direccion,
                                IdLocal=venta.IdLocal,
                                Nombre=local.Nombre,
                            }
                        }
                    };

        return resp;
    }
}