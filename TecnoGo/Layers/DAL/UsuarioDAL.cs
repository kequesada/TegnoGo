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
    public class UsuarioDAL : IUsuarioDAL
    {
        private static readonly ILog _myLogControlEventos = LogManager.GetLogger("MyControlEventos");

        public Usuario Save(Usuario pUsuario)
        {
            string msg = "";
            SqlCommand command = new SqlCommand();
            Usuario oUsuario = null;
            string sql = @"Insert into Usuario(IdPerfil,Login,Password,Nombre,Estado) 
                       values (@IdPerfil,@Login,@Password,@Nombre,@Estado)";
            double row = 0;

            try
            {
                command.Parameters.AddWithValue("@IdPerfil", pUsuario.IdPerfil);
                command.Parameters.AddWithValue("@Login", pUsuario.Login);
                command.Parameters.AddWithValue("@Password", pUsuario.Password);
                command.Parameters.AddWithValue("@Nombre", pUsuario.Nombre);
                command.Parameters.AddWithValue("@Estado", pUsuario.Estado.ToString());

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                if (row > 0)
                    oUsuario = GetById(pUsuario.Login);

                return oUsuario;
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

        public Usuario Update(Usuario pUsuario)
        {
            SqlCommand command = new SqlCommand();
            Usuario oUsuario = null;
            string sql = @"Update Usuario 
                       SET IdPerfil=@IdPerfil, Password=@Password, Nombre=@Nombre, Estado=@Estado 
                       Where (Login = @Login)";
            double row = 0;
            string msg = "";

            try
            {
                command.Parameters.AddWithValue("@Login", pUsuario.Login);
                command.Parameters.AddWithValue("@IdPerfil", pUsuario.IdPerfil);
                command.Parameters.AddWithValue("@Password", pUsuario.Password);
                command.Parameters.AddWithValue("@Nombre", pUsuario.Nombre);
                command.Parameters.AddWithValue("@Estado", pUsuario.Estado);

                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                if (row > 0)
                    oUsuario = GetById(pUsuario.Login);

                return oUsuario;
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

        public Usuario Login(string pLogin, string pPassword)
        {
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Usuario oUsuario = null;
            string msg = "";

            try
            {
                command.CommandText = @"select * from Usuario with (rowlock)  
                                    where Login = @pLogin and Password = @pPassword";
                command.Parameters.AddWithValue("@pLogin", pLogin);
                command.Parameters.AddWithValue("@pPassword", pPassword);
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        oUsuario = new Usuario();
                        oUsuario.Id = int.Parse(reader["Id"].ToString());
                        oUsuario.IdPerfil = int.Parse(reader["IdPerfil"].ToString());
                        oUsuario.Login = reader["Login"].ToString();
                        oUsuario.Password = reader["Password"].ToString();
                        oUsuario.Nombre = reader["Nombre"].ToString();
                        oUsuario.Estado = (Estado)Enum.Parse(typeof(Estado), reader["Estado"].ToString());
                    }
                }

                return oUsuario;
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

        public Usuario GetById(string pLogin)
        {
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            Usuario oUsuario = null;
            string msg = "";

            try
            {
                command.CommandText = @"select * from Usuario with (rowlock)  
                                    where Login = @Login";
                command.Parameters.AddWithValue("@Login", pLogin);
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        oUsuario = new Usuario();
                        oUsuario.Id = int.Parse(reader["Id"].ToString());
                        oUsuario.IdPerfil = int.Parse(reader["IdPerfil"].ToString());
                        oUsuario.Login = reader["Login"].ToString();
                        oUsuario.Password = reader["Password"].ToString();
                        oUsuario.Nombre = reader["Nombre"].ToString();
                        oUsuario.Estado = (Estado)Enum.Parse(typeof(Estado), reader["Estado"].ToString());
                    }
                }

                return oUsuario;
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

        public IEnumerable<Usuario> GetAll()
        {
            string msg = "";
            SqlCommand command = new SqlCommand();
            IDataReader reader = null;
            IList<Usuario> lista = new List<Usuario>();

            try
            {
                command.CommandText = @"select * from Usuario with (NOLOCK)";
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    reader = db.ExecuteReader(command);

                    while (reader.Read())
                    {
                        Usuario oUsuario = new Usuario();
                        oUsuario.Id = int.Parse(reader["Id"].ToString());
                        oUsuario.IdPerfil = int.Parse(reader["IdPerfil"].ToString());
                        oUsuario.Login = reader["Login"].ToString();
                        oUsuario.Password = reader["Password"].ToString();
                        oUsuario.Nombre = reader["Nombre"].ToString();
                        oUsuario.Estado = (Estado)Enum.Parse(typeof(Estado), reader["Estado"].ToString());

                        lista.Add(oUsuario);
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

        public bool Delete(string pLogin)
        {
            SqlCommand command = new SqlCommand();
            string msg = "";
            string sql = @"Delete from Usuario where Login = @Login";
            double row = 0;

            try
            {
                command.Parameters.AddWithValue("@Login", pLogin);
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                using (IDataBase db = FactoryDatabase.CreateDataBase(FactoryConexion.CreateConnection()))
                {
                    row = db.ExecuteNonQuery(command, IsolationLevel.ReadCommitted);
                }

                return row > 0;
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
