using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.Interfaces
{
    public interface IInventarioDAL
    {
        List<Inventario> GetByFilter(string pObservaciones);
        Inventario GetById(int pId);
        Task<IEnumerable<Inventario>> GetAll();
        Task<Inventario> Save(Inventario pInventario);
        Task<Inventario> Update(Inventario pInventario);
        Task<bool> Delete(int pId);
    }
}
