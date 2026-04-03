using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Layers.Entities.BCCR;

namespace TecnoGo.Layers.DAL
{
    class DolarDAL
    {
        public double GetVentaDolar()
        {
            try
            {
                double tipoCambioVenta = 1;
                ServicesBCCR services = new ServicesBCCR();
                List<Dolar> cambioDolar = services.GetDolar(DateTime.Today, DateTime.Today, "v").ToList();
                if (cambioDolar != null)
                {
                    tipoCambioVenta = cambioDolar[0].Monto;
                }
                return tipoCambioVenta;
            }
            catch (Exception ex)
            {
                String error = ex.Message;
                return 0;
            }

        }
    }
}
