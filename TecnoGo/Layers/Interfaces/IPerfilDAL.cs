using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.Interfaces
{
    public interface IPerfilDAL
    {
        List<Perfil> GetAll();
    }
}
