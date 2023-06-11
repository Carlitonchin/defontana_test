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
        var bestProductGroup = 
        this.baseData.GroupBy(m=>m.IdProducto)
        .OrderByDescending(g=>g.Sum(v=>v.PrecioUnitario*v.Cantidad))
        .FirstOrDefault();

        if(bestProductGroup == null)
            return new BestProducto(0,new Producto());

        int monto = bestProductGroup.Sum(m=>m.Cantidad*m.PrecioUnitario);
        Producto prod = bestProductGroup.First().IdProductoNavigation;

        return new BestProducto(monto,prod);
    }}
    public BestLocal BestLocal{
        get{
            var bestLocalGroup = 
            this.baseData.DistinctBy(d=>d.IdVenta)
            .GroupBy(m=>m.IdVentaNavigation.IdLocal)
            .OrderByDescending(g=>g.Sum(v=>v.IdVentaNavigation.Total))
            .FirstOrDefault();

            if(bestLocalGroup == null)
                return new BestLocal(0,new Local());

            int monto = bestLocalGroup.Sum(g=>g.IdVentaNavigation.Total);
            Local local = bestLocalGroup.First().IdVentaNavigation.IdLocalNavigation;

            return new BestLocal(monto, local);
        }}

    public BestMarca BestMarca{
        get{
            var bestMarcaGroup = 
            this.baseData.GroupBy(m=>m.IdProductoNavigation.IdMarca)
            .OrderByDescending(g=>g.Sum(m=>{
                return m.Cantidad*m.PrecioUnitario - (m.Cantidad * m.IdProductoNavigation.CostoUnitario);
            })).FirstOrDefault();

            if(bestMarcaGroup == null)
            return new BestMarca(0,0, new Marca());

            var costo = bestMarcaGroup.Sum(g=>g.Cantidad*g.IdProductoNavigation.CostoUnitario);
            var ventas = bestMarcaGroup.Sum(g=>g.Cantidad*g.PrecioUnitario);
            var marca = bestMarcaGroup.First().IdProductoNavigation.IdMarcaNavigation;

            return new BestMarca(costo, ventas, marca);
        }
    }
}