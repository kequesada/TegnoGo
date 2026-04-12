using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Enumeraciones;

namespace TecnoGo.Layers.Entities
{
    public class Usuario
    {
        public int Id { set; get; }
        public int IdPerfil { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
        public string Nombre { set; get; }
        public EstadoGeneral Estado { set; get; }

        public override string ToString() => $"{Login.Trim()} - {Nombre.Trim()}";
    }
}
