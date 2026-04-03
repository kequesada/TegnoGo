using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.Interfaces
{
    interface IProvinciaBLL
    {
        List<Provincia> GetAll();
        Provincia GetById(int pId);
        Provincia Save(Provincia pProvincia);
        Provincia GetProvinciaFromInternet(int pId);
        bool Delete(int pId);
    }
}
