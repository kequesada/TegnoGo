using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.DAL;
using TecnoGo.Layers.Entities;
using TecnoGo.Layers.Interfaces;

namespace TecnoGo.Layers.BLL
{
    public class PerfilBLL : IPerfilBLL
    {
        public List<Perfil> GetAll()
        {
            IPerfilDAL dalPerfil = new PerfilDAL();
            return dalPerfil.GetAll();
        }
    }
}
