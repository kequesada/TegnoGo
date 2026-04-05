using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using TecnoGo.Enumeraciones;

namespace TecnoGo.Layers.Entities
{
    public class FacturaEncabezado
    {
        public int Id { set; get; }
        public string IdCliente { set; get; }
        public int IdUsuario { set; get; }
        public DateTime FechaFactura { set; get; }
        public double Subtotal { set; get; }
        public double Impuesto { set; get; }
        public double TotalCRC { set; get; }
        public double TotalUSD { set; get; }
        public double TipoCambio { set; get; }
        public TipoPago TipoPago { set; get; }
        public string Documento { set; get; }
        public Banco Banco { set; get; }
        public TipoTarjeta TipoTarjeta { set; get; }
        public byte[] FirmaCliente { set; get; }
        public System.Web.UI.WebControls.Xml XmlFactura { set; get; }
        public EstadoFactura Estado { set; get; }
        public List<FacturaDetalle> ListaDetalles { get; set; } = new List<FacturaDetalle>();
    }
}
