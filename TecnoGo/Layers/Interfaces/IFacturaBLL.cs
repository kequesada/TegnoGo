using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;
using TecnoGo.Layers.Entities.DTO;

namespace TecnoGo.Layers.Interfaces
{
    public interface IFacturaBLL
    {
        FacturaEncabezado Save(FacturaEncabezado pFactura);
        int GetNextNumeroFactura();
        int GetCurrentNumeroFactura();
        double GetTotalFactura(double pIdFactura);
        double CalculateTax(double precio, int cantidad);
        Task<IEnumerable<VentasDTO>> GetTotalVentasXFecha(DateTime pFechaInicial, DateTime pFechaFinal);
    }
}
