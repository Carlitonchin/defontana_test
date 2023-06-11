select top 1 m.id_marca,max(m.nombre) as nombre,sum(p.costo_unitario*vd.Cantidad) as costo,
sum(vd.TotalLinea) as ventas, sum(vd.totallinea) - sum(p.Costo_Unitario * vd.Cantidad) as ganancias
from VentaDetalle vd
join Producto p on p.ID_Producto = vd.ID_Producto
join Marca m on m.id_marca = p.id_marca
join Venta v on v.id_venta = vd.ID_Venta
where v.fecha >= Convert(varchar, DATEADD(DAY, -30, GETDATE()), 23)
group by m.ID_Marca
order by ganancias desc