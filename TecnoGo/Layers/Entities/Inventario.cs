using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Enumeraciones;

namespace TecnoGo.Layers.Entities
{
    public class Inventario
    {
        public int Id { set; get; }
        public EstadoInventario TipoEstado { set; get; }
        public int IdProducto { set; get; }
        public int Cantidad { set; get; }
        public string Observaciones { set; get; }
        public DateTime FechaRegistro { set; get; }
    }

}
