using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoGo.Layers.Entities
{
    public class FacturaDetalle
    {
        public int Id { set; get; }
        public int IdFactura { set; get; }
        public int IdProducto { set; get; }
        public int Cantidad { set; get; }
        public double Precio { set; get; }
        public double Subtotal { set; get; }
        public double Impuesto { set; get; }
        public double Total { set; get; }
    }

}
