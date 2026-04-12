using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Enumeraciones;

namespace TecnoGo.Layers.Entities
{
    public class TipoDispositivo
    {
        public int Id { set; get; }
        public string Nombre { set; get; }
        public EstadoGeneral Estado { set; get; }
    }

}
