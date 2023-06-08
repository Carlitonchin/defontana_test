using defontana.Models;

namespace defontana.Repositorio;

public class Repositorio{
    private PruebaContext db;
    private IEnumerable<VentaDetalle> baseData;
    private IEnumerable<VentaDetalle> getLastDaysVentaDetalles(int days){
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
    public Repositorio(PruebaContext db, int days){
        this.db =db;
        this.baseData = this.getLastDaysVentaDetalles(days);
    }

    public Tuple<int, int> GetDataVentas(){
        var totalVentas = this.baseData.DistinctBy(d=>d.IdVenta).Count();
        var montoTotal = this.baseData.Sum(d=>d.PrecioUnitario * d.Cantidad);

        return new Tuple<int, int>(montoTotal, totalVentas);
    }
}