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
    public class InventarioDAL : IInventarioDAL
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
                string sql = @"Delete from Inventario Where (Id = @Id)";
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
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg);
                throw;
            }
        }

        public async Task<IEnumerable<Inventario>> GetAll()
        {
            List<Inventario> lista = new List<Inventario>();
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,TipoEstado,IdProducto,Cantidad,Observaciones,FechaRegistro 
                           from Inventario WITH (NOLOCK)";
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (SqlDataReader reader = await db.ExecuteReaderAsync(command))
                    {
                        while (await reader.ReadAsync())
                        {
                            Inventario oInventario = new Inventario();
                            oInventario.Id = int.Parse(reader["Id"].ToString());
                            Enum.TryParse(reader["TipoEstado"].ToString(), out EstadoInventario tipoEstado);
                            oInventario.TipoEstado = tipoEstado;
                            oInventario.IdProducto = int.Parse(reader["IdProducto"].ToString());
                            oInventario.Cantidad = int.Parse(reader["Cantidad"].ToString());
                            oInventario.Observaciones = reader["Observaciones"].ToString();
                            oInventario.FechaRegistro = DateTime.Parse(reader["FechaRegistro"].ToString());

                            lista.Add(oInventario);
                        }
                    }
                }

                return lista;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg);
                throw;
            }
        }

        public List<Inventario> GetByFilter(string pDescripcion)
        {
            DataSet ds = null;
            List<Inventario> lista = new List<Inventario>();
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,TipoEstado,IdProducto,Cantidad,Observaciones,FechaRegistro
                           from Inventario WITH (NOLOCK)
                           Where Observaciones like @filtro";

                command.Parameters.AddWithValue("@filtro", pDescripcion);
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
                        Inventario oInventario = new Inventario();
                        oInventario.Id = int.Parse(dr["Id"].ToString());
                        Enum.TryParse(dr["TipoEstado"].ToString(), out EstadoInventario tipoEstado);
                        oInventario.TipoEstado = tipoEstado;
                        oInventario.IdProducto = int.Parse(dr["IdProducto"].ToString());
                        oInventario.Cantidad = int.Parse(dr["Cantidad"].ToString());
                        oInventario.Observaciones = dr["Observaciones"].ToString();
                        oInventario.FechaRegistro = DateTime.Parse(dr["FechaRegistro"].ToString());

                        lista.Add(oInventario);
                    }
                }

                return lista;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg);
                throw;
            }
        }

        public Inventario GetById(int pId)
        {
            Inventario oInventario = null;
            SqlCommand command = new SqlCommand();
            string msg = "";

            try
            {
                string sql = @"Select Id,TipoEstado,IdProducto,Cantidad,Observaciones,FechaRegistro
                           from Inventario Where Id = @Id";

                command.Parameters.AddWithValue("@Id", pId);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    using (IDataReader dr = db.ExecuteReader(command))
                    {
                        while (dr.Read())
                        {
                            oInventario = new Inventario();
                            oInventario.Id = int.Parse(dr["Id"].ToString());
                            Enum.TryParse(dr["TipoEstado"].ToString(), out EstadoInventario tipoEstado);
                            oInventario.TipoEstado = tipoEstado;
                            oInventario.IdProducto = int.Parse(dr["IdProducto"].ToString());
                            oInventario.Cantidad = int.Parse(dr["Cantidad"].ToString());
                            oInventario.Observaciones = dr["Observaciones"].ToString();
                            oInventario.FechaRegistro = DateTime.Parse(dr["FechaRegistro"].ToString());
                        }
                    }
                }

                return oInventario;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg);
                throw;
            }
        }

        public async Task<Inventario> Save(Inventario pInventario)
        {
            string msg = "";
            Inventario oInventario = null;
            SqlCommand command = new SqlCommand();

            string sql = @"Insert into Inventario(TipoEstado,IdProducto,Cantidad,Observaciones,FechaRegistro)
                       values (@TipoEstado,@IdProducto,@Cantidad,@Observaciones,@FechaRegistro)";

            try
            {
                command.Parameters.AddWithValue("@TipoEstado", pInventario.TipoEstado.ToString());
                command.Parameters.AddWithValue("@IdProducto", pInventario.IdProducto);
                command.Parameters.AddWithValue("@Cantidad", pInventario.Cantidad);
                command.Parameters.AddWithValue("@Observaciones", pInventario.Observaciones);
                command.Parameters.AddWithValue("@FechaRegistro", pInventario.FechaRegistro);

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    var rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);

                    if (rows > 0)
                    {
                        oInventario = GetByFilter(pInventario.Observaciones).FirstOrDefault();
                    }
                }

                return oInventario;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg);
                throw;
            }
        }

        public async Task<Inventario> Update(Inventario pInventario)
        {
            string msg = "";
            int rows = 0;
            SqlCommand command = new SqlCommand();
            Inventario oInventario = new Inventario();

            string sql = @"Update Inventario SET 
                        TipoEstado = @TipoEstado,
                        IdProducto = @IdProducto,
                        Cantidad = @Cantidad,
                        Observaciones = @Observaciones,
                        FechaRegistro = @FechaRegistro
                       Where Id = @Id";

            try
            {
                command.Parameters.AddWithValue("@Id", pInventario.Id);
                command.Parameters.AddWithValue("@TipoEstado", pInventario.TipoEstado.ToString());
                command.Parameters.AddWithValue("@IdProducto", pInventario.IdProducto);
                command.Parameters.AddWithValue("@Cantidad", pInventario.Cantidad);
                command.Parameters.AddWithValue("@Observaciones", pInventario.Observaciones);
                command.Parameters.AddWithValue("@FechaRegistro", pInventario.FechaRegistro);

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    rows = await db.ExecuteNonQueryAsync(command, IsolationLevel.ReadCommitted);
                }

                if (rows > 0)
                    oInventario = this.GetById(pInventario.Id);

                return oInventario;
            }
            catch (SqlException er)
            {
                _myLogControlEventos.ErrorFormat("Error {0}",
                    msg.ToExceptionDetail(MethodBase.GetCurrentMethod(), er, command));
                throw new CustomException(msg.ToSqlServerDetailError(er));
            }
            catch (Exception er)
            {
                msg = msg.ToExceptionDetail(er, MethodBase.GetCurrentMethod());
                _myLogControlEventos.ErrorFormat("Error {0}", msg);
                throw;
            }
        }
    }
}
