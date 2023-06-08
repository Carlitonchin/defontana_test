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
    public BestDiaVenta BestDiaVenta {
        get{
        var best = this.baseData.MaxBy(m=>m.IdVentaNavigation.Total);
        if(best == null)
            return new BestDiaVenta(0, DateTime.MinValue);

        return new BestDiaVenta(best.IdVentaNavigation.Total,best.IdVentaNavigation.Fecha);
    }
    }
    public BestProducto BestProducto{
        get{
        var groupProducts = this.baseData.GroupBy(m=>m.IdProducto);
        var bestProductGroup = groupProducts.OrderByDescending(g=>g.Sum(v=>v.PrecioUnitario*v.Cantidad))
                                        .FirstOrDefault();

        if(bestProductGroup == null)
            return new BestProducto(0,new Producto());

        int monto = bestProductGroup.Sum(m=>m.Cantidad*m.PrecioUnitario);
        Producto prod = bestProductGroup.First().IdProductoNavigation;

        return new BestProducto(monto,prod);
    }}
    public BestLocal BestLocal{
        get{
            var ventas = this.baseData.DistinctBy(d=>d.IdVenta);
            var groupLocals = ventas.GroupBy(m=>m.IdVentaNavigation.IdLocal);
            var bestLocalGroup = groupLocals.OrderByDescending(g=>g.Sum(v=>v.IdVentaNavigation.Total))
                                    .FirstOrDefault();

            if(bestLocalGroup == null)
                return new BestLocal(0,new Local());

            int monto = bestLocalGroup.Sum(g=>g.IdVentaNavigation.Total);
            Local local = bestLocalGroup.First().IdVentaNavigation.IdLocalNavigation;

            return new BestLocal(monto, local);
        }}
}