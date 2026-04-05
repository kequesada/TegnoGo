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
    public class InventarioBLL : IInventarioBLL
    {
        public List<Inventario> GetByFilter(string pObservaciones)
        {
            IInventarioDAL dal = new InventarioDAL();
            return dal.GetByFilter(pObservaciones);
        }

        public Inventario GetById(int pId)
        {
            IInventarioDAL dal = new InventarioDAL();
            return dal.GetById(pId);
        }

        public Task<IEnumerable<Inventario>> GetAll()
        {
            IInventarioDAL dal = new InventarioDAL();
            return dal.GetAll();
        }

        public Task<Inventario> Save(Inventario pInventario)
        {
            IInventarioDAL dal = new InventarioDAL();
            Task<Inventario> oInventario = null;

            if (dal.GetById(pInventario.Id) == null)
                oInventario = dal.Save(pInventario);
            else
                oInventario = dal.Update(pInventario);

            return oInventario;
        }

        public Task<bool> Delete(int pId)
        {
            IInventarioDAL dal = new InventarioDAL();
            return dal.Delete(pId);
        }
    }
}
