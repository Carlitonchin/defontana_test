using defontana.Models;
namespace defontana.Repositorio;
public class DataVentas{
    int Total {get;}
    int Monto {get;}

    public DataVentas(int total, int monto){
        this.Total = total;
        this.Monto = monto;
    }

    public override string ToString()
    {
        return String.Format("Monto: {0} | Cantidad de ventas: {1}", this.Monto, this.Total);
    }
}

public class BestDiaVenta{
    int Monto {get;}
    DateTime Fecha {get;}

    public BestDiaVenta(int monto, DateTime fecha){
        this.Monto = monto;
        this.Fecha = fecha;
    }

    public override string ToString()
    {
        return String.Format("Mejor Dia en Ventas: Fecha:{0} | Valor: {1}", this.Fecha, this.Monto);
    }
}

public class BestProducto{
    int Monto {get;}
    Producto Producto {get;}

    public BestProducto(int monto, Producto producto){
        this.Monto = monto;
        this.Producto = producto;
    }

    public override string ToString()
    {
        return String.Format("Mejor Producto: {0} | Monto de Ventas: {1}", 
        this.Producto.IdProducto + " - " + this.Producto.Nombre,
          this.Monto);
    }
}