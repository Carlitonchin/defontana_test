with data_locales as (
select l.id_local, sum(vd.Cantidad) as unidades, vd.ID_Producto from VentaDetalle vd 
join Venta v on v.ID_Venta = vd.ID_Venta
join Local l on v.id_local = l.id_local
and v.fecha >= Convert(varchar, DATEADD(DAY, -30, GETDATE()), 23)
group by l.id_local,vd.ID_Producto
)

Select id_local, 
(
	select top 1 ID_Producto 
	from data_locales subdl 
	where subdl.unidades = max(dl.unidades) and dl.ID_Local = subdl.ID_Local
	order by subdl.id_producto
) as id_producto,
max(unidades) as unidades_vendidas
from data_locales as dl
group by ID_Local
order by id_local

