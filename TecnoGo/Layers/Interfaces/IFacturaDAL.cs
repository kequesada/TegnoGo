using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;
using TecnoGo.Layers.Entities.DTO;

namespace TecnoGo.Layers.Interfaces
{
    public interface IFacturaDAL
    {
        FacturaEncabezado Save(FacturaEncabezado pFactura);
        int GetNextNumeroFactura();
        int GetCurrentNumeroFactura();
        double GetTotalFactura(double pIdFactura);
        Task<IEnumerable<VentasDTO>> GetTotalVentasXFecha(DateTime pFechaInicial, DateTime pFechaFinal);
        FacturaEncabezado GetFactura(double pIdFactura);
    }
}
