using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.Interfaces
{
    public interface IClienteBLL
    {
        List<Cliente> GetByFilter(string pDescripcion);
        Cliente GetById(string pId);
        Task<IEnumerable<Cliente>> GetAll();
        Task<Cliente> Save(Cliente pCliente);
        Task<bool> Delete(string pId);
    }
}
