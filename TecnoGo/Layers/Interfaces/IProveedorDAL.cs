using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.Interfaces
{
    public interface IProveedorDAL
    {
        List<Proveedor> GetByFilter(string pDescripcion);
        Proveedor GetById(int pId);
        Task<IEnumerable<Proveedor>> GetAll();
        Task<Proveedor> Save(Proveedor pProveedor);
        Task<Proveedor> Update(Proveedor pProveedor);
        Task<bool> Delete(int pId);
    }

}
