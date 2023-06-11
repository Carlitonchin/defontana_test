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

public class BestLocal{
    int Monto {get;}
    Local Local{get;}

    public BestLocal(int monto, Local local){
        this.Monto = monto;
        this.Local = local;
    }

    public override string ToString()
    {
        return String.Format("Mejor Local: {0} | Monto de Ventas: {1}",
        this.Local.IdLocal + " - " + this.Local.Nombre,
        this.Monto);
    }
}

public class BestMarca{
    int Costo{get;}
    int Ventas{get;}
    Marca Marca{get;}

    public BestMarca(int costo, int ventas, Marca marca){
        this.Costo = costo;
        this.Ventas = ventas;
        this.Marca = marca;
    }

    public override string ToString()
    {
        return String.Format("Mejor Marca: {0} | Costo: {1} | Ventas {2} | Margen de ganancia {3}",
        this.Marca.IdMarca + " - " + this.Marca.Nombre,
        this.Costo, this.Ventas, this.Ventas - this.Costo);
    }
}

public class DataLocal{
    public Local Local{get;}
    public Producto Producto{get;}
    public int UnidadesVendidas{get;}

    public DataLocal(Local local, Producto producto, int cantUnidades){
        this.Local = local;
        this.Producto = producto;
        this.UnidadesVendidas = cantUnidades;
    }

    public override string ToString()
    {
        return String.Format("{0} --> {1} ({2} unidades vendidas)",
        this.Local.IdLocal + " - " + this.Local.Nombre,
        this.Producto.IdProducto + " - " + this.Producto.Nombre,
        this.UnidadesVendidas);
    }
}