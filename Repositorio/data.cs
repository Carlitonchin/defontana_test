using defontana.Models;

namespace defontana.Repositorio;

public class Data{
    private IEnumerable<VentaDetalle> baseData;
    public Data(PruebaContext db, int days){
        var repo = new Repo(db);
        this.baseData = repo.getLastDaysVentaDetalles(days);
    }

    public DataVentas DataVentas{
        get{
        var ventas = this.baseData.DistinctBy(d=>d.IdVenta);
        var totalVentas = ventas.Count();
        var montoTotal = ventas.Sum(d=>d.IdVentaNavigation.Total);

        return new DataVentas(totalVentas, montoTotal);
    }
    }

    public Tuple<DateTime, int> BestVenta {
        get{
        var best = this.baseData.MaxBy(m=>m.IdVentaNavigation.Total);
        if(best == null)
            return new Tuple<DateTime, int>(DateTime.MinValue,0);

        return new Tuple<DateTime, int>(best.IdVentaNavigation.Fecha, best.IdVentaNavigation.Total);
    }
    }

    public Tuple<Producto, int> BestProducto{
        get{
        var groupProducts = this.baseData.GroupBy(m=>m.IdProducto);
        var bestProductGroup = groupProducts.OrderByDescending(g=>g.Sum(v=>v.PrecioUnitario*v.Cantidad))
                                        .FirstOrDefault();

        if(bestProductGroup == null)
            return new Tuple<Producto, int>(new Producto(),0);

        int monto = bestProductGroup.Sum(m=>m.Cantidad*m.PrecioUnitario);
        Producto prod = bestProductGroup.First().IdProductoNavigation;

        return new Tuple<Producto, int>(prod, monto);
    }}

    public Tuple<Local, int> BestLocal{
        get{
            var ventas = this.baseData.DistinctBy(d=>d.IdVenta);
            var groupLocals = ventas.GroupBy(m=>m.IdVentaNavigation.IdLocal);
            var bestLocalGroup = groupLocals.OrderByDescending(g=>g.Sum(v=>v.IdVentaNavigation.Total))
                                    .FirstOrDefault();

            if(bestLocalGroup == null)
                return new Tuple<Local, int>(new Local(),0);

            int monto = bestLocalGroup.Sum(g=>g.IdVentaNavigation.Total);
            Local local = bestLocalGroup.First().IdVentaNavigation.IdLocalNavigation;

            return new Tuple<Local, int>(local, monto);
        }}
}