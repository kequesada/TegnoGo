using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecnoGo.Layers.Entities
{
    public class Producto
    {
        public int Id { set; get; }
        public string Nombre { set; get; }
        public int IdTipoDispositivo { set; get; }
        public int IdMarca { set; get; }
        public string Modelo { set; get; }
        public int IdProveedor { set; get; }
        public string Color { set; get; }
        public string Caracteristicas { set; get; }
        public string Extras { set; get; }
        public byte[] Fotografia { set; get; }
        public byte[] DocumentoEspecificaciones { set; get; }
        public int CantidadStock { set; get; }
        public double Precio { set; get; }
        public string Estado { set; get; }
    }

}
