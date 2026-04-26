using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TecnoGo.Enumeraciones;
using TecnoGo.Extensions;
using TecnoGo.Layers.Entities;
using TecnoGo.Layers.Interfaces;

namespace TecnoGo.Layers.DAL
{
    public class ProductoDAL : IProductoDAL
    {
        private static readonly ILog _myLogControlEventos = LogManager.GetLogger("MyControlEventos");

        public async Task<bool> Delete(int pId)
        {
            String msg = "";
            bool retorno = false;
            double rows = 0d;

            SqlCommand command = new SqlCommand();
            try
            {
                string sql = @"Delete from Producto Where (Id = @Id)";
                command.Parameters.AddWithValue("@Id", pId);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);
                }

                if (rows > 0)
                    retorno = true;

                return retorno;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<Producto>> GetAll()
        {
            List<Producto> lista = new List<Producto>();
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,Nombre,IdTipoDispositivo,IdMarca,Modelo,IdProveedor,Color,
                                  Caracteristicas,Extras,Fotografia,DocumentoEspecificaciones,
                                  CantidadStock,Precio,Estado
                           from Producto WITH (NOLOCK)";
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (SqlDataReader reader = await db.ExecuteReaderAsync(command))
                    {
                        while (await reader.ReadAsync())
                        {
                            Producto oProducto = new Producto();
                            oProducto.Id = int.Parse(reader["Id"].ToString());
                            oProducto.Nombre = reader["Nombre"].ToString();
                            oProducto.IdTipoDispositivo = int.Parse(reader["IdTipoDispositivo"].ToString());
                            oProducto.IdMarca = int.Parse(reader["IdMarca"].ToString());
                            oProducto.Modelo = reader["Modelo"].ToString();
                            oProducto.IdProveedor = int.Parse(reader["IdProveedor"].ToString());

                            Enum.TryParse(reader["Color"].ToString(), out Colores color);
                            oProducto.Color = color;

                            oProducto.Caracteristicas = reader["Caracteristicas"].ToString();
                            oProducto.Extras = reader["Extras"].ToString();
                            oProducto.Fotografia = reader["Fotografia"] as byte[];
                            oProducto.DocumentoEspecificaciones = reader["DocumentoEspecificaciones"] as byte[];
                            oProducto.CantidadStock = int.Parse(reader["CantidadStock"].ToString());
                            oProducto.Precio = double.Parse(reader["Precio"].ToString());

                            Enum.TryParse(reader["Estado"].ToString(), out EstadoGeneral estado);
                            oProducto.Estado = estado;


                            lista.Add(oProducto);
                        }
                    }
                }

                return lista;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                throw;
            }
        }

        public List<Producto> GetByFilter(string pNombre)
        {
            DataSet ds = null;
            List<Producto> lista = new List<Producto>();
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,Nombre,IdTipoDispositivo,IdMarca,Modelo,IdProveedor,Color,
                                  Caracteristicas,Extras,Fotografia,DocumentoEspecificaciones,
                                  CantidadStock,Precio,Estado
                           from Producto WITH (NOLOCK)
                           Where Nombre like @filtro";

                command.Parameters.AddWithValue("@filtro", pNombre);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    ds = db.ExecuteReader(command, "query");
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Producto oProducto = new Producto();
                        oProducto.Id = int.Parse(dr["Id"].ToString());
                        oProducto.Nombre = dr["Nombre"].ToString();
                        oProducto.IdTipoDispositivo = int.Parse(dr["IdTipoDispositivo"].ToString());
                        oProducto.IdMarca = int.Parse(dr["IdMarca"].ToString());
                        oProducto.Modelo = dr["Modelo"].ToString();
                        oProducto.IdProveedor = int.Parse(dr["IdProveedor"].ToString());

                        Enum.TryParse(dr["Color"].ToString(), out Colores color);
                        oProducto.Color = color;

                        oProducto.Caracteristicas = dr["Caracteristicas"].ToString();
                        oProducto.Extras = dr["Extras"].ToString();
                        oProducto.Fotografia = dr["Fotografia"] as byte[];
                        oProducto.DocumentoEspecificaciones = dr["DocumentoEspecificaciones"] as byte[];
                        oProducto.CantidadStock = int.Parse(dr["CantidadStock"].ToString());
                        oProducto.Precio = double.Parse(dr["Precio"].ToString());

                        Enum.TryParse(dr["Estado"].ToString(), out EstadoGeneral estado);
                        oProducto.Estado = estado;

                        lista.Add(oProducto);
                    }
                }

                return lista;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                throw;
            }
        }

        public Producto GetById(int pId)
        {
            Producto oProducto = null;
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,Nombre,IdTipoDispositivo,IdMarca,Modelo,IdProveedor,Color,
                                  Caracteristicas,Extras,Fotografia,DocumentoEspecificaciones,
                                  CantidadStock,Precio,Estado
                           from Producto Where Id = @Id";

                command.Parameters.AddWithValue("@Id", pId);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader dr = db.ExecuteReader(command))
                    {
                        while (dr.Read())
                        {
                            oProducto = new Producto();
                            oProducto.Id = int.Parse(dr["Id"].ToString());
                            oProducto.Nombre = dr["Nombre"].ToString();
                            oProducto.IdTipoDispositivo = int.Parse(dr["IdTipoDispositivo"].ToString());
                            oProducto.IdMarca = int.Parse(dr["IdMarca"].ToString());
                            oProducto.Modelo = dr["Modelo"].ToString();
                            oProducto.IdProveedor = int.Parse(dr["IdProveedor"].ToString());

                            Enum.TryParse(dr["Color"].ToString(), out Colores color);
                            oProducto.Color = color;

                            oProducto.Caracteristicas = dr["Caracteristicas"].ToString();
                            oProducto.Extras = dr["Extras"].ToString();
                            oProducto.Fotografia = dr["Fotografia"] as byte[];
                            oProducto.DocumentoEspecificaciones = dr["DocumentoEspecificaciones"] as byte[];
                            oProducto.CantidadStock = int.Parse(dr["CantidadStock"].ToString());
                            oProducto.Precio = double.Parse(dr["Precio"].ToString());

                            Enum.TryParse(dr["Estado"].ToString(), out EstadoGeneral estado);
                            oProducto.Estado = estado;
                        }
                    }
                }

                return oProducto;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                throw;
            }
        }

        public async Task<Producto> Save(Producto pProducto)
        {
            string msg = "";
            Producto oProducto = null;
            SqlCommand command = new SqlCommand();

            string sql = @"Insert into Producto(Nombre,IdTipoDispositivo,IdMarca,Modelo,IdProveedor,
                                            Color,Caracteristicas,Extras,Fotografia,
                                            DocumentoEspecificaciones,CantidadStock,Precio,Estado)
                       values (@Nombre,@IdTipoDispositivo,@IdMarca,@Modelo,@IdProveedor,
                               @Color,@Caracteristicas,@Extras,@Fotografia,
                               @DocumentoEspecificaciones,@CantidadStock,@Precio,@Estado)";

            try
            {
                command.Parameters.AddWithValue("@Nombre", pProducto.Nombre);
                command.Parameters.AddWithValue("@IdTipoDispositivo", pProducto.IdTipoDispositivo);
                command.Parameters.AddWithValue("@IdMarca", pProducto.IdMarca);
                command.Parameters.AddWithValue("@Modelo", pProducto.Modelo);
                command.Parameters.AddWithValue("@IdProveedor", pProducto.IdProveedor);
                command.Parameters.AddWithValue("@Color", pProducto.Color);
                command.Parameters.AddWithValue("@Caracteristicas", pProducto.Caracteristicas);
                command.Parameters.AddWithValue("@Extras", pProducto.Extras);
                command.Parameters.AddWithValue("@Fotografia", pProducto.Fotografia);
                command.Parameters.AddWithValue("@DocumentoEspecificaciones", pProducto.DocumentoEspecificaciones);
                command.Parameters.AddWithValue("@CantidadStock", pProducto.CantidadStock);
                command.Parameters.AddWithValue("@Precio", pProducto.Precio);
                command.Parameters.AddWithValue("@Estado", pProducto.Estado.ToString());

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);

                    if (rows > 0)
                    {
                        oProducto = GetByFilter(pProducto.Nombre).FirstOrDefault();
                    }
                }

                return oProducto;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                throw;
            }
        }

        public async Task<Producto> Update(Producto pProducto)
        {
            string msg = "";
            int rows = 0;
            SqlCommand command = new SqlCommand();
            Producto oProducto = new Producto();

            string sql = @"Update Producto SET 
                        Nombre = @Nombre,
                        IdTipoDispositivo = @IdTipoDispositivo,
                        IdMarca = @IdMarca,
                        Modelo = @Modelo,
                        IdProveedor = @IdProveedor,
                        Color = @Color,
                        Caracteristicas = @Caracteristicas,
                        Extras = @Extras,
                        Fotografia = @Fotografia,
                        DocumentoEspecificaciones = @DocumentoEspecificaciones,
                        CantidadStock = @CantidadStock,
                        Precio = @Precio,
                        Estado = @Estado
                       Where Id = @Id";

            try
            {
                command.Parameters.AddWithValue("@Id", pProducto.Id);
                command.Parameters.AddWithValue("@Nombre", pProducto.Nombre);
                command.Parameters.AddWithValue("@IdTipoDispositivo", pProducto.IdTipoDispositivo);
                command.Parameters.AddWithValue("@IdMarca", pProducto.IdMarca);
                command.Parameters.AddWithValue("@Modelo", pProducto.Modelo);
                command.Parameters.AddWithValue("@IdProveedor", pProducto.IdProveedor);
                command.Parameters.AddWithValue("@Color", pProducto.Color);
                command.Parameters.AddWithValue("@Caracteristicas", pProducto.Caracteristicas);
                command.Parameters.AddWithValue("@Extras", pProducto.Extras);
                command.Parameters.AddWithValue("@Fotografia", pProducto.Fotografia);
                command.Parameters.AddWithValue("@Documento", pProducto.DocumentoEspecificaciones);
                command.Parameters.AddWithValue("@CantidadStock", pProducto.CantidadStock);
                command.Parameters.AddWithValue("@Precio", pProducto.Precio);
                command.Parameters.AddWithValue("@Estado", pProducto.Estado.ToString());

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);
                }

                if (rows > 0)
                    oProducto = this.GetById(pProducto.Id);

                return oProducto;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg.ToString());
                throw;
            }
        }
    }
}
