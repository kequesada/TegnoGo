using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

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
        public string TipoPago { set; get; }
        public string Documento { set; get; }
        public string Banco { set; get; }
        public string TipoTarjeta { set; get; }
        public byte[] FirmaCliente { set; get; }
        public System.Web.UI.WebControls.Xml XmlFactura { set; get; }
        public string Estado { set; get; }
    }

}
