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
    public class TipoDispositivoDAL : ITipoDispositivoDAL
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
                string sql = @"Delete from TipoDispositivo Where (Id = @Id)";
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

        public async Task<IEnumerable<TipoDispositivo>> GetAll()
        {
            List<TipoDispositivo> lista = new List<TipoDispositivo>();
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,Nombre,Estado from TipoDispositivo WITH (NOLOCK)";
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (SqlDataReader reader = await db.ExecuteReaderAsync(command))
                    {
                        while (await reader.ReadAsync())
                        {
                            TipoDispositivo oTipo = new TipoDispositivo();
                            oTipo.Id = int.Parse(reader["Id"].ToString());
                            oTipo.Nombre = reader["Nombre"].ToString();

                            // Conversión string → enum
                            Enum.TryParse(reader["Estado"].ToString(), out Estado estado);
                            oTipo.Estado = estado;

                            lista.Add(oTipo);
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

        public List<TipoDispositivo> GetByFilter(string pNombre)
        {
            DataSet ds = null;
            List<TipoDispositivo> lista = new List<TipoDispositivo>();
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,Nombre,Estado 
                           from TipoDispositivo WITH (NOLOCK)
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
                        TipoDispositivo oTipo = new TipoDispositivo();
                        oTipo.Id = int.Parse(dr["Id"].ToString());
                        oTipo.Nombre = dr["Nombre"].ToString();

                        Enum.TryParse(dr["Estado"].ToString(), out Estado estado);
                        oTipo.Estado = estado;

                        lista.Add(oTipo);
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

        public TipoDispositivo GetById(int pId)
        {
            TipoDispositivo oTipo = null;
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,Nombre,Estado from TipoDispositivo Where Id = @Id";

                command.Parameters.AddWithValue("@Id", pId);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader dr = db.ExecuteReader(command))
                    {
                        while (dr.Read())
                        {
                            oTipo = new TipoDispositivo();
                            oTipo.Id = int.Parse(dr["Id"].ToString());
                            oTipo.Nombre = dr["Nombre"].ToString();

                            Enum.TryParse(dr["Estado"].ToString(), out Estado estado);
                            oTipo.Estado = estado;
                        }
                    }
                }

                return oTipo;
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

        public async Task<TipoDispositivo> Save(TipoDispositivo pTipo)
        {
            string msg = "";
            TipoDispositivo oTipo = null;
            SqlCommand command = new SqlCommand();

            string sql = @"Insert into TipoDispositivo(Nombre,Estado)
                       values (@Nombre,@Estado)";

            try
            {
                command.Parameters.AddWithValue("@Nombre", pTipo.Nombre);
                command.Parameters.AddWithValue("@Estado", pTipo.Estado.ToString()); // enum → string

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);

                    if (rows > 0)
                    {
                        oTipo = GetByFilter(pTipo.Nombre).FirstOrDefault();
                    }
                }

                return oTipo;
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

        public async Task<TipoDispositivo> Update(TipoDispositivo pTipo)
        {
            string msg = "";
            int rows = 0;
            SqlCommand command = new SqlCommand();
            TipoDispositivo oTipo = new TipoDispositivo();

            string sql = @"Update TipoDispositivo SET 
                        Nombre = @Nombre,
                        Estado = @Estado
                       Where Id = @Id";

            try
            {
                command.Parameters.AddWithValue("@Id", pTipo.Id);
                command.Parameters.AddWithValue("@Nombre", pTipo.Nombre);
                command.Parameters.AddWithValue("@Estado", pTipo.Estado.ToString());

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);
                }

                if (rows > 0)
                    oTipo = this.GetById(pTipo.Id);

                return oTipo;
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
