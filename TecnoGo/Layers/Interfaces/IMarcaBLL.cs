using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.Interfaces
{
    public interface IMarcaBLL
    {
        List<Marca> GetByFilter(string pDescripcion);
        Marca GetById(int pId);
        Task<IEnumerable<Marca>> GetAll();
        Task<Marca> Save(Marca pMarca);
        Task<bool> Delete(int pId);
    }
}
