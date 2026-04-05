using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.DAL;
using TecnoGo.Layers.Entities;
using TecnoGo.Layers.Entities.DTO;
using TecnoGo.Layers.Interfaces;

namespace TecnoGo.Layers.BLL
{
    public class FacturaBLL : IFacturaBLL
    {
        public int GetNextNumeroFactura()
        {
            IFacturaDAL dal = new FacturaDAL();
            return dal.GetNextNumeroFactura();
        }

        public int GetCurrentNumeroFactura()
        {
            IFacturaDAL dal = new FacturaDAL();
            return dal.GetCurrentNumeroFactura();
        }

        public FacturaEncabezado Save(FacturaEncabezado pFactura)
        {
            IFacturaDAL dal = new FacturaDAL();
            IProductoBLL bllProducto = new ProductoBLL();

            // 1. Validar stock
            foreach (var det in pFactura.ListaDetalles)
            {
                bllProducto.ValidarExistencia(det.IdProducto, det.Cantidad);
            }

            // 2. Calcular detalle (IVA constante)
            foreach (var det in pFactura.ListaDetalles)
            {
                det.Subtotal = det.Precio * det.Cantidad;
                det.Impuesto = det.Subtotal * ConstantesImpuestos.IVA_CR; // 13%
                det.Total = det.Subtotal + det.Impuesto;
            }

            // 3. Calcular encabezado
            pFactura.Subtotal = pFactura.ListaDetalles.Sum(d => d.Subtotal);
            pFactura.Impuesto = pFactura.ListaDetalles.Sum(d => d.Impuesto);
            pFactura.TotalCRC = pFactura.Subtotal + pFactura.Impuesto;
            pFactura.TotalUSD = pFactura.TotalCRC / pFactura.TipoCambio;

            // 4. Guardar en BD
            return dal.Save(pFactura);
        }

        public double GetTotalFactura(double pId)
        {
            IFacturaDAL dal = new FacturaDAL();
            return dal.GetTotalFactura(pId);
        }

        public double CalculateTax(double precio, int cantidad)
        {
            return precio * cantidad * ConstantesImpuestos.IVA_CR;
        }

        public async Task<IEnumerable<VentasDTO>> GetTotalVentasXFecha(DateTime pFechaInicial, DateTime pFechaFinal)
        {
            IFacturaDAL dal = new FacturaDAL();
            return await dal.GetTotalVentasXFecha(pFechaInicial, pFechaFinal);
        }
    }
}
