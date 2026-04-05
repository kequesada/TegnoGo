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
    public class ProveedorBLL : IProveedorBLL
    {
        public List<Proveedor> GetByFilter(string pNombre)
        {
            IProveedorDAL dal = new ProveedorDAL();
            return dal.GetByFilter(pNombre);
        }

        public Proveedor GetById(int pId)
        {
            IProveedorDAL dal = new ProveedorDAL();
            return dal.GetById(pId);
        }

        public Task<IEnumerable<Proveedor>> GetAll()
        {
            IProveedorDAL dal = new ProveedorDAL();
            return dal.GetAll();
        }

        public Task<Proveedor> Save(Proveedor pProveedor)
        {
            IProveedorDAL dal = new ProveedorDAL();
            Task<Proveedor> oProveedor = null;

            if (dal.GetById(pProveedor.Id) == null)
                oProveedor = dal.Save(pProveedor);
            else
                oProveedor = dal.Update(pProveedor);

            return oProveedor;
        }

        public Task<bool> Delete(int pId)
        {
            IProveedorDAL dal = new ProveedorDAL();
            return dal.Delete(pId);
        }
    }
}
