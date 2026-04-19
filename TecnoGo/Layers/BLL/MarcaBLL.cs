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
        public List<Marca> GetByFilter(string pNombre)
        {
            IMarcaDAL dal = new MarcaDAL();
            return dal.GetByFilter(pNombre);
        }

        public Marca GetById(int pId)
        {
            IMarcaDAL dal = new MarcaDAL();
            return dal.GetById(pId);
        }

        public Task<IEnumerable<Marca>> GetAll()
        {
            IMarcaDAL dal = new MarcaDAL();
            return dal.GetAll();
        }

        public Task<Marca> Save(Marca pMarca)
        {
            IMarcaDAL dal = new MarcaDAL();
            Task<Marca> oMarca = null;

            if (dal.GetById(pMarca.Id) == null)
                oMarca = dal.Save(pMarca);
            else
                oMarca = dal.Update(pMarca);

            return oMarca;
        }

        public Task<bool> Delete(int pId)
        {
            IMarcaDAL dal = new MarcaDAL();
            return dal.Delete(pId);
        }
    }
}
