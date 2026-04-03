using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.Interfaces
{
    public interface IUsuarioBLL
    {
        Usuario Login(string pLogin, string pPassword);
        IEnumerable<Usuario> GetAll();
        Usuario GetById(string pLogin);
        Usuario Save(Usuario pUsuario);
        bool Delete(string pLogin);
    }
}
