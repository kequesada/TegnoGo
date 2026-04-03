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
    public class UsuarioBLL : IUsuarioBLL
    {
        public Usuario Login(string pLogin, string pPassword)
        {

            IUsuarioDAL dalUsuario = new UsuarioDAL();
            // Encriptar el password
            string crytpPasswd = Cryptography.EncrypthAES(pPassword);

            return dalUsuario.Login(pLogin, crytpPasswd);
        }

        public IEnumerable<Usuario> GetAll()
        {
            IUsuarioDAL dalUsuario = new UsuarioDAL();
            return dalUsuario.GetAll();
        }

        public Usuario Save(Usuario pUsuario)
        {
            IUsuarioDAL dalUsuario = new UsuarioDAL();
            string mensaje = "";
            Usuario oUsuario = null;

            if (!IsValidPassword(pUsuario.Password, ref mensaje))
            {
                throw new Exception(mensaje);
            }

            // Encriptar la contraseña.
            pUsuario.Password = Cryptography.EncrypthAES(pUsuario.Password);

            if (dalUsuario.GetById(pUsuario.Login) != null)
                oUsuario = dalUsuario.Update(pUsuario);
            else
                oUsuario = dalUsuario.Save(pUsuario);
            return oUsuario;
        }

        public Usuario GetById(string pLogin)
        {
            IUsuarioDAL dalUsuario = new UsuarioDAL();
            return dalUsuario.GetById(pLogin);
        }

        public bool Delete(string pLogin)
        {
            IUsuarioDAL dalUsuario = new UsuarioDAL();
            return dalUsuario.Delete(pLogin);
        }

        private bool IsValidPassword(string pPassword, ref string pMensaje)
        {
            if (pPassword.Trim().Length <= 6)
            {
                pMensaje = "El password debe ser mayor o igual a 6 caracteres";
                return false;
            }

            if (pPassword.Trim().Length > 10)
            {
                pMensaje = "El password debe ser menor o igual que 10 caracteres";
                return false;
            }

            return true;
        }
    }
}
