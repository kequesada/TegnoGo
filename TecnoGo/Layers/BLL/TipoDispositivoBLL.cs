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
    public class TipoDispositivoBLL : ITipoDispositivoBLL
    {
        public List<TipoDispositivo> GetByFilter(string pNombre)
        {
            ITipoDispositivoDAL dal = new TipoDispositivoDAL();
            return dal.GetByFilter(pNombre);
        }

        public TipoDispositivo GetById(int pId)
        {
            ITipoDispositivoDAL dal = new TipoDispositivoDAL();
            return dal.GetById(pId);
        }

        public Task<IEnumerable<TipoDispositivo>> GetAll()
        {
            ITipoDispositivoDAL dal = new TipoDispositivoDAL();
            return dal.GetAll();
        }

        public Task<TipoDispositivo> Save(TipoDispositivo pTipo)
        {
            ITipoDispositivoDAL dal = new TipoDispositivoDAL();
            Task<TipoDispositivo> oTipo = null;

            if (dal.GetById(pTipo.Id) == null)
                oTipo = dal.Save(pTipo);
            else
                oTipo = dal.Update(pTipo);

            return oTipo;
        }

        public Task<bool> Delete(int pId)
        {
            ITipoDispositivoDAL dal = new TipoDispositivoDAL();
            return dal.Delete(pId);
        }
    }
}
