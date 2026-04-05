using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.Interfaces
{
    public interface ITipoDispositivoDAL
    {
        List<TipoDispositivo> GetByFilter(string pNombre);
        TipoDispositivo GetById(int pId);
        Task<IEnumerable<TipoDispositivo>> GetAll();
        Task<TipoDispositivo> Save(TipoDispositivo pTipoDispositivo);
        Task<TipoDispositivo> Update(TipoDispositivo pTipoDispositivo);
        Task<bool> Delete(int pId);
    }

}
