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
    public class ProductoBLL : IProductoBLL
    {
        public List<Producto> GetByFilter(string pDescripcion)
        {
            IProductoDAL dal = new ProductoDAL();
            return dal.GetByFilter(pDescripcion);
        }

        public Producto GetById(int pId)
        {
            IProductoDAL dal = new ProductoDAL();
            return dal.GetById(pId);
        }

        public Task<IEnumerable<Producto>> GetAll()
        {
            IProductoDAL dal = new ProductoDAL();
            return dal.GetAll();
        }

        public Task<Producto> Save(Producto pNombre)
        {
            IProductoDAL dal = new ProductoDAL();
            Task<Producto> oProducto = null;

            if (dal.GetById(pNombre.Id) == null)
                oProducto = dal.Save(pNombre);
            else
                oProducto = dal.Update(pNombre);

            return oProducto;
        }

        public Task<bool> Delete(int pId)
        {
            IProductoDAL dal = new ProductoDAL();
            return dal.Delete(pId);
        }

        public void ValidarExistencia(int idProducto, int cantidadSolicitada)
        {
            IProductoDAL dal = new ProductoDAL();
            var producto = dal.GetById(idProducto);

            if (producto == null)
                throw new Exception($"El producto con ID {idProducto} no existe.");

            // Ajustá 'CantidadStock' al nombre real de tu propiedad
            int disponible = producto.CantidadStock;

            if (disponible < cantidadSolicitada)
                throw new Exception(
                    $"No hay suficiente inventario. Disponible: {disponible}, solicitado: {cantidadSolicitada}."
                );
        }

    }
}
