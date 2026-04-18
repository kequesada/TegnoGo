using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.Interfaces
{
    public interface IMarcaDAL
    {
        List<Proveedor> GetByFilter(string pNombre);
        Proveedor GetById(int pId);
        Task<IEnumerable<Proveedor>> GetAll();
        Task<Proveedor> Save(Proveedor pMarca);
        Task<Proveedor> Update(Proveedor pMarca);
        Task<bool> Delete(int pId);
    }

}
