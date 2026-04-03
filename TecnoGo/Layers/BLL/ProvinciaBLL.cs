using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TecnoGo.Layers.DAL;
using TecnoGo.Layers.Entities;
using TecnoGo.Layers.Interfaces;

namespace TecnoGo.Layers.BLL
{
    /*public class BLLProvincia : IProvinciaBLL
    {
        public bool Delete(int pId)
        {
            IProvinciaDAL dalProvincia = new ProvinciaDAL();
            return dalProvincia.Delete(pId);
        }
        public List<Provincia> GetAll()
        {
            IProvinciaDAL dalProvincia = new ProvinciaDAL();
            return dalProvincia.GetAll();
        }
        public Provincia GetById(int pId)
        {
            IProvinciaDAL dalProvincia = new ProvinciaDAL();
            return dalProvincia.GetById(pId);
        }
        /// <summary>
        /// Leerlo json de internet acceder https://github.com/lateraluz/Datos y buscar el archivo provincias.json
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        /// <exception cref=""></exception>
        public Provincia GetProvinciaFromInternet(int pId)
        {
            Provincia provincia = null;
            string json = "";
            // Leer del App.Config el URL con el Key URLPadron
            string url = ConfigurationManager.AppSettings["URLProvincia"];
            // Creates a GET request to fetch  
            WebRequest request = WebRequest.Create(url);
            // Verb GET
            request.Method = "GET";
            // GetResponse returns a web response containing the response to the request
            using (WebResponse webResponse = request.GetResponse())
            {
                // Reading data
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                json = reader.ReadToEnd();
            }
            // Todas las provincias
            List<Provincia> lista = JsonSerializer.Deserialize<List<Provincia>>(json);
            provincia = lista.Find(p => p.IdProvincia == pId);
            return provincia;
        }
        public Provincia Save(Provincia pProvincia)
        {
            IProvinciaDAL dalProvincia = new ProvinciaDAL();
            Provincia oProvincia = null;
            if (dalProvincia.GetById(pProvincia.IdProvincia) == null)
                oProvincia = dalProvincia.Save(pProvincia);
            else
                oProvincia = dalProvincia.Update(pProvincia);
            return oProvincia;
        }
    }*/
}

