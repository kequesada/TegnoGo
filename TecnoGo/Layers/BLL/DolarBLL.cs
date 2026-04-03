using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.DAL;
using TecnoGo.Layers.Entities.BCCR;

namespace TecnoGo.Layers.BLL
{
	class DolarBLL
	{
        public double GetVentaDolar()
        { 
            DolarDAL dolar = new DolarDAL();
            return dolar.GetVentaDolar();
        }
    }
}
