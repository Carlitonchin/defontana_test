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