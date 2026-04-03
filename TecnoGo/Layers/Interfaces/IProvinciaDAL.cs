using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.Interfaces
{
    interface IProvinciaDAL
    {
        List<Provincia> GetAll();
        Provincia GetById(int pId);
        Provincia Save(Provincia pBodega);
        Provincia Update(Provincia pBodega);
        bool Delete(int pId);
    }
}
