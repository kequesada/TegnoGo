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
    public class ProveedorDAL : IProveedorDAL
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
                string sql = @"Delete from Proveedor Where (Id = @Id)";
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

        public async Task<IEnumerable<Proveedor>> GetAll()
        {
            List<Proveedor> lista = new List<Proveedor>();
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,Nombre,Estado from Proveedor WITH (NOLOCK)";
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (SqlDataReader reader = await db.ExecuteReaderAsync(command))
                    {
                        while (await reader.ReadAsync())
                        {
                            Proveedor oProveedor = new Proveedor();
                            oProveedor.Id = int.Parse(reader["Id"].ToString());
                            oProveedor.Nombre = reader["Nombre"].ToString();

                            Enum.TryParse(reader["Estado"].ToString(), out EstadoGeneral estado);
                            oProveedor.Estado = estado;

                            lista.Add(oProveedor);
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

        public List<Proveedor> GetByFilter(string pNombre)
        {
            DataSet ds = null;
            List<Proveedor> lista = new List<Proveedor>();
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,Nombre,Estado 
                           from Proveedor WITH (NOLOCK)
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
                        Proveedor oProveedor = new Proveedor();
                        oProveedor.Id = int.Parse(dr["Id"].ToString());
                        oProveedor.Nombre = dr["Nombre"].ToString();

                        Enum.TryParse(dr["Estado"].ToString(), out EstadoGeneral estado);
                        oProveedor.Estado = estado;

                        lista.Add(oProveedor);
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

        public Proveedor GetById(int pId)
        {
            Proveedor oProveedor = null;
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,Nombre,Estado from Proveedor Where Id = @Id";

                command.Parameters.AddWithValue("@Id", pId);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader dr = db.ExecuteReader(command))
                    {
                        while (dr.Read())
                        {
                            oProveedor = new Proveedor();
                            oProveedor.Id = int.Parse(dr["Id"].ToString());
                            oProveedor.Nombre = dr["Nombre"].ToString();

                            Enum.TryParse(dr["Estado"].ToString(), out EstadoGeneral estado);
                            oProveedor.Estado = estado;
                        }
                    }
                }

                return oProveedor;
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

        public async Task<Proveedor> Save(Proveedor pProveedor)
        {
            string msg = "";
            Proveedor oProveedor = null;
            SqlCommand command = new SqlCommand();

            string sql = @"Insert into Proveedor(Nombre,Estado)
                       values (@Nombre,@Estado)";

            try
            {
                command.Parameters.AddWithValue("@Nombre", pProveedor.Nombre);
                command.Parameters.AddWithValue("@Estado", pProveedor.Estado.ToString());

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);

                    if (rows > 0)
                    {
                        oProveedor = GetByFilter(pProveedor.Nombre).FirstOrDefault();
                    }
                }

                return oProveedor;
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

        public async Task<Proveedor> Update(Proveedor pProveedor)
        {
            string msg = "";
            int rows = 0;
            SqlCommand command = new SqlCommand();
            Proveedor oProveedor = new Proveedor();

            string sql = @"Update Proveedor SET 
                        Nombre = @Nombre,
                        Estado = @Estado
                       Where Id = @Id";

            try
            {
                command.Parameters.AddWithValue("@Id", pProveedor.Id);
                command.Parameters.AddWithValue("@Nombre", pProveedor.Nombre);
                command.Parameters.AddWithValue("@Estado", pProveedor.Estado.ToString());

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);
                }

                if (rows > 0)
                    oProveedor = this.GetById(pProveedor.Id);

                return oProveedor;
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
