DECLARE @mayorPrecio int;
SET @mayorPrecio = (SELECT max(total) FROM Venta where fecha >= Convert(varchar, DATEADD(DAY, -30, GETDATE()), 23));

select total as Valor, Fecha
from venta
where fecha >= Convert(varchar, DATEADD(DAY, -30, GETDATE()), 23) and total = @mayorPrecio