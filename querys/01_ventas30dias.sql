select sum(total) as monto, count(id_venta) as total_ventas 
from Venta 
where fecha >= Convert(varchar, DATEADD(DAY, -30, GETDATE()), 23)