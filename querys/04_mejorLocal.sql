SELECT top 1 sum(v.total) as monto, l.ID_Local,  max(l.Nombre) as nombre
from venta v
join local l on v.ID_Local = l.ID_Local
where v.fecha >= Convert(varchar, DATEADD(DAY, -30, GETDATE()), 23)
group by l.ID_Local
order by monto desc