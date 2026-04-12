using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Enumeraciones;

namespace TecnoGo.Layers.Entities
{
    public class Cliente
    {
        public string Id { set; get; }
        public TipoIdentificacion TipoIdentificacion { set; get; }
        public string Nombre { set; get; }
        public string Apellido1 { set; get; }
        public string Apellido2 { set; get; }
        public string Sexo { set; get; }
        public string Telefono { set; get; }
        public string Correo { set; get; }
        public string Provincia { set; get; }
        public string Direccion { set; get; }
        public byte[] Fotografia { set; get; }
        public EstadoGeneral Estado { set; get; }
        public override string ToString() => $"{Nombre} {Apellido1} {Apellido2}";
    }
}
