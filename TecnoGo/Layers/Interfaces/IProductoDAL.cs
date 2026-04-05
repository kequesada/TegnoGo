using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.Interfaces
{
    public interface IProductoDAL
    {
        List<Producto> GetByFilter(string pNombre);
        Producto GetById(int pId);
        Task<IEnumerable<Producto>> GetAll();
        Task<Producto> Save(Producto pProducto);
        Task<Producto> Update(Producto pProducto);
        Task<bool> Delete(int pId);
    }

}
