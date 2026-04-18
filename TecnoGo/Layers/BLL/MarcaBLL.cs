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
    public class MarcaBLL : IMarcaBLL
    {
        public List<Proveedor> GetByFilter(string pNombre)
        {
            IMarcaDAL dalMarca = new MarcaDAL();
            return dalMarca.GetByFilter(pNombre);
        }

        public Proveedor GetById(int pId)
        {
            IMarcaDAL dalMarca = new MarcaDAL();
            return dalMarca.GetById(pId);
        }

        public Task<IEnumerable<Proveedor>> GetAll()
        {
            IMarcaDAL dalMarca = new MarcaDAL();
            return dalMarca.GetAll();
        }

        public Task<Proveedor> Save(Proveedor pMarca)
        {
            IMarcaDAL dalMarca = new MarcaDAL();
            Task<Proveedor> oMarca = null;

            if (dalMarca.GetById(pMarca.Id) == null)
                oMarca = dalMarca.Save(pMarca);
            else
                oMarca = dalMarca.Update(pMarca);

            return oMarca;
        }

        public Task<bool> Delete(int pId)
        {
            IMarcaDAL dalMarca = new MarcaDAL();
            return dalMarca.Delete(pId);
        }
    }
}
