using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using TecnoGo.Layers.Entities;

namespace TecnoGo.Layers.BLL
{
    internal class HaciendaBLL
    {
        public async Task<string> ObtenerNombrePorCedula(string cedula)
        {
            string url = $"https://api.hacienda.go.cr/fe/ae?identificacion={cedula}";

            using (HttpClient client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Hacienda datos = serializer.Deserialize<Hacienda>(json);

                return datos?.nombre;
            }
        }

        public void SepararNombre(string nombreCompleto,
                                  out string nombre,
                                  out string primerApellido,
                                  out string segundoApellido)
        {
            string[] partes = nombreCompleto.Split(' ');

            if (partes.Length < 3)
            {
                nombre = partes[0];
                primerApellido = partes.Length > 1 ? partes[1] : "";
                segundoApellido = "";
                return;
            }

            segundoApellido = partes[partes.Length - 1];
            primerApellido = partes[partes.Length - 2];

            nombre = string.Join(" ", partes.Take(partes.Length - 2));
        }
    }
}
