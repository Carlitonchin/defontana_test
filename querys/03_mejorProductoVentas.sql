SELECT top 1 sum(vd.Precio_Unitario*vd.Cantidad) as monto, p.ID_Producto, max(p.Nombre) as nombre_producto
from VentaDetalle vd
join venta v on v.ID_Venta = vd.ID_Venta
join Producto p on p.id_producto = vd.id_producto
where v.fecha >= Convert(varchar, DATEADD(DAY, -30, GETDATE()), 23)
group by p.ID_Producto
order by monto desc