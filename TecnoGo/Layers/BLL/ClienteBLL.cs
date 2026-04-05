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
    public class ClienteBLL : IClienteBLL
    {
        public List<Cliente> GetByFilter(string pNombre)
        {
            IClienteDAL dalCliente = new ClienteDAL();
            return dalCliente.GetByFilter(pNombre);
        }

        public Cliente GetById(string pId)
        {
            IClienteDAL dalCliente = new ClienteDAL();
            return dalCliente.GetById(pId);
        }

        public Task<IEnumerable<Cliente>> GetAll()
        {
            IClienteDAL dalCliente = new ClienteDAL();
            return dalCliente.GetAll();
        }

        public Task<Cliente> Save(Cliente pCliente)
        {
            IClienteDAL dalCliente = new ClienteDAL();
            Task<Cliente> oCliente = null;

            if (dalCliente.GetById(pCliente.Id) == null)
                oCliente = dalCliente.Save(pCliente);
            else
                oCliente = dalCliente.Update(pCliente);

            return oCliente;
        }

        public Task<bool> Delete(string pId)
        {
            IClienteDAL dalCliente = new ClienteDAL();
            return dalCliente.Delete(pId);
        }
    }

}
