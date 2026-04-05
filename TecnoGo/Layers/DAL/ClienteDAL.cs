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
    public class ClienteDAL : IClienteDAL
    {
        private static readonly ILog _myLogControlEventos = LogManager.GetLogger("MyControlEventos");

        public async Task<bool> Delete(string pId)
        {
            String msg = "";
            bool retorno = false;
            double rows = 0d;

            SqlCommand command = new SqlCommand();
            try
            {
                string sql = @"Delete from Cliente Where (Id = @Id)";
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

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            List<Cliente> lista = new List<Cliente>();
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,TipoIdentificacion,Nombre,Apellido1,Apellido2,Sexo,Telefono,Correo,Provincia,Direccion,Fotografia,Estado 
                           from Cliente WITH (NOLOCK)";
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (SqlDataReader reader = await db.ExecuteReaderAsync(command))
                    {
                        while (await reader.ReadAsync())
                        {
                            Cliente oCliente = new Cliente();
                            oCliente.Id = reader["Id"].ToString();
                            oCliente.TipoIdentificacion = (TipoIdentificacion)Enum.Parse(typeof(TipoIdentificacion), reader["TipoIdentificacion"].ToString());
                            oCliente.Nombre = reader["Nombre"].ToString();
                            oCliente.Apellido1 = reader["Apellido1"].ToString();
                            oCliente.Apellido2 = reader["Apellido2"].ToString();
                            oCliente.Sexo = reader["Sexo"].ToString();
                            oCliente.Telefono = reader["Telefono"].ToString();
                            oCliente.Correo = reader["Correo"].ToString();
                            oCliente.Provincia = reader["Provincia"].ToString();
                            oCliente.Direccion = reader["Direccion"].ToString();
                            oCliente.Fotografia = reader["Fotografia"] as byte[];
                            oCliente.Estado = (Estado)Enum.Parse(typeof(Estado), reader["Estado"].ToString());

                            lista.Add(oCliente);
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

        public List<Cliente> GetByFilter(string pNombre)
        {
            DataSet ds = null;
            List<Cliente> lista = new List<Cliente>();
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,TipoIdentificacion,Nombre,Apellido1,Apellido2,Sexo,Telefono,Correo,Provincia,Direccion,Fotografia,Estado
                           from Cliente WITH (NOLOCK)
                           Where Nombre + Apellido1 + Apellido2 like @filtro";

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
                        Cliente oCliente = new Cliente();
                        oCliente.Id = dr["Id"].ToString();
                        oCliente.TipoIdentificacion = (TipoIdentificacion)Enum.Parse(typeof(TipoIdentificacion), dr["TipoIdentificacion"].ToString());
                        oCliente.Nombre = dr["Nombre"].ToString();
                        oCliente.Apellido1 = dr["Apellido1"].ToString();
                        oCliente.Apellido2 = dr["Apellido2"].ToString();
                        oCliente.Sexo = dr["Sexo"].ToString();
                        oCliente.Telefono = dr["Telefono"].ToString();
                        oCliente.Correo = dr["Correo"].ToString();
                        oCliente.Provincia = dr["Provincia"].ToString();
                        oCliente.Direccion = dr["Direccion"].ToString();
                        oCliente.Fotografia = dr["Fotografia"] as byte[];
                        oCliente.Estado = (Estado)Enum.Parse(typeof(Estado), dr["Estado"].ToString());

                        lista.Add(oCliente);
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

        public Cliente GetById(string pId)
        {
            Cliente oCliente = null;
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,TipoIdentificacion,Nombre,Apellido1,Apellido2,Sexo,Telefono,Correo,Provincia,Direccion,Fotografia,Estado
                           from Cliente Where Id = @Id";

                command.Parameters.AddWithValue("@Id", pId);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader dr = db.ExecuteReader(command))
                    {
                        while (dr.Read())
                        {
                            oCliente = new Cliente();
                            oCliente.Id = dr["Id"].ToString();
                            oCliente.TipoIdentificacion = (TipoIdentificacion)Enum.Parse(typeof(TipoIdentificacion), dr["TipoIdentificacion"].ToString());
                            oCliente.Nombre = dr["Nombre"].ToString();
                            oCliente.Apellido1 = dr["Apellido1"].ToString();
                            oCliente.Apellido2 = dr["Apellido2"].ToString();
                            oCliente.Sexo = dr["Sexo"].ToString();
                            oCliente.Telefono = dr["Telefono"].ToString();
                            oCliente.Correo = dr["Correo"].ToString();
                            oCliente.Provincia = dr["Provincia"].ToString();
                            oCliente.Direccion = dr["Direccion"].ToString();
                            oCliente.Fotografia = dr["Fotografia"] as byte[];
                            oCliente.Estado = (Estado)Enum.Parse(typeof(Estado), dr["Estado"].ToString());
                        }
                    }
                }

                return oCliente;
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

        public async Task<Cliente> Save(Cliente pCliente)
        {
            string msg = "";
            Cliente oCliente = null;
            SqlCommand command = new SqlCommand();

            string sql = @"Insert into Cliente(Id,TipoIdentificacion,Nombre,Apellido1,Apellido2,Sexo,Telefono,Correo,Provincia,Direccion,Fotografia,Estado)
                       values (@Id,@TipoIdentificacion,@Nombre,@Apellido1,@Apellido2,@Sexo,@Telefono,@Correo,@Provincia,@Direccion,@Fotografia,@Estado)";

            try
            {
                command.Parameters.AddWithValue("@Id", pCliente.Id);
                command.Parameters.AddWithValue("@TipoIdentificacion", pCliente.TipoIdentificacion);
                command.Parameters.AddWithValue("@Nombre", pCliente.Nombre);
                command.Parameters.AddWithValue("@Apellido1", pCliente.Apellido1);
                command.Parameters.AddWithValue("@Apellido2", pCliente.Apellido2);
                command.Parameters.AddWithValue("@Sexo", pCliente.Sexo);
                command.Parameters.AddWithValue("@Telefono", pCliente.Telefono);
                command.Parameters.AddWithValue("@Correo", pCliente.Correo);
                command.Parameters.AddWithValue("@Provincia", pCliente.Provincia);
                command.Parameters.AddWithValue("@Direccion", pCliente.Direccion);
                command.Parameters.AddWithValue("@Fotografia", pCliente.Fotografia);
                command.Parameters.AddWithValue("@Estado", pCliente.Estado);

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);

                    if (rows > 0)
                        oCliente = this.GetById(pCliente.Id);
                }

                return oCliente;
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

        public async Task<Cliente> Update(Cliente pCliente)
        {
            string msg = "";
            int rows = 0;
            SqlCommand command = new SqlCommand();
            Cliente oCliente = new Cliente();

            string sql = @"Update Cliente SET 
                        TipoIdentificacion = @TipoIdentificacion,
                        Nombre = @Nombre,
                        Apellido1 = @Apellido1,
                        Apellido2 = @Apellido2,
                        Sexo = @Sexo,
                        Telefono = @Telefono,
                        Correo = @Correo,
                        Provincia = @Provincia,
                        Direccion = @Direccion,
                        Fotografia = @Fotografia,
                        Estado = @Estado
                        Where Id = @Id";

            try
            {
                command.Parameters.AddWithValue("@Id", pCliente.Id);
                command.Parameters.AddWithValue("@TipoIdentificacion", pCliente.TipoIdentificacion);
                command.Parameters.AddWithValue("@Nombre", pCliente.Nombre);
                command.Parameters.AddWithValue("@Apellido1", pCliente.Apellido1);
                command.Parameters.AddWithValue("@Apellido2", pCliente.Apellido2);
                command.Parameters.AddWithValue("@Sexo", pCliente.Sexo);
                command.Parameters.AddWithValue("@Telefono", pCliente.Telefono);
                command.Parameters.AddWithValue("@Correo", pCliente.Correo);
                command.Parameters.AddWithValue("@Provincia", pCliente.Provincia);
                command.Parameters.AddWithValue("@Direccion", pCliente.Direccion);
                command.Parameters.AddWithValue("@Fotografia", pCliente.Fotografia);
                command.Parameters.AddWithValue("@Estado", pCliente.Estado);

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);
                }

                if (rows > 0)
                    oCliente = this.GetById(pCliente.Id);

                return oCliente;
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
